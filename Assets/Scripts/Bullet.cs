/*using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 80f;

    private Transform target;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(
        transform.position,
        target.position) < 0.5f)
        {
            ScoreManager.instance.AddScore(10);
            Destroy(target.gameObject);
            Destroy(gameObject);
        }

        Vector3 direction =
            (target.position - transform.position).normalized;

        transform.position +=
            direction * speed * Time.deltaTime;

        transform.forward = direction;

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}*/


using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Kecepatan peluru
    public float speed = 1000f;
    // Target yang akan ditembak
    private Transform target;
    // Efek ledakan saat peluru mengenai target
    public GameObject popEffect;

    // Suara ledakan saat peluru mengenai target
    public AudioClip popSound;

    public void SetTarget(Transform targetTransform)
    {
        // Set target untuk peluru
        target = targetTransform;
    }

    void Update()
    {
        // Jika target sudah tidak ada, hancurkan peluru
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Hitung jarak antara peluru dan target
        Vector3 direction =
            (target.position - transform.position).normalized;

        // Gerakkan peluru ke arah target
        transform.position +=
            direction * speed * Time.deltaTime;

        // Arahkan peluru ke target
        transform.forward = direction;

        // Cek jika peluru sudah dekat dengan target
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            // Tambahkan skor jika ScoreManager ada
            if (ScoreManager.instance != null)
            {
                // Tambahkan skor untuk setiap target yang berhasil ditembak
                ScoreManager.instance.AddScore(10);
            }
            else
            {
                // Jika ScoreManager tidak ditemukan, tampilkan pesan error
                Debug.LogError("ScoreManager tidak ditemukan!");
            }
            // Hancurkan target dan peluru
            /*Destroy(target.gameObject);
            Destroy(gameObject);*/

            // Mainkan suara ledakan saat peluru mengenai target
            AudioSource.PlayClipAtPoint(
                popSound,
                target.position
            );

            // Tampilkan efek ledakan saat peluru mengenai target
            Instantiate(
                popEffect,
                target.position,
                Quaternion.identity
            );

            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}