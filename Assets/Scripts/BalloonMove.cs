using UnityEngine;

public class BalloonMove : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {
        transform.Translate(
            Vector3.up *
            speed *
            Time.deltaTime);
    }
}