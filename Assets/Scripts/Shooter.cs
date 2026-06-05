using UnityEngine;


public class Shooter : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position,
                           cam.transform.forward,
                           out hit,
                           range))
        {
            Balloon balloon =
                hit.transform.GetComponent<Balloon>();

            if (balloon != null)
            {
                balloon.Pop();
            }
        }
    }
}