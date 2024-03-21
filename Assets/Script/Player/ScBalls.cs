using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBalls : MonoBehaviour {
    private Collider2D _ballCollider;
    private Rigidbody2D _ballBody;
    public int damage;
    public float gravity;
    public Vector2 velocity;

    private void Start () { 
        _ballCollider = GetComponent<Collider2D>();
        _ballBody = GetComponent<Rigidbody2D>();
        _ballBody.gravityScale = gravity;
    }

    void FixedUpdate () {
        transform.Translate (velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScGround groundScript)) {
            switch (groundScript.type) {
                case ScGround.BlockType.semi:
                    Physics2D.IgnoreCollision(collision.collider, _ballCollider, true);
                    break;
                case ScGround.BlockType.breakable:
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                case ScGround.BlockType.crate:
                    groundScript.SpawnLoot();
                    Destroy(this.gameObject);
                    break;
                case ScGround.BlockType.normal:
                    Destroy(this.gameObject);
                break;
            }
        }
    }
}
