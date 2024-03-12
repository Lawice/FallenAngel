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

    [Header("Bullet Jump")]
    [SerializeField] Transform _shootPoint;
    [SerializeField] GameObject _bulletPrefab;
    public int magazineSize;
    public int bulletLeft;
    public float bulletSpeed;

    [Header("Mouvements")]
    public float speed;
    private float horizontal;

    private void Start(){
        _body = GetComponent<Rigidbody2D>();
        bulletLeft = magazineSize;
    }

    private void Update() {
        if (IsGrounded) { jumpNb = 0; IsAbleToJump = true; bulletLeft = magazineSize; }
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
            if (jumpNb == jumpMax) { Shoot(); }
            
        }

    }

    public void SideMovement(InputAction.CallbackContext ctx) {
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ground")) {IsGrounded = true;}
    }

    private void Shoot() {
        if (bulletLeft > 0){
            if (!IsAbleToJump ) { _body.velocity = new Vector2(_body.velocity.x, 0.5f); }
            GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position,Quaternion.identity);
            Destroy(bullet, 2f);
            bulletLeft--;
        }
    }
}
