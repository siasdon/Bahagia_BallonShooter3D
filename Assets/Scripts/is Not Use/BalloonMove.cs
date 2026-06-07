using UnityEngine;

public class BalloonMove : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 3f;

    void Start()
    {
        int moveType = Random.Range(0, 3);

        switch (moveType)
        {
            // Bergerak dari kiri ke kanan
            case 0:
                moveDirection = Vector3.right;
                break;

            // Bergerak dari kanan ke kiri
            case 1:
                moveDirection = Vector3.left;
                break;

            // Jatuh dari atas
            case 2:
                moveDirection = Vector3.down;
                break;
        }

        speed = Random.Range(2f, 5f);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}