using UnityEngine;

public class PagesController : MonoBehaviour
{
    [SerializeField] private UrlProvider _urlProvider;
    [SerializeField] private GameObject _noConnectionPanel;

    private WebViewController _webViewController;

    private void Start()
    {
        _webViewController = new WebViewController();
    }

    public void OpenContentPage()
    {
        string url = _urlProvider.GetUrl();

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _noConnectionPanel.SetActive(true);
            return;
        }

        // Got url from local storage
        if (_urlProvider.IsUrlLocal == true)
        {
            _webViewController.ShowUrlFullScreen(url);
            return;
        }

        // Got url from remote storage
        if (string.IsNullOrEmpty(url) == false)
        {
            _webViewController.ShowUrlFullScreen(url);
        }
        else
        {
            // Show mock
        }
    }
}
