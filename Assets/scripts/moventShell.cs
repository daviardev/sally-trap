using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moventShell : MonoBehaviour {
    float impulse;
    float velocityX;
    float aceleration = 100f;
    float velocityMax = 14f;
    
    float raycastLengh = .99f;
    float raycastSize = .4f;

    bool isMovent = false;

    public Animator anim;

    public bool Movents() {
        return this.isMovent;
    }

    public void Movents(bool movent) {
        this.isMovent = movent;
    }

    void Start() {
        anim.GetComponent<Animator>();
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
                isMovent = true;
                impulse = velocityX;
            } else {
                isMovent = false;
            }

            anim.SetBool("isMoving", isMovent);

            Vector3 velocity = rigidShell.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -velocityMax, velocityMax);
            rigidShell.velocity = velocity;

            var ray = transform.Find("ray");

            var homeRay = new Vector2(ray.transform.position.x, ray.transform.position.y + raycastSize);

            var rayLeft = new Vector2(ray.transform.position.x - raycastLengh, ray.transform.position.y + raycastSize);
            var rayRight = new Vector2(ray.transform.position.x + raycastLengh, ray.transform.position.y + raycastSize);

            RaycastHit2D[] hitLeft = Physics2D.LinecastAll(homeRay, rayLeft);
            RaycastHit2D[] hitRight = Physics2D.LinecastAll(homeRay, rayRight);

            Debug.DrawLine(homeRay, rayLeft, Color.red);
            Debug.DrawLine(homeRay, rayRight, Color.red);

            foreach (RaycastHit2D hit in hitLeft) {
                var coll = hit.collider;

                if (coll != null) {
                    if (coll.gameObject.tag == "wall") {
                        rigidShell.velocity = new Vector2(Mathf.Abs(rigidShell.velocity.x), rigidShell.velocity.y);
                        rigidShell.AddForce(new Vector2(velocityMax, 0));
                    }
                }
            }

            foreach (RaycastHit2D hit in hitRight) {
                var coll = hit.collider;

                if (coll != null) {
                    if (coll.gameObject.tag == "wall") {
                        rigidShell.velocity = new Vector2(-Mathf.Abs(rigidShell.velocity.x), rigidShell.velocity.y);
                        rigidShell.AddForce(new Vector2(-velocityMax, 0));
                    }
                }
            }
        }
    }
}