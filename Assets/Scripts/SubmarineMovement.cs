using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gerakan kiri
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
        }
        // Gerakan kanan
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 5 * Time.deltaTime);
        }
/*
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.back * 5 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }*/
    }
   
}