using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public static void Play() {
        Debug.Log("Play");
        ///SceneManager.LoadSceneAsync(1);
    }
    
    public static void Quit() {
        Debug.Log("Quit");
        //Application.Quit();
    }
}
