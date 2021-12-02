using UnityEngine;
using UnityEngine.UI;

public class NetworkInputField : MonoBehaviour
{
    [SerializeField] private InputField hostNameField;
    [SerializeField] private InputField portField;

    private void Awake()
    {
        hostNameField.text = MessageSender.HostName;
        hostNameField.onValueChanged.AddListener(UpdateHost);

        portField.text = MessageSender.Port == -1 ? "" : MessageSender.Port.ToString();
        portField.onValueChanged.AddListener(UpdatePort);
    }

    private static void UpdateHost(string hostName)
    {
        MessageSender.HostName = hostName;
    }

    private static void UpdatePort(string portName)
    {
        MessageSender.Port = int.Parse(portName);
    }
}