using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ShowDebugInfo : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField, Header("Amount of frames until updated DebugInfo")]
    float updateRate = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(DebugInfo), 0, updateRate);
    }

    void DebugInfo()
    {
        string debugInfo = "";

        debugInfo += "<size=30><b><u>" + Application.productName + "</u></b></size> " + Application.version + "\n";
        debugInfo += "<indent=10px>";
        debugInfo += "FPS: " + Mathf.Round(1.0f / Time.unscaledDeltaTime) + "\n";
        debugInfo += "</indent>";

        debugInfo += "\n<size=25><b><u>SYSTEM INFO</u></b></size>\n";
        debugInfo += "<indent=10px>";

        debugInfo += SystemInfo.deviceName + "\n";
        debugInfo += SystemInfo.operatingSystem + " " + RuntimeInformation.OSArchitecture + "\n";
        debugInfo += SystemInfo.processorModel + "\n";
        debugInfo += SystemInfo.graphicsDeviceName + "\n";
        debugInfo += SystemInfo.systemMemorySize / 1000 + "GB RAM " + "| " + SystemInfo.graphicsMemorySize / 1000 + "GB Available VRAM" + "\n";

        debugInfo += "</indent>";

        text.text = debugInfo;
    }
}
