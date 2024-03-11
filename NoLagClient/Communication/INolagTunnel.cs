namespace NoLagClient.Communication;

/// <summary>
/// Nolag library provides a client
/// This client provides a way to connect to Nolag Tunnel
/// Definition
///     Tunnel: is away path way to Nolag.
///     Tunnel manages several topics.
///         Properties
///             -> Topic: A tunnel can have communication with multiple topics
///         Functionalities
///             -> Publish message to topic
///             -> Receive message for topic
///
/// Note: This class will utilize the client to connect with websocket.
/// It has the awareness about topic 
/// </summary>
public interface INolagTunnel
{
     
}