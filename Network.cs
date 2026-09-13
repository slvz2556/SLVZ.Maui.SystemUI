

namespace SLVZ.Maui.SystemUI;

public enum ConnectionProfile
{
    /// <summary>
    /// No connection profile is available.
    /// </summary>
    None,
    /// <summary>
    /// The device is connected to a Wi-Fi network.
    /// </summary>
    WiFi,
    /// <summary>
    /// The device is connected to a cellular network.
    /// </summary>
    Cellular,
    /// <summary>
    /// The device is connected to an Ethernet network.
    /// </summary>
    Ethernet,
    /// <summary>
    /// The device is connected to a Bluetooth network.
    /// </summary>
    Bluetooth,

    All
}





public class Network
{
    /// <summary>
    /// Raised when the system network changes.
    /// </summary>
    public static event EventHandler<List<ConnectionProfile>>? OnConnectionChange;

    static Network()
    {
        // Replace this with your actual system event
        Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    private static void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        var list = new List<ConnectionProfile>();

        foreach(var profile in e.ConnectionProfiles)
        {
            list.Add(profile switch
            {
                Microsoft.Maui.Networking.ConnectionProfile.Unknown => ConnectionProfile.None,
                Microsoft.Maui.Networking.ConnectionProfile.WiFi => ConnectionProfile.WiFi,
                Microsoft.Maui.Networking.ConnectionProfile.Bluetooth => ConnectionProfile.Bluetooth,
                Microsoft.Maui.Networking.ConnectionProfile.Ethernet => ConnectionProfile.Ethernet,
                Microsoft.Maui.Networking.ConnectionProfile.Cellular => ConnectionProfile.Cellular
            });
        }

        OnConnectionChange?.Invoke(sender, list);
    }

    public static bool Connected
    {
        get => (Connectivity.ConnectionProfiles.Contains(Microsoft.Maui.Networking.ConnectionProfile.WiFi) || Connectivity.ConnectionProfiles.Contains(Microsoft.Maui.Networking.ConnectionProfile.Bluetooth) || Connectivity.ConnectionProfiles.Contains(Microsoft.Maui.Networking.ConnectionProfile.Ethernet) || Connectivity.ConnectionProfiles.Contains(Microsoft.Maui.Networking.ConnectionProfile.Cellular))
            && Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
