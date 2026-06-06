using UnityEngine;

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
}