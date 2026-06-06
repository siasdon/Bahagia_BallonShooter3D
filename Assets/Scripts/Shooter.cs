using UnityEngine;


public class Shooter : MonoBehaviour
{
    //variabel untuk kamera dan jarak tembak
    public Camera cam;
    public float range = 100f;

    
    void Update()
    {
        //cek input mouse kiri untuk menembak
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //buat variabel untuk menyimpan informasi tentang objek yang terkena tembakan
        RaycastHit hit;

        //lakukan raycast dari posisi kamera ke arah yang dihadapinya dengan jarak tertentu
        if (Physics.Raycast(cam.transform.position,
                           cam.transform.forward,
                           out hit,
                           range))
        {
            //cek jika objek yang terkena tembakan memiliki tag "Balloon"
            Balloon balloon =
                hit.transform.GetComponent<Balloon>();
            //jika ada, panggil fungsi Pop() pada objek tersebut
            if (balloon != null)
            {
                balloon.Pop();
            }
        }
    }
}