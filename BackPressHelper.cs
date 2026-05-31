#if ANDROID
using AndroidX.Activity;

namespace SLVZ.Maui.SystemUI;

public static class BackPressHelper
{
    public static event EventHandler OnBackPressed = delegate { };

    private static bool _isInitialized = false;

    static BackPressHelper()
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
        => BackPressHelper.OnBackPressed.Invoke("BackPressHandler", EventArgs.Empty);
    }
}
#endif