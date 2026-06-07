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

    public GameObject balloonPrefab;// Prefab balon yang akan di-spawn
    public Transform player;// Referensi ke transform pemain untuk menentukan posisi spawn balon

    // Pengaturan spawn balon
    [Header("Spawn Settings")]
    public int balloonsPerSpawn = 10;// Jumlah balon yang di-spawn setiap interval
    public float spawnInterval = 0.3f;// Interval waktu antara spawn balon dalam detik
    public int maxBalloons = 150;

    private Vector3 moveDir;// Arah gerakan spawner
    public float speed;// Kecepatan gerakan spawner

    void Start()
    {
        // Jika referensi pemain belum diatur, coba cari objek dengan tag "Player"
        if (player == null)
        {
            GameObject p = GameObject.Find("Player");
            // Jika ditemukan, gunakan transform-nya sebagai referensi pemain
            if (p != null)
            {
                player = p.transform;
            }
        }

        // Tentukan arah gerakan spawner secara acak
        moveDir = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-1f, 1f)
        ).normalized;

        // Tentukan kecepatan gerakan spawner secara acak
        speed = Random.Range(1f, 4f);
        //Debug.Log("Spawner : " + gameObject.name);

        // Mulai memanggil fungsi SpawnBalloons secara berulang setiap spawnInterval detik
        InvokeRepeating(nameof(SpawnBalloons), 1f, spawnInterval);
    }

    void Update()
    {
        // Gerakkan spawner berdasarkan arah dan kecepatan yang telah ditentukan
        transform.position +=
            moveDir * speed * Time.deltaTime;
    }


    void SpawnBalloons()
    {
        // Cari semua objek dengan tag "Balloon" untuk menghitung jumlah balon yang sudah ada
        GameObject[] balloons =
            GameObject.FindGameObjectsWithTag("Balloon");

        // Jika jumlah balon sudah mencapai batas maksimum, hentikan spawn balon baru
        if (balloons.Length >= maxBalloons)
            return;

        /*// Spawn balon sebanyak balloonsPerSpawn dengan posisi acak di sekitar pemain
        for (int i = 0; i < balloonsPerSpawn; i++)
        {
            *//* Vector3 randomPos = player.position +
                                 new Vector3(
                                     Random.Range(-50f, 50f),  // kiri-kanan
                                     Random.Range(-15f, 20f),  // atas-bawah
                                     Random.Range(10f, 80f));  // depan*//*

            // Hitung posisi acak di sekitar pemain dengan mempertimbangkan arah hadap pemain
            Vector3 randomPos = player.position +
                    player.forward * Random.Range(0f, 3.2f) +
                    player.right * Random.Range(-50f, 50f) +
                    Vector3.up * Random.Range(-15f, 20f);

            // Buat balon baru dengan posisi acak dan rotasi acak
            Instantiate(
                balloonPrefab,
                randomPos,
                Random.rotation
            );
        }*/

        // Spawn balon sebanyak balloonsPerSpawn dengan posisi acak di sekitar pemain
        for (int i = 0; i < balloonsPerSpawn; i++)
        {
            Vector3 spawnPos = player.position;

            int side = Random.Range(0, 3);

            switch (side)
            {
                // Kiri
                case 0:
                    spawnPos += new Vector3(
                        -50f,
                        Random.Range(-5f, 15f),
                        Random.Range(15f, 50f));
                    break;

                // Kanan
                case 1:
                    spawnPos += new Vector3(
                        50f,
                        Random.Range(-5f, 15f),
                        Random.Range(15f, 50f));
                    break;

                // Atas
                case 2:
                    spawnPos += new Vector3(
                        Random.Range(-40f, 40f),
                        25f,
                        Random.Range(15f, 50f));
                    break;
            }

            Instantiate(
                balloonPrefab,
                spawnPos,
                Quaternion.identity);
        }
    }
}