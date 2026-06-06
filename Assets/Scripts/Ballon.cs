using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{

    public void Pop()
    {
        ScoreManager.instance.AddScore(10);

        Destroy(gameObject);
    }
}