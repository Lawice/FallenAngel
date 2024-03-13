using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGround : MonoBehaviour {
    public enum BlockType { normal, semi, breakable, wall}
    public BlockType type;
    SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        switch (type) {
            case BlockType.normal:
                spriteRenderer.color = Color.green;
                break;
            case BlockType.semi:
                spriteRenderer.color = new Color(255f,106f, 0f);
                break;
            case BlockType.breakable:
                spriteRenderer.color = Color.cyan;
                break;
            case BlockType.wall:
                spriteRenderer.color = Color.white;
                break;

        }
    }
}
