using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class MessageSender : MonoBehaviour, IReadableMessage
{
    public static string HostName
    {
        get => PlayerPrefs.GetString("HostName", "");
        set => PlayerPrefs.SetString("HostName", value);
    }

    public static int Port
    {
        get => PlayerPrefs.GetInt("Port", -1);
        set => PlayerPrefs.SetInt("Port", Mathf.Clamp(value, 10, 65535));
    }

    private bool HasValidConnectionData => HostName != "" && Port != -1;
    
    [SerializeField] private GameObject[] startData;
    [SerializeField] private GameObject[] updateData;

    private NetworkStream _stream;
    private TcpClient _client = new TcpClient();
    private StringBuilder _stringBuilder = new StringBuilder();
    private INetworkMessage[] _startMessages;
    private INetworkMessage[] _updateMessages;
    
    private void Awake()
    {
        _startMessages = GetMessages(startData);
        _updateMessages = GetMessages(updateData);
    }

    private static INetworkMessage[] GetMessages(GameObject[] objects)
    {
        var result = new INetworkMessage[objects.Length];
        int currentIndex = 0;

        foreach (var targetObject in objects)
        {
            if (targetObject.TryGetComponent(out result[currentIndex]))
                currentIndex++;
        }

        return result;
    }

    private void OnDestroy()
    {
        StopConnection();
    }

    public void SetupConnection()
    {
        if (!_client.Connected && HasValidConnectionData)
        {
            try
            {
                _client = new TcpClient(HostName, Port);
                _stream = _client.GetStream();
            }
        
            catch (Exception exception) { Debug.LogException(exception); }
        }
    }

    public void StopConnection()
    {
        if (_client.Connected)
        {
            try
            {
                _client.Close();
                _client.Dispose();

                _stream.Close();
                _stream.Dispose();
            }
        
            catch (Exception exception) { Debug.LogException(exception); }
        }
    }

    private bool ClientIsConnected() { return _client.Connected; }

    private IEnumerator Start()
    {
        yield return new WaitUntil(ClientIsConnected);
        SendMessages(_startMessages);
    }

    private void Update()
    {
        if (ClientIsConnected())
            SendMessages(_updateMessages);
    }

    private void SendMessages(IEnumerable<INetworkMessage> messages)
    {
        try
        {
            byte[] encodedMessages = GetEncodedMessage(messages);
            int size = encodedMessages.Length;

            _stream.Write(encodedMessages, 0, size);
        }
        
        catch (Exception exception) { Debug.LogException(exception); }
    }

    private byte[] GetEncodedMessage(IEnumerable<INetworkMessage> messages)
    {
        _stringBuilder.Clear();
        
        foreach (var message in messages)
        {
            string encodedMessage = (int) message.MessageCode + message.NetworkMessage + '\n';
            _stringBuilder.Append(encodedMessage);
        }
        
        return Encoding.ASCII.GetBytes(_stringBuilder.ToString());
    }

    // IReadableMessage Implementation
    
    public string ReadableMessage
    {
        get
        {
            string clientStatus = ClientIsConnected() ? "Connected" : "Disconnected";
            return $"Client Status: {clientStatus}";
        }
    }
}