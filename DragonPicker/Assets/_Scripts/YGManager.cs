using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class YGManager : MonoBehaviour
{
    public static Action AuthSuccess;
    public static bool IsAuthorized
    {
        get;
        private set;
    } = false;
    private YandexGame YGInstance;
    private void Awake()
    {
        YandexGame.GetDataEvent += SdkDataReceived;
        SceneManager.sceneLoaded += SceneLoaded;
        YGInstance = GameObject.Find("YandexGame").GetComponent<YandexGame>();
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        YGInstance = GameObject.Find("YandexGame").GetComponent<YandexGame>();
    }
    private void SdkDataReceived()
    {
        if (YandexGame.auth)
        {
            Debug.Log($"User authorized: {YandexGame.playerName}");
            AuthSuccess?.Invoke();
            IsAuthorized = true;
        }
        else
        {
            Debug.Log("User not authorized, invoking auth dialog");
            YandexGame.AuthDialog();
        }
    }
}
