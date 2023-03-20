using UnityEngine;

public class levelManager : MonoBehaviour {
    public enum Layers {
        player = 3,
        ground = 6,
        shell = 7,
        grabShell = 8
    }

    void Start() {
        Physics2D.IgnoreLayerCollision((int) Layers.player, (int) Layers.shell, true);
        Physics2D.IgnoreLayerCollision((int) Layers.grabShell, (int) Layers.shell, true);
        Physics2D.IgnoreLayerCollision((int) Layers.grabShell, (int) Layers.ground, true);
    }
}