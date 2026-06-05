using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;

    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating(
            "SpawnBalloon",
            1,
            spawnRate);
    }

    void SpawnBalloon()
    {
        Vector3 pos =
        new Vector3(
            Random.Range(-8, 8),
            Random.Range(-3, 3),
            Random.Range(5, 20));

        Instantiate(
            balloonPrefab,
            pos,
            Quaternion.identity);
    }
}