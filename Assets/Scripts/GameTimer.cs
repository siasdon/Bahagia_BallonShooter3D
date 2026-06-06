using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 60f;

    public TMP_Text timerText;
    public GameObject gameOverText;

    private bool gameEnded = false;

    void Start()
    {
        gameOverText.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        gameTime -= Time.deltaTime;

        if (gameTime < 0)
        {
            gameTime = 0;
        }

        /*timerText.text =
            Mathf.CeilToInt(gameTime).ToString();
           */

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text =
            string.Format("{0:00}:{1:00}",
            minutes,
            seconds);


        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;

        gameOverText.SetActive(true);

        Time.timeScale = 0f;
    }
}