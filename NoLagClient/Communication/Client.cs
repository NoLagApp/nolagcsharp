using NoLagClient.Shared;

namespace NoLagClient.Communication;

public class Client
{
    private string host;
    private string authToken;
    public object wsInstance = null;
    private string protocol;
    private string url;
    private string deviceConnectionId = null;
    public string deviceTokenId = null;
    //  check connection
    private int defaultCheckConnectionInterval = 100;
    private int defaultCheckConnectionTimeout = 10000;
    private int checkConnectionInterval;
    private int checkConnectionTimeout;

    private Action<string, string> callbackOnOpen;
    private Action<string, string> callbackOnReceive;
    private Action<string, string> callbackOnClose;
    private Action<string, string> callbackOnError;
    
    private ConnectionStatusEnumeration connectionStatus = ConnectionStatusEnumeration.Idle;

    public Client(string authToken, ConnectOptions? connectOptions)
    {
        this.authToken = authToken ?? "";
        this.host = connectOptions?.Host ?? Constants.DefaultWsHost;
        this.protocol = connectOptions?.Protocol ?? Constants.DefaultWsProtocol;
        this.url = Constants.DefaultWsUrl;
        this.checkConnectionInterval =
            connectOptions?.CheckConnectionInterval ??
            this.defaultCheckConnectionInterval;
        this.checkConnectionTimeout =
            connectOptions?.CheckConnectionTimeout ??
            this.defaultCheckConnectionTimeout;
    }
    
    
    /**
 * Promise - Setup the connection process, code will detect if the code is being used in the front-end or backend
 * @param callbackMain used as a event trigger
 * @returns NoLagClient instance
 */
    public Client connect(): Client {
        this.connectionStatus = ConnectionStatusEnumeration.Idle;
        
        return new Promise((resolve, reject) => {
            const checkConnection = setInterval(() => {
                if (this.connectionStatus === EConnectionStatus.Connected) {
                    resolve(this);
                    clearInterval(checkConnection);
                }
            }, this.checkConnectionInterval);
            setTimeout(() => {
                if (this.connectionStatus === EConnectionStatus.Idle) {
                    reject(true);
                    clearInterval(checkConnection);
                }
            }, this.checkConnectionTimeout);
        });
    }
}