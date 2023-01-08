using UnityEngine;

public class UrlProvider : MonoBehaviour
{
    private RemoteConfigProvider _remoteConfigProvider;
    private UrlSaver _urlSaver;

    public bool LinkSetup => IsUrlLocal || _remoteConfigProvider.IsLinkLoaded;
    public bool LinkNotLoaded => !IsUrlLocal && _remoteConfigProvider.IsLoadingFailed;
    public bool IsUrlLocal => _urlSaver.HasSavedUrl;

    private void Awake()
    {
        _urlSaver = new UrlSaver();
        _remoteConfigProvider = new RemoteConfigProvider();

        if (IsUrlLocal == false)
        {
            _remoteConfigProvider.Init();
        }
    }

    public string GetUrl()
    {
        string url;

        if (IsUrlLocal)
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
