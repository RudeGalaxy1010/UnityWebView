using UnityEngine;

public class UrlSaver
{
    private const string Key = "URL";

    public bool HasSavedUrl => PlayerPrefs.HasKey(Key);

    public void SaveUrl(string url)
    {
        PlayerPrefs.SetString(Key, url);
        PlayerPrefs.Save();
    }

    public string LoadUrl()
    {
        return PlayerPrefs.GetString(Key, "");
    }
}
