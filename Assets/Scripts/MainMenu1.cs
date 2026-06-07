using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    // Menyimpan referensi ke panel menu utama
    public GameObject menupanel;
    // Menyimpan referensi ke panel informasi / petunjuk permainan
    public GameObject infopanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Dipanggil sekali saat scene pertama kali dijalankan
    private void Start()
    {   
        // Saat game dibuka, tampilkan menu utama
        menupanel.SetActive(true);
        // Sembunyikan panel informasi
        infopanel.SetActive(false);
    }
    // Update dipanggil setiap frame
    // Saat ini belum digunakan
    void Update()
    {
        
    }
    // Fungsi untuk memulai permainan
    // Parameter scenename diisi dengan nama scene tujuan
    public void StartButton(string scenename)
    {   
        // Berpindah ke scene yang dipilih
        SceneManager.LoadScene(scenename);      
    }
    // Fungsi ketika tombol Info ditekan
    public void InfoButton()
    {   
        // Sembunyikan menu utama
        menupanel.SetActive(false);
        // Tampilkan panel informasi
        infopanel.SetActive(true);
    }
    // Fungsi ketika tombol Back ditekan
    public void BackButton()
    {   
        // Tampilkan kembali menu utama
        menupanel.SetActive(true);
        // Sembunyikan panel informasi
        infopanel.SetActive(false);
    }
    // Fungsi ketika tombol Quit ditekan
    public void QuitButton()
    {   
        // Menutup aplikasi
        Application.Quit();
        // Pesan ini hanya terlihat di Console Unity
        // Tidak akan muncul pada game yang sudah di-build
        Debug.Log("Tombol Keluar Telah Ditekan!....");
    }
}
