using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
    public enum Layers {
        ground = 6,
        player = 3,
        shell = 7,
        grabShell = 8
    }

    private void Start() {
        Physics2D.IgnoreLayerCollision((int) Layers.player, (int) Layers.shell, true);
        Physics2D.IgnoreLayerCollision((int) Layers.grabShell, (int) Layers.shell, true);
        Physics2D.IgnoreLayerCollision((int) Layers.grabShell, (int) Layers.ground, true);
    }
}