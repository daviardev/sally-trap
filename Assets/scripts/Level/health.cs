using System;
using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
    int life;
    float timeAwait = .5f;

    public GameObject[] hearts;
    public event EventHandler diePlayer;
    
    move mv;

    void Start() {
        life = hearts.Length;
        mv = GetComponent<move>();
    }

    public void Damage(Vector2 position) {
        shakeCamera.Instance.MoveCamera(5, 5, .5f);
        life--;
        StartCoroutine(LoseControl());
        mv.Back(position);

        // bug delete two lifes in one, fix: create two hearts for resolve this problem.
        if (life < 1) {
            Destroy(hearts[0].gameObject);
            diePlayer?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        } else if (life < 2) {
            Destroy(hearts[1].gameObject);
        } else if (life < 3) {
            Destroy(hearts[2].gameObject);
        } else if (life < 4) {
            Destroy(hearts[3].gameObject);
        } else if (life < 5) {
            Destroy(hearts[4].gameObject);
        }
    }
    // don't can move player
    private IEnumerator LoseControl() {
        mv.canMove = false;
        yield return new WaitForSeconds(timeAwait);
        mv.canMove = true;
    }
}