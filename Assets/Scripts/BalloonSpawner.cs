/*using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Transform player;

    public int balloonsPerSpawn = 50; // jumlah balon setiap spawn
    public float spawnInterval = 1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBalloon), 1f, spawnInterval);
    }

    void SpawnBalloon()
    {
        Vector3 spawnPos = Vector3.zero;

        int spawnType = Random.Range(0, 4);

        switch (spawnType)
        {
            // Dari kiri
            case 0:
                spawnPos = player.position +
                           player.forward * Random.Range(10f, 30f) +
                           player.right * -20f +
                           Vector3.up * Random.Range(-3f, 5f);
                break;

            // Dari kanan
            case 1:
                spawnPos = player.position +
                           player.forward * Random.Range(10f, 30f) +
                           player.right * 20f +
                           Vector3.up * Random.Range(-3f, 5f);
                break;

            // Dari atas
            case 2:
                spawnPos = player.position +
                           player.forward * Random.Range(10f, 30f) +
                           Vector3.up * 40f;
                break;

            // Dari depan
            case 3:
                spawnPos = player.position +
                           player.forward * Random.Range(15f, 35f) +
                           player.right * Random.Range(-15f, 15f) +
                           Vector3.up * Random.Range(-3f, 5f);
                break;
        }

        Instantiate(balloonPrefab, spawnPos, Quaternion.identity);
    }
}*/

using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public int balloonsPerSpawn = 10;
    public float spawnInterval = 0.5f;
    public int maxBalloons = 100;

    private Vector3 moveDir;
    public float speed;

    void Start()
    {
        moveDir = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-1f, 1f)
        ).normalized;

        speed = Random.Range(1f, 4f);
        InvokeRepeating(nameof(SpawnBalloons), 1f, spawnInterval);
    }

    void Update()
    {
        transform.position +=
            moveDir * speed * Time.deltaTime;
    }


    void SpawnBalloons()
    {
        GameObject[] balloons =
            GameObject.FindGameObjectsWithTag("Balloon");

        if (balloons.Length >= maxBalloons)
            return;

        for (int i = 0; i < balloonsPerSpawn; i++)
        {
            Vector3 randomPos = player.position +
                                new Vector3(
                                    Random.Range(-50f, 50f),  // kiri-kanan
                                    Random.Range(-15f, 20f),  // atas-bawah
                                    Random.Range(10f, 80f));  // depan

            Instantiate(
                balloonPrefab,
                randomPos,
                Random.rotation
            );
        }
    }
}