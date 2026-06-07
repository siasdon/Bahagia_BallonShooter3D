using UnityEngine;

public class BalloonColor : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponentInChildren<Renderer>();

        if (rend != null)
        {
            Material newMat = new Material(rend.material);

            Color[] colors =
            {
                Color.red,
                Color.yellow,
                Color.green,
                Color.blue
            };

            Color randomColor =
                colors[Random.Range(0, colors.Length)];

            newMat.SetColor("_BaseColor", randomColor);

            rend.material = newMat;
        }
    }
}