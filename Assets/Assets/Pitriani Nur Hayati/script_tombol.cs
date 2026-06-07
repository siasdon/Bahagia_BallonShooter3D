using UnityEngine;
using UnityEngine.SceneManagement;

public class script_tombol : MonoBehaviour
{

    public GameObject menupanel;
    public GameObject infopanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        menupanel.SetActive(true);
        infopanel.SetActive(false);
    }
    void Update()
    {

    }
    public void StartButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void InfoButton()
    {
        menupanel.SetActive(false);
        infopanel.SetActive(true);
    }
    public void BackButton()
    {
        menupanel.SetActive(true);
        infopanel.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Tombol Keluar Telah Ditekan!....");
    }

    public void BackHome(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
