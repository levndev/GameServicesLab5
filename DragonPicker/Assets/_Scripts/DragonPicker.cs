using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;
using UnityEngine.SceneManagement;
using System.Linq;

public class DragonPicker : MonoBehaviour
{
    public GameObject EnergyShieldPrefab;
    public int EnergyShieldAmount = 3;
    public float EnegyShieldBottomY = -6;
    public float EnegyShieldRadius = 1.5f;
    public List<GameObject> shieldList;
    private int score;
    public TextMeshProUGUI scoreGT;
    public TextMeshProUGUI healthText;
    private int healthLeft;
    private bool paused = false;
    public GameObject PausePanel;
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI HighScoreText;
    void Start()
    {
        var scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = $"Score: 0";
        shieldList = new List<GameObject>();
        for (var i = 1; i <= EnergyShieldAmount; i++)
        {
            var tShieldGo = Instantiate(EnergyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, EnegyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1 * i, 1 * i, 1 * i);
            tShieldGo.GetComponent<EnergyShield>().EggCaught += DragonEggCaught;
            shieldList.Add(tShieldGo);
        }
        healthLeft = EnergyShieldAmount;
        healthText.text = "HP: " + healthLeft.ToString();
        if (YGManager.IsAuthorized)
        {
            PlayerNameText.text = YandexGame.playerName;
            HighScoreText.text = $"High score: {YandexGame.savesData.HighScore}";
        }
        else
        {
            PlayerNameText.text = "Anon";
            HighScoreText.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                TogglePause();
            Quit();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
    }

    public void DragonEggCaught()
    {
        score++;
        if (score >= 20)
        {
            AchievementManager.Instance.CompleteAchievement(2);
        }
        scoreGT.text = $"Score: {score}";
    }

    public void DragonEggDestroyed()
    {
        var tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (var egg in tDragonEggArray)
        {
            Destroy(egg);
        }
        var shield = shieldList.Last();
        shieldList.Remove(shield);
        Destroy(shield);
        healthLeft--;
        healthText.text = "HP: " + healthLeft.ToString();
        if (shieldList.Count == 0)
        {
            AchievementManager.Instance.CompleteAchievement(1);
            Quit();
        }
    }

    private void Quit()
    {
        if (score > YandexGame.savesData.HighScore)
        {
            if (YandexGame.savesData.HighScore != 0)
            {
                AchievementManager.Instance.CompleteAchievement(0);
            }
            YandexGame.savesData.HighScore = score;
            YandexGame.SaveProgress();
            YandexGame.NewLeaderboardScores("TOPPlayerScore", score);
        }
        SceneManager.LoadScene("_0Scene");
    }
}
