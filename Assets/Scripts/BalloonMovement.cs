using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float speed = 3f;// Kecepatan gerakan balon

    float offset;// Offset acak untuk variasi gerakan

    void Start()
    {
        offset = Random.Range(0f, 100f);// Tentukan kecepatan gerakan balon secara acak
    }

    void Update()
    {
        // Gerakkan balon ke belakang
        transform.position +=
            Vector3.back * speed * Time.deltaTime;
        // Tambahkan gerakan melayang dengan fungsi sinus dan kosinus
        float x =
            Mathf.Sin(Time.time + offset) * 0.02f;
        // Gerakan vertikal lebih kecil untuk efek melayang yang halus
        float y =
            Mathf.Cos(Time.time + offset) * 0.01f;
        // Gabungkan gerakan ke belakang dengan gerakan melayang
        transform.position +=
            new Vector3(x, y, 0);

        // Hapus jika terlalu jauh
        if (transform.position.z < -30f)
        {
            Destroy(gameObject);
        }
    }
}