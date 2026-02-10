#if ANDROID
using Android.App;
using AndroidX.Activity;
using AndroidX.Core.View;
using AndroidX.Lifecycle;
using System.Diagnostics;

namespace SLVZ.Maui.SystemUI;

public class Edge2EdgeHandler
{


    private static Page? _page { get; set; }
    private static Shell? _shell { get; set; }

    private static InsetsListener _Insets;

    public static int NavbarHeight { get; private set; } = 0;
    public static int StatusbarHeight { get; private set; } = 0;

    private static bool _autoFit { get; set; }




    /// <summary>
    /// Configures the specified page for proper edge-to-edge layout handling.
    /// </summary>
    /// <param name="page">The page to configure.</param>
    /// <param name="AutoFit">
    /// If true (default), automatically calculates the status bar and navigation bar heights,
    /// handles edge-to-edge layout, and manages the on-screen keyboard so that nothing
    /// in your app is obscured when the keyboard appears.
    /// If false, no automatic adjustments are made; you can manually access
    /// <c>StatusBarHeight</c> and <c>NavbarHeight</c> to apply padding or layout changes.
    /// This is especially useful when building a full-screen app.
    /// </param>
    public static void SetPage(Page page, bool AutoFit = true)
    {
        _page = page;
        _autoFit = AutoFit;        

        page.Loaded -= OnLoaded;
        page.Loaded += OnLoaded;
    }



    /// <summary>
    /// Configures the specified shell for proper edge-to-edge layout handling.
    /// </summary>
    /// <param name="shell">The shell to configure.</param>
    /// <param name="AutoFit">
    /// If true (default), automatically calculates the status bar and navigation bar heights,
    /// handles edge-to-edge layout, and manages the on-screen keyboard so that nothing
    /// in your app is obscured when the keyboard appears.
    /// If false, no automatic adjustments are made; you can manually access
    /// <c>StatusBarHeight</c> and <c>NavbarHeight</c> to apply padding or layout changes.
    /// This is especially useful when building a full-screen app.
    /// </param>
    public static void SetShell(Shell shell, bool AutoFit = true)
    {
        _shell = shell;
        _autoFit = AutoFit;

        _shell.Loaded -= OnLoaded;
        _shell.Loaded += OnLoaded;
    }




    private static async void OnLoaded(object? sender, EventArgs e)
    {
        await Initialize();
    }

    private static async Task Initialize()
    {
        try
        {

            var activity = Platform.CurrentActivity as ComponentActivity;


            WindowCompat.SetDecorFitsSystemWindows(activity?.Window, false);

            var decorView = activity?.Window?.DecorView;
            var insets = decorView?.RootWindowInsets;

            float density = activity.Resources.DisplayMetrics.Density;

            int convertedValue = (int)(insets.SystemWindowInsetTop / density);
            StatusbarHeight = convertedValue;

            convertedValue = (int)(insets.StableInsetBottom / density);
            NavbarHeight = convertedValue;

            if (_autoFit)
            {
                if (_shell is not null)
                    _shell.Padding = new Thickness(0, StatusbarHeight, 0, NavbarHeight);
                else if (_page is not null)
                    _page.Padding = new Thickness(0, StatusbarHeight, 0, NavbarHeight);
            }

            _Insets = new InsetsListener();

            ViewCompat.SetOnApplyWindowInsetsListener(activity.Window?.DecorView, _Insets);

            _Insets.ThicknessChanged -= Insets_ThicknessChanged;
            _Insets.ThicknessChanged += Insets_ThicknessChanged;

        }
        catch (Exception e) { throw new Exception(e.Message); }
    }

    private static void Insets_ThicknessChanged(object? sender, Thickness e)
    {
        if (_autoFit)
        {
            if (e.Bottom > 200)
            {
                if (_shell is not null)
                    _shell.Padding = new Thickness(0, StatusbarHeight, 0, 0);
                else if (_page is not null)
                    _page.Padding = new Thickness(0, StatusbarHeight, 0, 0);
            }
            else
            {
                if (_page is not null)
                    _page.Padding = new Thickness(0, StatusbarHeight, 0, NavbarHeight);
                else if (_shell is not null)
                    _shell.Padding = new Thickness(0, StatusbarHeight, 0, NavbarHeight);
            }
        }
    }


    class InsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {

        public event EventHandler<Thickness>? ThicknessChanged;

        public WindowInsetsCompat? OnApplyWindowInsets(global::Android.Views.View? v, WindowInsetsCompat? insets)
        {
            var imeInsets = insets?.GetInsets(WindowInsetsCompat.Type.Ime());
            v.SetPadding(0, 0, 0, imeInsets.Bottom);
            ThicknessChanged?.Invoke(0, new Thickness(imeInsets.Left, imeInsets.Top, imeInsets.Right, imeInsets.Bottom));

            return insets;
        }
    }

}


#endif