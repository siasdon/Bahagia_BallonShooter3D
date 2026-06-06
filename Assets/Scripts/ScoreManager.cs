using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    int score;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        scoreText.text =
        "Score : " + score;
    }
}