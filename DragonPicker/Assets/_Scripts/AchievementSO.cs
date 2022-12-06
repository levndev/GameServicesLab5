using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dragon Picker/Achievement")]
[Serializable]
public class AchievementSO : ScriptableObject
{
    public int UniqueID;
    public string Name;
    public string Text;
    public Sprite Icon;
}
