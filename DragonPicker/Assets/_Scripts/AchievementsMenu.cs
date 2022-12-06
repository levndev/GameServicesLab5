using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : MonoBehaviour
{
    public GameObject ScrollViewContent;
    public GameObject AchievementPanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var achievement in AchievementManager.Instance.allAchievements.Values.OrderBy(a => a.UniqueID))
        {
            var panel = Instantiate(AchievementPanelPrefab, ScrollViewContent.transform);
            if (AchievementManager.Instance.completedAchievements.ContainsKey(achievement.UniqueID))
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            var icon = panel.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = achievement.Icon;
            var title = panel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            title.text = achievement.Name;
            var text = panel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.text = achievement.Text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
