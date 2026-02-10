#if ANDROID
using AndroidX.Activity;

namespace SLVZ.Maui.SystemUI;

public class BackPressHandler
{
    public static event EventHandler OnBackPressed = delegate { };

    private static bool _isInitialized = false;

    static BackPressHandler()
    {
        if (_isInitialized) return;
        var activity = Platform.CurrentActivity as ComponentActivity;
        var callback = new BackCallback(true);
        activity?.OnBackPressedDispatcher.AddCallback(activity, callback);
        _isInitialized = true;
    }

    class BackCallback : OnBackPressedCallback
    {
        public BackCallback(bool enabled) : base(enabled) { }

        public override void HandleOnBackPressed()
        => BackPressHandler.OnBackPressed.Invoke("BackPressHandler", EventArgs.Empty);
    }
}
#endif