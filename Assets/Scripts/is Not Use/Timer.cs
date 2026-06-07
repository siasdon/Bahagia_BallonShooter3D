using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 60;

    public TextMeshProUGUI timerText;

    void Update()
    {
        time -= Time.deltaTime;

        timerText.text =
        "Time : " +
        Mathf.Ceil(time);

        if (time <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}