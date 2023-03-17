using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class restarGame : MonoBehaviour {
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(string name) {
        SceneManager.LoadScene(name);
    }

    public void CloseGame() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}