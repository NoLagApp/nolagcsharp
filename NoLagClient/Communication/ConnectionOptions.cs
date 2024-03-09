namespace NoLagClient.Communication;

public class ConnectOptions
{
    public string Host { get; set; }
    public string WsHost { get; set; }
    public int? Port { get; set; }
    public string Path { get; set; }
    public string Protocol { get; set; }
    public string Url { get; set; }
    public string WsUrl { get; set; }
    public bool? DevMode { get; set; }
    public int? CheckConnectionInterval { get; set; }
    public int? CheckConnectionTimeout { get; set; }
}