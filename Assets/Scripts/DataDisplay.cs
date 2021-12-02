using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject readableData;
    [SerializeField] private Text displayText;

    private IReadableMessage _readableMessage;
    
    private void Start()
    {
        readableData.TryGetComponent(out _readableMessage);
    }

    private void Update()
    {
        if (_readableMessage != null)
            displayText.text = _readableMessage.ReadableMessage;
    }
}