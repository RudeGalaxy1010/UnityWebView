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
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _noConnectionPanel.SetActive(true);
            yield return null;
        }
        else
        {
            _webViewController = new WebViewController();
            yield return new WaitUntil(() => _urlProvider.LinkSetup || _urlProvider.LinkNotLoaded);

            OpenContentPage();
        }
    }

    public void OpenContentPage()
    {
        string url = _urlProvider.GetUrl();

        if (Application.isEditor || _urlProvider.IsUrlLocal == false
            || string.IsNullOrEmpty(url) || IsEmulator())
        {
            SceneManager.LoadScene(MockSceneName);
            return;
        }

        // Got url from local storage
        if (_urlProvider.IsUrlLocal == true)
        {
            _webViewController.ShowUrlFullScreen(url);
            return;
        }

        // Got url from remote storage
        _webViewController.ShowUrlFullScreen(url);
        return;
    }

    private bool IsEmulator()
    {
        AndroidJavaClass osBuild;
        osBuild = new AndroidJavaClass("android.os.Build");
        string fingerPrint = osBuild.GetStatic<string>("FINGERPRINT");
        return fingerPrint.Contains("generic");
    }
}
