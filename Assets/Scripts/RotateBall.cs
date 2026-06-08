using UnityEngine;

public class RotateBall : MonoBehaviour
{
    public float speed = 100f;

    void Update()
    {
        transform.Rotate(
            0,
            0,
            speed * Time.deltaTime
        );
    }
}