using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public TMP_Text EndScoreText;

    void Start()
    {
        int score =
            PlayerPrefs.GetInt(
                "FinalScore",
                0);

        EndScoreText.text =
            "Your Score : " + score;
    }
}