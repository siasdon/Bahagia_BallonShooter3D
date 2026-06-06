using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Transform player;

    public float spawnRadius = 30f;
    public int totalBalloons = 15;

    void Start()
    {
        InvokeRepeating(
            "SpawnBalloon",
            1f,
            2f);
    }

    public void SpawnBalloon()
    {
            Vector3 randomPos =
            player.position +
            player.forward * Random.Range(10f, 30f) +
            player.right * Random.Range(-15f, 15f) +
            Vector3.up * Random.Range(-3f, 3f);

        Instantiate(
            balloonPrefab,
            randomPos,
            Quaternion.identity
        );
    }
}