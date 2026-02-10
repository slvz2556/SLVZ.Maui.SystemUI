

#if ANDROID
using AndroidX.Activity;
using AndroidX.Core.View;
#endif

namespace SLVZ.Maui.SystemUI;

public enum UITheme
{
    Light,
    Dark,
    Unspecified
}
public static class ThemeHelper
{
    /// <summary>
    /// It will retuns native os theme
    /// </summary>
    public static UITheme SystemTheme
    {
        get
        {
#if ANDROID
            var nightMode = Android.App.Application.Context.Resources.Configuration.UiMode
                & Android.Content.Res.UiMode.NightMask;

            return nightMode == Android.Content.Res.UiMode.NightYes ? UITheme.Dark : UITheme.Light;
#elif WINDOWS
            var uiSettings = new Windows.UI.ViewManagement.UISettings();
            var color = uiSettings.GetColorValue(
                Windows.UI.ViewManagement.UIColorType.Background);

            return color == Windows.UI.Color.FromArgb(255,0,0,0) ? UITheme.Dark : UITheme.Light;
#endif
        }
    }


    /// <summary>
    /// Raised when the system theme changes.
    /// </summary>
    public static event EventHandler<UITheme>? OnThemeChange;

    static ThemeHelper()
    {
        // Replace this with your actual system event
        Application.Current.RequestedThemeChanged -= OnSystemThemeChanged;
        Application.Current.RequestedThemeChanged += OnSystemThemeChanged;
    }

    private static void OnSystemThemeChanged(object? sender, EventArgs e)
    {
        // Automatically invoke our event for all subscribers
        OnThemeChange?.Invoke(sender, SystemTheme);
    }





#if ANDROID

    /// <summary>
    /// Sets the status bar color to the specified MAUI color.
    /// </summary>
    /// <param name="color">The MAUI color to apply to the status bar.</param>
    /// <param name="autoContentColor">
    /// If true (default), automatically adjusts the status bar text color 
    /// to be either white or black based on the brightness of the provided color.
    /// If false, the text color will remain unchanged.
    /// </param>
    public static void SetStatusBarColor(Color color, bool autoContentColor = true)
    {
        Android.Graphics.Color mycolor = Android.Graphics.Color.Argb(
            (int)(color.Alpha * 255),
            (int)(color.Red * 255),
            (int)(color.Green * 255),
            (int)(color.Blue * 255));

        var activity = Platform.CurrentActivity as ComponentActivity;
        var controller = WindowCompat.GetInsetsController(activity?.Window, activity?.Window?.DecorView);

        activity?.Window?.SetStatusBarColor(mycolor);

        if (autoContentColor)
        {
            double luminance = color.Red * 0.2126 +
                color.Green * 0.7152 +
                color.Blue * 0.0722;

            if (luminance >= 0.5)
                controller?.AppearanceLightStatusBars = true;
            else
                controller?.AppearanceLightStatusBars = false;
        }
    }


    /// <summary>
    /// Sets the navigation bar color to the specified MAUI color.
    /// </summary>
    /// <param name="color">The MAUI color to apply to the navigation bar.</param>
    /// <param name="autoContentColor">
    /// If true (default), automatically adjusts the navigation bar text color 
    /// to be either white or black based on the brightness of the provided color.
    /// If false, the text color will remain unchanged.
    /// </param>
    public static void SetNavigationBarColor(Color color, bool autoContentColor = true)
    {
        Android.Graphics.Color mycolor = Android.Graphics.Color.Argb(
            (int)(color.Alpha * 255),
            (int)(color.Red * 255),
            (int)(color.Green * 255),
            (int)(color.Blue * 255));

        var activity = Platform.CurrentActivity as ComponentActivity;
        var controller = WindowCompat.GetInsetsController(activity?.Window, activity?.Window?.DecorView);

        activity?.Window?.SetNavigationBarColor(mycolor);

        if (autoContentColor)
        {
            double luminance = color.Red * 0.2126 +
            color.Green * 0.7152 +
            color.Blue * 0.0722;

            if (luminance >= 0.5)
                controller?.AppearanceLightNavigationBars = true;
            else
                controller?.AppearanceLightNavigationBars = false;
        }
    }




    /// <summary>
    /// Sets the status bar content color based on the specified app theme.
    /// </summary>
    /// <param name="theme">
    /// The current app theme.
    /// If <see cref="UITheme.Dark"/> is provided, it indicates the app is in dark mode,
    /// so the status bar content (text/icons) should be light for visibility.
    /// If <see cref="UITheme.Light"/> is provided, the content should be dark.
    /// </param>
    public static void SetStatusBarContentColor(UITheme theme)
    {
        var activity = Platform.CurrentActivity as ComponentActivity;
        var controller = WindowCompat.GetInsetsController(activity?.Window, activity?.Window?.DecorView);



        if (theme==UITheme.Dark)
            controller?.AppearanceLightStatusBars = false;
        else
            controller?.AppearanceLightStatusBars = true;
    }


    /// <summary>
    /// Sets the nacigation bar content color based on the specified app theme.
    /// </summary>
    /// <param name="theme">
    /// The current app theme.
    /// If <see cref="UITheme.Dark"/> is provided, it indicates the app is in dark mode,
    /// so the nacigation bar content (text/icons) should be light for visibility.
    /// If <see cref="UITheme.Light"/> is provided, the content should be dark.
    /// </param>
    public static void SetNavigationBarContentColor(UITheme theme)
    {
        var activity = Platform.CurrentActivity as ComponentActivity;
        var controller = WindowCompat.GetInsetsController(activity?.Window, activity?.Window?.DecorView);



        if (theme == UITheme.Dark)
            controller?.AppearanceLightNavigationBars = false;
        else
            controller?.AppearanceLightNavigationBars = true;
    }

#endif
}
