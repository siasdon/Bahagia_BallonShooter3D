using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AnimationClip FadeOutClip;
    int indexScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeLoadScene(int NomorScene)
    {
        gameObject.SetActive(true);
        indexScene = NomorScene;
        Invoke("FadeOutCeck", FadeOutClip.length);
        
    }
    void FadeOutCheck() 
    {
        SceneManager.LoadScene(indexScene);
    }
}
