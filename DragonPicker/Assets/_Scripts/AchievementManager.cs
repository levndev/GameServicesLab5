using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance
    {
        get;
        private set;
    } = null;
    public Dictionary<int, AchievementSO> completedAchievements;
    public Dictionary<int, AchievementSO> allAchievements;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        completedAchievements = new Dictionary<int, AchievementSO>();
        allAchievements = new Dictionary<int, AchievementSO>();
        foreach (var a in Resources.LoadAll<AchievementSO>("Achievements"))
        {
            allAchievements.Add(a.UniqueID, a);
        }
        if (YGManager.IsAuthorized)
        {
            LoadData();
        }
        else
        {
            YGManager.AuthSuccess += LoadData;
        }
    }

    public void CompleteAchievement(int id)
    {
        if (allAchievements.ContainsKey(id))
        {
            if (!completedAchievements.ContainsKey(id))
            {
                completedAchievements.Add(id, allAchievements[id]);
                YandexGame.savesData.Achievements = completedAchievements.Keys.ToArray();
                YandexGame.SaveProgress();
            }
        }
    }

    private void LoadData()
    {
        completedAchievements.Clear();
        if (YandexGame.savesData.Achievements != null)
        {
            foreach (var a in YandexGame.savesData.Achievements)
            {
                if (allAchievements.ContainsKey(a))
                {
                    completedAchievements.Add(a, allAchievements[a]);
                }
            }
        }
    }
}
