using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public TextMeshProUGUI HighScoreText;
    private void Start()
    {
        SettingsMenu.GetComponent<Settings>().LoadData();
        if (YGManager.IsAuthorized)
        {
            HighScoreText.text = YandexGame.savesData.HighScore.ToString();
        }
        else
        {
            YGManager.AuthSuccess += AuthSuccess;
        }
    }

    private void AuthSuccess()
    {
        HighScoreText.text = YandexGame.savesData.HighScore.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
