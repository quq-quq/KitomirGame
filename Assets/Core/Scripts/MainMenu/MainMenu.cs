using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public static void Play() {
        SceneManager.LoadScene("KitomirHomeScene");
    }
    
    public static void Quit() {
        Application.Quit();
    }
}
