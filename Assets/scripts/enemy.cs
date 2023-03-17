using UnityEngine;

public class enemy : MonoBehaviour {
    float velociyX = 4f;
    float distance = 1f;

    bool isMoving;

    public Transform controllerGround;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        // detect raycast
        RaycastHit2D infoGround = Physics2D.Raycast(controllerGround.position, Vector2.down, distance);

        // add velocity
        rb.velocity = new Vector2(velociyX, rb.velocity.y);

        // rotate when don't detect ground
        if (infoGround == false) {
            Rotate();
        }
    }

    // rotate enemy
    private void Rotate() {
        isMoving = !isMoving;
        transform.eulerAngles = new Vector2(0, transform.eulerAngles.y + 180);
        velociyX *= -1;
    }

    // method collision when the player collision with the enemy
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            foreach (ContactPoint2D point in collision.contacts) {
                if (point.normal.y <= -.1f) {
                    collision.gameObject.GetComponent<move>().Rebounce();
                    Destroy(gameObject);
                    return;
                } else {
                    collision.gameObject.GetComponent<health>().Damage(point.normal);
                }
            }
        }
    }

    // draw raycast
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controllerGround.transform.position, controllerGround.transform.position + Vector3.down * distance);
    }
}