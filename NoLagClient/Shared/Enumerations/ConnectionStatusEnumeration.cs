namespace NoLagClient.Shared;

public class ConnectionStatusEnumeration(int id, string name, short status) : Enumeration(id, name)
{
    public static ConnectionStatusEnumeration Idle = new ConnectionStatusEnumeration(0, nameof(Idle), 0);
    public static ConnectionStatusEnumeration Connecting = new ConnectionStatusEnumeration(1, nameof(Connecting), 6);
    public static ConnectionStatusEnumeration Connected = new ConnectionStatusEnumeration(2, nameof(Connected), 66);
    public static ConnectionStatusEnumeration Disconnected = new ConnectionStatusEnumeration(3, nameof(Disconnected), 666);
}