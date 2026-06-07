using UnityEngine;

public class Balloon : MonoBehaviour
{
    // Fungsi untuk menghancurkan balon dan menambahkan skor
    public void Pop()
    {
        // Tambahkan skor ke ScoreManager
        ScoreManager.instance.AddScore(10);
        // Hancurkan balon
        Destroy(gameObject);
    }
}