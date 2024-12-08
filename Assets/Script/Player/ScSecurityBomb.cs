using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSecurityBomb : MonoBehaviour {
    public float bombRadius = 2f;

    private void OnEnable() {
        Collider2D[] blocks = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        foreach (Collider2D blockHit in blocks) {
            if (blockHit.gameObject.TryGetComponent(out ScGround groundScipt)) {
                if (groundScipt.type != ScGround.BlockType.wall ) {
                    Destroy(blockHit.gameObject);
                }
            }
        }
    }

    private void Start() {
        Destroy(this.gameObject);
    }
}
