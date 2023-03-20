using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trigger : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other) {
        var control = transform.parent.GetComponent<move>();

        if (other.gameObject.tag == "shell") {
            var shell = other.gameObject.GetComponent<shell>();

            if (shell.Movent()) {
                var positionFoot = control.transform.Find("ground");

                var homeRay = positionFoot.transform.position;
                var rayUp = new Vector2(positionFoot.transform.position.x, positionFoot.transform.position.y);

                RaycastHit2D[] hits = Physics2D.LinecastAll(homeRay, rayUp);

                foreach (RaycastHit2D hit in hits) {
                    var coll = hit.collider;

                    if (coll != null) {
                        if (coll.gameObject.tag == "shell") {
                            var rigidPlayer = control.GetComponent<Rigidbody2D>();
                            rigidPlayer.velocity = new Vector2(rigidPlayer.velocity.x, 18f);

                            shell.Movent(false);
                            var rb = shell.GetComponent<Rigidbody2D>();
                            rb.velocity = new Vector2();
                        }
                    }
                }
            } else {
                if (Input.GetKey(KeyCode.Z)) {
                    control.GrabShell();
                } else {
                    control.KickShell();
                }
            }
        }
    }
}