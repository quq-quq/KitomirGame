using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start() {
        FadeScreen.Instance.OnFadeComplete += FadeScreen_OnFadeComplete;
    }

    private void FadeScreen_OnFadeComplete(object sender, EventArgs e) {
        SceneManager.LoadScene("KitomirHomeScene");
    }

    public static void Play() {
        FadeScreen.Instance.Fade(1.5f, 0);
    }
    
    public static void Quit() {
        Application.Quit();
    }
}
