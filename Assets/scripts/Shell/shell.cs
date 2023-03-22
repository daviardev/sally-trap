using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shell : MonoBehaviour {
    float impulse;
    float velocityX;

    float velocityMax = 16f;
    float aceleration = 300f;

    float sizeRay = .4f;
    float lenghtRay = .99f;

    bool isMoving = false;

    public Animator anim;

    void Start() {
        anim.GetComponent<Animator>();
    }

    public bool Movent() {
        return this.isMoving;
    }

    public void Movent(bool movent) {
        this.isMoving = movent;
    }

    void FixedUpdate() {
        var rigidShell = GetComponent<Rigidbody2D>();

        if (rigidShell != null) {
            velocityX = rigidShell.velocity.x;

            if (velocityX < 0) {
                rigidShell.AddForce(new Vector2(-aceleration, 0));
            } else if (velocityX > 0) {
                rigidShell.AddForce(new Vector2(aceleration, 0));
            }

            if (velocityX != 0) {
                isMoving = true;
                impulse = velocityX;
            } else {
                isMoving = false;
            }

            anim.SetBool("isMoving", isMoving);

            Vector3 vel = rigidShell.velocity;
            vel.x = Mathf.Clamp(vel.x, -velocityMax, velocityMax);

            rigidShell.velocity = vel;

            var ray = transform.Find("ray");

            var homeRay = new Vector2(ray.transform.position.x, ray.transform.position.y + sizeRay);

            var rightRay = new Vector2(ray.transform.position.x + lenghtRay, ray.transform.position.y + sizeRay);
            var leftRay = new Vector2(ray.transform.position.x - lenghtRay, ray.transform.position.y + sizeRay);

            RaycastHit2D[] rightHit = Physics2D.LinecastAll(homeRay, rightRay);
            RaycastHit2D[] leftHit = Physics2D.LinecastAll(homeRay, leftRay);

            Debug.DrawLine(homeRay, rightRay, Color.red);
            Debug.DrawLine(homeRay, leftRay, Color.red);

            foreach (RaycastHit2D hit in rightHit) {
                var coll = hit.collider;

                if (coll != null) {
                    if (coll.gameObject.tag == "wall") {
                        rigidShell.velocity = new Vector2(-Mathf.Abs(rigidShell.velocity.x), rigidShell.velocity.y);
                        rigidShell.AddForce(new Vector2(-velocityMax, 0));
                    }
                }
            }

            foreach (RaycastHit2D hit in leftHit) {
                var coll = hit.collider;

                if (coll != null) {
                    if (coll.gameObject.tag == "wall") {
                        rigidShell.velocity = new Vector2(Mathf.Abs(rigidShell.velocity.x), rigidShell.velocity.y);
                        rigidShell.AddForce(new Vector2(velocityMax, 0));
                    }
                }
            }
        }
    }
}
