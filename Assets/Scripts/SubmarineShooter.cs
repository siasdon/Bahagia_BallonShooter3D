using UnityEngine;

public class SubmarineShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );

        Debug.Log("TEMBAK!");
    }
}