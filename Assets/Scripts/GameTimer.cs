using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameTimer : MonoBehaviour
{
    // Durasi permainan dalam detik
    public float gameTime = 60f;

    // Referensi ke UI Text untuk menampilkan timer`
    public TMP_Text timerText;
    // Referensi ke UI Text untuk menampilkan "Game Over"
    public GameObject gameOverText;

    // Variabel untuk menandai apakah permainan sudah berakhir
    private bool gameEnded = false;

    void Start()
    {
        // game over text disembunyikan saat permainan dimulai
        gameOverText.SetActive(false);
    }

    void Update()
    {
        // Jika permainan sudah berakhir, tidak perlu update timer
        if (gameEnded) return;

        // Kurangi waktu permainan dengan waktu yang telah berlalu sejak frame terakhir
        gameTime -= Time.deltaTime;

        // Pastikan waktu tidak menjadi negatif
        if (gameTime < 0)
        {
            gameTime = 0;
        }

        /*timerText.text =
            Mathf.CeilToInt(gameTime).ToString();
           */

        //variabel untuk waktu menit dan detik
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        // Format timer text dengan format "Time : MM:SS"
        timerText.text = "Time : " +
            string.Format("{0:00}:{1:00}",
            minutes,
            seconds);

        // Jika waktu habis, akhiri permainan
        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    // Fungsi untuk mengakhiri permainan
    void EndGame()
    {
        gameEnded = true;

        gameOverText.SetActive(true);

        Time.timeScale = 0f;
    }
}