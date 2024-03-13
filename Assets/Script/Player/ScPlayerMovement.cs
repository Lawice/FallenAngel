using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class ScPlayerMovement : MonoBehaviour {
    private Rigidbody2D _body;

    [Header("Jump")]
    public bool IsAbleToJump;
    public int jumpNb;
    public int jumpMax;
    public float jumpForce;
    private bool IsGrounded;
    private bool IsWalled;

    [Header("Mouvements")]
    public float speed;
    private float horizontal;
    
    public static ScPlayerMovement Instance;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Start(){
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (IsGrounded) { jumpNb = 0; IsAbleToJump = true; ScShoot.Instance.Reload(); }
        if (jumpNb >= jumpMax) { IsAbleToJump = false; }
        _body.velocity = new Vector2(horizontal * speed, _body.velocity.y);
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            if (IsAbleToJump) {
                IsGrounded = false;
                jumpNb++;
                _body.velocity = new Vector2(_body.velocity.x, jumpForce);
            }
            if (jumpNb == jumpMax) { ScShoot.Instance.Shoot(); }
        }
    }

    public void SideMovement(InputAction.CallbackContext ctx) {
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScGround component)) {
            if (component.type != ScGround.BlockType.wall) {
                IsGrounded = true;
            }
            else { 
                IsWalled = true;
            }
        }
    }
}
