/*using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;

    public int totalBalloons = 30;

    public Vector3 areaSize = new Vector3(50, 15, 50);

    void Start()
    {
        for (int i = 0; i < totalBalloons; i++)
        {
            SpawnBalloon();
        }
    }

    public void SpawnBalloon()
    {
        Vector3 randomPos =
            new Vector3(
                Random.Range(-areaSize.x, areaSize.x),
                Random.Range(-10f, 5f),
                Random.Range(10f, areaSize.z)
            );

        Instantiate(
            balloonPrefab,
            randomPos,
            Quaternion.identity
        );
    }
}*/