using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RemoteConfigProvider
{
    private const string UrlPropertyName = "URL";

    private Firebase.DependencyStatus _dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    private string _configUrl;
    public void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            _dependencyStatus = task.Result;
            if (_dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + _dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        Dictionary<string, object> defaults = new Dictionary<string, object>();

        defaults.Add("config_test_string", "default local string");
        defaults.Add("config_test_int", 1);
        defaults.Add("config_test_float", 1.0);
        defaults.Add("config_test_bool", false);

        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
          .ContinueWithOnMainThread(task => FetchDataAsync());
    }

    public string GetConfigUrl()
    {
        return _configUrl;
    }

    public Task FetchDataAsync()
    {
        Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    void FetchComplete(Task fetchTask)
    {
        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                .ContinueWithOnMainThread(task => { _configUrl = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(UrlPropertyName).StringValue; });

                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                break;
        }
    }
}
