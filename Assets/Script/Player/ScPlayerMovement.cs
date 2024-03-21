using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class ScPlayerMovement : MonoBehaviour {
    private Rigidbody2D _body;
    private Transform _transform;

    [Header("~~~~~~Jump~~~~~~")]
    public bool IsAbleToJump;
    public int jumpNb;
    public int jumpMax;
    public float jumpForce;
    private bool IsGrounded;
    private bool IsWalled;

    [Header("~~~~~~Mouvements~~~~~~")]
    public float speed;
    private float _horizontal;
    public enum Facing { Left, Right};
    public Facing facing = Facing.Right;
    
    public static ScPlayerMovement Instance;
    private SpriteRenderer _spriteRenderer;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Start(){
        _transform = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (IsGrounded) { jumpNb = 0; IsAbleToJump = true; ScShoot.Instance.Reload(); }
        if (IsWalled) { jumpNb = 0; IsAbleToJump = true; }
        if (jumpNb >= jumpMax) { IsAbleToJump = false; }
        _body.velocity = new Vector2(_horizontal * speed, _body.velocity.y);
        if (_horizontal < 0){
            _spriteRenderer.flipX = true;
            facing = Facing.Left;
        }
        else if (_horizontal > 0) {
            _spriteRenderer.flipX = false;
            facing = Facing.Right;
        }
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            if (IsAbleToJump) {
                IsGrounded = false;
                IsWalled = false;
                jumpNb++;
                _body.velocity = new Vector2(_body.velocity.x, jumpForce);
            }
            if (jumpNb == jumpMax) { ScShoot.Instance.Shoot(); }
        }
    }

    public void SideMovement(InputAction.CallbackContext ctx) {
        _horizontal = ctx.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScGround component)) {
            if (component.type != ScGround.BlockType.wall) {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(_transform.position.x, _transform.position.y+ 1), Vector2.up, 0.5f);
                if(hit.collider == null) { IsGrounded = true; }
            }
            else { 
                IsWalled = true;
            }
        }
    }

}
