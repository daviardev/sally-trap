using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class score : MonoBehaviour {
    int Score = 0;
    public TMP_Text textScore;

    void Start() {
        Score = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "coin") {
            Score++;
            textScore.text = " X " + Score;
        }
    }
}