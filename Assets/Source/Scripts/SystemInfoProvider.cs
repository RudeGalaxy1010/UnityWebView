using UnityEngine;

public class SystemInfoProvider : MonoBehaviour
{
    private AndroidJavaObject _pluginInstance;

    public SystemInfoProvider()
    {
        AndroidJavaClass unityClass;
        AndroidJavaObject unityActivity;

        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        _pluginInstance = new AndroidJavaObject("com.emulatordetection.unityplugin.PluginInstance");
        _pluginInstance.CallStatic("ReceiveActivity", unityActivity);
    }

    public bool IsEmulator => _pluginInstance.Call<bool>("IsEmulator");
    public bool HasSimCard => _pluginInstance.Call<bool>("HasSimCard");
    public string MobileOperatorName => _pluginInstance.Call<string>("GetOperatorName");
}
