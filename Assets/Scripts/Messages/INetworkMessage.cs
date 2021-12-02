public interface INetworkMessage
{
    MessageCode MessageCode { get; }
    string NetworkMessage { get; }
}