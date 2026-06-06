using UnityEngine;

public class BallonFloat : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.position +=
            new Vector3(
                Mathf.Sin(Time.time) * 0.001f,
                Mathf.Cos(Time.time) * 0.001f,
                0
            );

        transform.Rotate(
            0,
            speed,
            0
        );
    }
}