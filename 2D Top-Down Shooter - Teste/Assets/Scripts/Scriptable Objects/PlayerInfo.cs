using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Player Info")]
public class PlayerInfo : ScriptableObject
{
    public string playerName;
    public float gameTime;
    public int score;
    public int enemiesKilled;
}
