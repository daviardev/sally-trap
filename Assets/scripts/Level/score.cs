using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class score : MonoBehaviour {
   int Score = 0;
   
   public TMP_Text textScore;

   Dictionary<int, bool> collectedCoins = new Dictionary<int, bool>();

   void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "coin") {
            int coinID = other.gameObject.GetInstanceID();
            if (collectedCoins.ContainsKey(coinID) && collectedCoins[coinID]) {
                return;
            }
            collectedCoins[coinID] = true;
            Score++;
            textScore.text = " X " + Score;
        }
    }
}