
#if ANDROID
using Android.Views;
using AndroidX.Activity;

namespace SLVZ.Maui.SystemUI;

public class ScreenHelper
{
    public static void FullScreenMode()
    {
        var activity = Platform.CurrentActivity as ComponentActivity;

        var uiOptions = SystemUiFlags.HideNavigation
                        | SystemUiFlags.ImmersiveSticky
                        | SystemUiFlags.Fullscreen;

        
        activity?.Window?.DecorView?.SystemUiVisibility = (StatusBarVisibility)uiOptions;
    }

    public static void ExitFullScreenMode()
    {
        var activity = Platform.CurrentActivity as ComponentActivity;
        var uiOptions = SystemUiFlags.Visible;
        activity?.Window?.DecorView?.SystemUiVisibility = (StatusBarVisibility)uiOptions;
    }
}

#endif