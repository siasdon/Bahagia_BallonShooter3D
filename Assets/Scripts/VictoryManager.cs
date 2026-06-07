using TMPro;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timeText;

    void Start()
    {
        int score =
            PlayerPrefs.GetInt("FinalScore", 0);

        float remainingTime =
            PlayerPrefs.GetFloat("RemainingTime", 0);

        scoreText.text =
            "Final Score : " + score;

        int minutes =
           Mathf.FloorToInt(remainingTime / 60);

        int seconds =
            Mathf.FloorToInt(remainingTime % 60);

        timeText.text =
            "Time Remaining : " +
            string.Format("{0:00}:{1:00}",
            minutes,
            seconds);

        float timeUsed =
        PlayerPrefs.GetFloat("TimeUsed", 0);

        timeText.text =
            "Time Used : " +
            timeUsed.ToString("F1") +
            " Seconds";
    }
}