using UnityEngine;

public class DestroyEffect : MonoBehaviour
{

    //
    void Start()
    {
        Destroy(gameObject, 1f);// Menghancurkan objek ini setelah 1 detik
    }
}