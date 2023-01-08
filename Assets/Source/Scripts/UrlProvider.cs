using UnityEngine;

public class UrlProvider : MonoBehaviour
{
    private RemoteConfigProvider _remoteConfigProvider;
    private UrlSaver _urlSaver;

    public bool LinkSetup => !string.IsNullOrEmpty(GetUrl());
    public bool IsUrlLocal => _urlSaver.HasSavedUrl;

    private void Awake()
    {
        _urlSaver = new UrlSaver();
        _remoteConfigProvider = new RemoteConfigProvider();
        _remoteConfigProvider.Init();
    }

    public string GetUrl()
    {
        string url;

        if (_urlSaver.HasSavedUrl)
        {
            url = _urlSaver.LoadUrl();
        }
        else
        {
            url = _remoteConfigProvider.GetConfigUrl();

            if (string.IsNullOrEmpty(url) == false)
            {
                _urlSaver.SaveUrl(url);
            }
        }

        return url;
    }
}
