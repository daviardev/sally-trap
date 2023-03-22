using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class backgroundScroll : MonoBehaviour {
    public Vector2 moventVelocity;
    Vector2 offset;
    
    Rigidbody2D rbPlayer;
    Material material;

    private void Awake() {
        material = GetComponent<SpriteRenderer>().material;
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        offset = (rbPlayer.velocity.x * .1f) * moventVelocity * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}