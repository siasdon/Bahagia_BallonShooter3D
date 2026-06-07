using UnityEngine;

public class ClickShooter : MonoBehaviour
{

    public Camera cam;//variabel untuk kamera
    public GameObject bulletPrefab;//
    public Transform firePoint;//

    void Update()
    {
        // Deteksi klik kiri mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Buat ray dari posisi kamera ke arah klik mouse
            Ray ray =
                cam.ScreenPointToRay(Input.mousePosition);
            // Variabel untuk menyimpan informasi tentang objek yang terkena raycast
            RaycastHit hit;

            // Lakukan raycast dan cek jika mengenai objek
            if (Physics.Raycast(ray, out hit))
            {
                Shoot(hit.transform);
            }
        }
    }

    // Fungsi untuk menembak ke arah target
    void Shoot(Transform target)
    {
        // Hitung arah dari firePoint ke target
        Vector3 dir =
        (target.position - firePoint.position).normalized;

        // Buat peluru dan arahkan ke target
        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(dir));

        // Set target pada script Bullet
        bullet.GetComponent<Bullet>()
              .SetTarget(target);

    }


}