using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    private const string MockSceneName = "MockScene";

    [SerializeField] private UrlProvider _urlProvider;
    [SerializeField] private GameObject _noConnectionPanel;

    private WebViewController _webViewController;

    private IEnumerator Start()
    {
        _webViewController = new WebViewController();

        yield return new WaitUntil(() => _urlProvider.LinkSetup);

        OpenContentPage();
    }

    public void OpenContentPage()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(MockSceneName);
        return;
#endif
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
            SceneManager.LoadScene(MockSceneName);
        }
    }
}
