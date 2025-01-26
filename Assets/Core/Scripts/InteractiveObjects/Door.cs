using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractiveObject {
    
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float timeInDarknessDuration = 0f;

    private void Start() {
        FadeScreen.Instance.OnFadeComplete += FadeScreen_OnFadeComplete;
        IsInteractable = ConfigGameManager.Instance.IsDoorInteractable(sceneToLoad);
    }

    private void FadeScreen_OnFadeComplete(object sender, EventArgs e) {
        if (Player.Instance.IsItemSelected(this)) {
            //ConfigGameManager.Instance.SetPlayerPosition(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public override void Interact() {
        FadeScreen.Instance.Fade(1.5f, timeInDarknessDuration);
    }
}
