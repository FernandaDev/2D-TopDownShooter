using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;

    private void Start()
    {
        GameManager.GetInstance().OnTimeChanged += RefreshTimeText;
    }

    private void RefreshTimeText(float newTime)
    {
        int min = (int)newTime / 60;
        int sec = (int)newTime % 60;

        if(timeText)
            timeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
