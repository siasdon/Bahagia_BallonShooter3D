using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public int maxBalloons = 20;

    void Start()
    {
        SpawnInitialBalloons();
    }

    void SpawnInitialBalloons()
    {
        for (int i = 0; i < maxBalloons; i++)
        {
            SpawnBalloon();
        }
    }

    public void SpawnBalloon()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-30f, 30f),
            Random.Range(-8f, 3f),
            Random.Range(10f, 60f)
        );

        Instantiate(
            balloonPrefab,
            randomPos,
            Quaternion.identity
        );
    }
}