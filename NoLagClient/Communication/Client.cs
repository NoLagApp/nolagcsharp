using System.Net;
using System.Net.WebSockets;
using System.Text;
using NoLagClient.Shared;

namespace NoLagClient.Communication;

/**
 * This client manages the raw connection to and from web socket server
 */
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

    // The following funcs take in 
    // 1. string: message coming from server (if any)
    // return: a task so we can make the func async
    private Func<string?, Task> callbackOnOpenAsync;
    private Func<string?, Task> callbackOnReceiveAsync;
    private Func<string?, Task> callbackOnCloseAsync;
    private Func<string?, Task> callbackOnErrorAsync;

    // TODO: Need a way to clean up this resource on 
    // Close
    // Error
    private ClientWebSocket _clientWebSocket;

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
     * This is the main task for the library.
     * It does two things
     * 1. Connect to the socket server
     * 2. Listen to incoming messages
     */
    public async Task ConnectAndListenToMessages(CancellationToken cancellationToken)
    {
        this.connectionStatus = ConnectionStatusEnumeration.Idle;
        // Replace the WebSocket URL with the appropriate server address
        string webSocketUrl = $"{this.protocol}://{this.host}";

        // avoid initiate
        if (Equals(this.connectionStatus, ConnectionStatusEnumeration.Connected))
        {
            return;
        }

        this._clientWebSocket = new ClientWebSocket();
        Console.WriteLine("Connecting to the WebSocket server...");
        await _clientWebSocket.ConnectAsync(new Uri(webSocketUrl), cancellationToken);

        // Invoke lister on successfully connected
        await this.callbackOnOpenAsync(null);

        // Start a separate thread to handle incoming messages
        _ = Task.Run(() => this.ReceiveMessage(this._clientWebSocket, cancellationToken));

        // // Send a message to the server
        // string message = "Hello, WebSocket server!";
        // await SendMessage(_clientWebSocket, message);
    }

    private async Task ReceiveMessage(ClientWebSocket webSocket, CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[1024];

        while (webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result =
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {receivedMessage}");
                await this.callbackOnReceiveAsync(receivedMessage);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
            }
        }
    }

    private async Task Authenticate(string message)
    {
        
    }

    public async Task Disconnect(ClientWebSocket webSocket, CancellationToken cancellationToken)
    {
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
    }

    public async Task SendMessage(ClientWebSocket webSocket, string message, CancellationToken cancellationToken)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
    }
}