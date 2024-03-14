using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBalls : MonoBehaviour {
    private Collider2D _ballCollider;
    public int damage;
    public float speed;

    private void Start () { 
        _ballCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScGround component)) {
            switch (component.type) {
                case ScGround.BlockType.semi:
                    Physics2D.IgnoreCollision(collision.collider, _ballCollider, true);
                    break;
                case ScGround.BlockType.breakable:
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                case ScGround.BlockType.normal:
                    Destroy(this.gameObject);
                break;
            }
        }
    }
}
