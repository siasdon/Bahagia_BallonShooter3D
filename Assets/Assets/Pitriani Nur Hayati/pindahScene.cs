using UnityEngine;
using UnityEngine.SceneManagement;

public class pindahScene : MonoBehaviour
{
    public GameObject MenuHome;
    public GameObject PlayNext;
    public GameObject Retry;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuHome.SetActive(true);
        PlayNext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // memanggil scene Next Level
    public void play(string Level2)
    {
        SceneManager.LoadScene(Level2);
    }

    // Tombol Play Next Level
    public void btn_Play()
    {
        MenuHome.SetActive(false);
        PlayNext.SetActive(true);
    }

    // memanggil scene Main Menu
    public void Menu(string MainMenu1)
    {
        SceneManager.LoadScene(MainMenu1);
    }

    // Tombol Main Menu
    public void btn_Menu()
    {
        MenuHome.SetActive(true);
        PlayNext.SetActive(false);
    }

    // memanggil scene MainGame
    public void retry(string MainGame)
    {
        SceneManager.LoadScene(MainGame);
    }

    // Tombol Main Game Ulang
    public void btn_Retry()
    {
        MenuHome.SetActive(false);
        Retry.SetActive(true);
    }

    // Tombol Exit
    public void ExitGame()
    {
        Application.Quit();
    }
}
