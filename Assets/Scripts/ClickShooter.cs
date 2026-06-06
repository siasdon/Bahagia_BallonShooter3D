using UnityEngine;

public class ClickShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray =
                cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Shoot(hit.transform);
            }
        }
    }

    void Shoot(Transform target)
    {
        Vector3 dir =
        (target.position - firePoint.position).normalized;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(dir));

        bullet.GetComponent<Bullet>()
              .SetTarget(target);

    }


}