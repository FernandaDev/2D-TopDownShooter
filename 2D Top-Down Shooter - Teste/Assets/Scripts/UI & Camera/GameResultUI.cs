using UnityEngine;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text killsText;

    private void Start()
    {
        int min = (int)playerInfo.gameTime / 60;
        int sec = (int)playerInfo.gameTime % 60;

        if(timeText)
            timeText.text = min.ToString("00") + ":" + sec.ToString("00");

        if(scoreText)
            scoreText.text = playerInfo.score.ToString();

        if(killsText)
            killsText.text = playerInfo.enemiesKilled.ToString();
    }
}
