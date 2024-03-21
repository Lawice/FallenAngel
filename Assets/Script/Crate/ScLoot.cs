using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLoot : MonoBehaviour {
    public enum Type {gem, heart, securityBomb };
    public Type type;
    public ScShoot.GunType weaponType;

    public Sprite gem;
    public Sprite heart;
    public Sprite securityBomb;
    SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        switch (type) {
            case Type.gem:
                _spriteRenderer.sprite = gem;
                break;
            case Type.heart:
                _spriteRenderer.sprite = heart;
                break;
            case Type.securityBomb:
                _spriteRenderer.sprite = securityBomb;
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScShoot shootScript)) {
            ScHeal scoreScript = collision.gameObject.GetComponent<ScHeal>();
            shootScript.gunType = weaponType;
            ScShoot.Instance.UpdateWeapon();
            ScShoot.Instance.ShowDisplay();
            switch (type) {
                case Type.heart:
                    ScHeal.Instance.Heal();
                    break;
                case Type.securityBomb:
                    ScShoot.Instance.AddBomb();
                    break;
                 

            }
            Destroy(this.gameObject);
        }
    }
}
