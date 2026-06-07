using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern untuk memudahkan akses dari script lain
    public static ScoreManager instance;
    // Referensi
    public TextMeshPro scoreText;
    // Variabel untuk menyimpan skor
    private int score;

    // Inisialisasi singleton
    void Awake()
    {
        // Pastikan hanya ada satu instance ScoreManager
        instance = this;
    }

    
    void Start()
    {
        Debug.Log(scoreText);// Cek apakah scoreText sudah terhubung

        // Pastikan scoreText sudah terhubung di Inspector
        if (scoreText == null)
        {
            Debug.LogError("ScoreText belum terhubung!");
            return;
        }
        // Inisialisasi skor dan tampilkan di UI
        scoreText.text = "Score : 0";
    }

    // Method untuk menambahkan skor
    public void AddScore(int amount)
    {
        // Tambahkan skor dan perbarui UI
        score += amount;
        Debug.Log("Score : " + score);
        scoreText.text = "Score : " + score;// Perbarui teks skor di UI
    }
}