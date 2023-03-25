using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;

public class Reloj : MonoBehaviour {
    [SerializeField] int min, seg;
    [SerializeField] TMP_Text text;

    private float remainder;
    private bool inMotion;

    private void Awake() {
        remainder = (min * 60) + seg;
        inMotion = true;
    }

    void Update() {
        if (inMotion) {
            remainder -= Time.deltaTime;
            if (remainder < 1) {
                inMotion = true;
                SceneManager.LoadScene(1);
            }

            int tempMin = Mathf.FloorToInt(remainder / 60);
            int tempSeg = Mathf.FloorToInt(remainder % 60);
            
            text.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
        }
    }
}