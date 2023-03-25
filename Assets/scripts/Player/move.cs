using UnityEngine;

public class move : MonoBehaviour {

    // controller direction
    float directionX;
    Vector2 direction;

    // controller velocity
    float velocityX = 14f;
    float jumpForce = 12f;

    // back
    public bool canMove = true;
    public Vector2 velocityBack;

    // coyote time
    bool isCoyote = false;
    float timeCoyote;
    float timeCoyoteTime = .1f;

    // shell
    bool isGrab;
    GameObject shell;
    float kickForce = 700f;
    public Transform hand;
    public GameObject shellObject;

    // ground
    bool isGround;
    float ratioFoot = .03f;

    // dead
    public static bool dead = false;

    // rebouce
    float rebounceVelocity = 16f;

    // components
    public Animator anim;
    public Transform foot;
    public LayerMask ground;
    public GameObject player;
    public Rigidbody2D rb;


    void Start() {
        rb.GetComponent<Rigidbody2D>();
    }

    public void KickShell() {
        var rigidShell = shellObject.GetComponent<Rigidbody2D>();
        var rb = GetComponent<Rigidbody2D>();
        var scaleLocal = rb.transform.localScale;

        isGrab = false;

        rigidShell.AddForce(new Vector2(kickForce * scaleLocal.x, 0));
    }

    public void GrabShell() {
        if (shell == null) {
            shell = shellObject;

            shell.transform.parent = this.transform;
            var positionHand = hand.transform.localPosition;
            isGrab = true;

            shell.transform.localPosition = new Vector2(positionHand.x * hand.localScale.x, positionHand.y);

            anim.SetBool("isHolding", true);
            var rigidShell = shellObject.GetComponent<Rigidbody2D>();
            Object.Destroy(rigidShell);

            shell.layer = (int) levelManager.Layers.grabShell;
        }
    }

    public void DropShell() {
        if (shell != null) {
            shell.transform.parent = null;
            shell.gameObject.AddComponent<Rigidbody2D>();

            var rigidShell = shell.GetComponent<Rigidbody2D>();
            rigidShell.freezeRotation = true;
            rigidShell.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rigidShell.gravityScale = 3;

            KickShell();

            shell.layer = (int) levelManager.Layers.shell;
            anim.SetBool("isHolding", false);
            isGrab = false;
            shell = null;
        }
    }

    void FixedUpdate() {
        // Control movement of the character
        float movementX = .0f;

        directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Jump");
        direction = new Vector2(directionX, directionY);

        movementX = directionX * velocityX;

        // Detect the ground
        isGround = Physics2D.OverlapCircle(foot.position, ratioFoot, ground);

        anim.SetBool("isGround", isGround ? true : false);
        
        if (canMove) Move();

        if (dead) Dead();

        if (shell != null && !Input.GetKey(KeyCode.Z)) DropShell();

        // Better jump
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (3f - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 & !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (4.5f - 1) * Time.deltaTime;
        }

        if (Input.GetButton("Jump") && (isGround || isCoyote)) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += direction * jumpForce;
            anim.SetBool("isGround", false);
        }

        anim.SetBool("Fall", rb.velocity.y <= 0 ? true : false);

        // Coyote time
        if (isGround) {
            isCoyote = true;
            timeCoyote = 0;
            anim.SetBool("Fall", false);
        }

        if (!isGround && isCoyote) {
            timeCoyote += Time.deltaTime;
            if (timeCoyote > timeCoyoteTime) isCoyote = false;
        }
    }

    // add rebounce when the player is hit
    public void Rebounce() {
        rb.velocity = new Vector2(rb.velocity.x, rebounceVelocity);
    }
    // add back the player when the enemy hit he
    public void Back(Vector2 pointHit) {
        rb.velocity = new Vector2(-velocityBack.x * pointHit.x, velocityBack.y);
    }
    // move script
    private void Move() {
        // Detect the direction
        if (directionX > 0) {
            rb.velocity = (new Vector2(direction.x * velocityX, rb.velocity.y));
            player.transform.localScale = new Vector2(1, 1);
        } else if (directionX < 0) {
            rb.velocity = (new Vector2(direction.x * velocityX, rb.velocity.y));
            player.transform.localScale = new Vector2(-1, 1);
        }
        // stop anim of wall
        anim.SetFloat("velX", directionX != 0 && isGround ? 1 : 0);
    }

    public void Dead() {
        dead = true;
        Destroy(gameObject);
    }
}