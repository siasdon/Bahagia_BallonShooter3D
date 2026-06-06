using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(
            Vector3.forward *
            speed *
            Time.deltaTime
        );
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            ScoreManager.instance.AddScore(10);

            Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }
}