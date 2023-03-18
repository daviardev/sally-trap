using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour {
    public GameObject menuGameOver;
    private health h;

    private void Start() {
        h = GameObject.FindGameObjectWithTag("Player").GetComponent<health>();
        h.diePlayer += ActivateMenu;
    }

    private void ActivateMenu(object sender, EventArgs e) {
        menuGameOver.SetActive(true);
    }

    public void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackMenu(string name) {
        SceneManager.LoadScene(name);
    }
}