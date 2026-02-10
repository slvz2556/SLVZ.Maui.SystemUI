

namespace SLVZ.Maui.SystemUI;

public class Network
{
    /// <summary>
    /// Raised when the system network changes.
    /// </summary>
    public static event EventHandler<ConnectivityChangedEventArgs>? OnConnectionChange;

    static Network()
    {
        // Replace this with your actual system event
        Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    private static void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        OnConnectionChange?.Invoke(sender, e);
    }

    public static bool Connected
    {
        get => (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Bluetooth) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Ethernet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular))
            && Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
