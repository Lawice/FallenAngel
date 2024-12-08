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
        switch (weaponType) { 
            case ScShoot.GunType.normal:
                _spriteRenderer.color = Color.cyan;
                break;
            case ScShoot.GunType.laser:
                _spriteRenderer.color = Color.gray;
                break;
            case ScShoot.GunType.noppy:
                _spriteRenderer.color = Color.green;
                break;
            case ScShoot.GunType.oppy:
                _spriteRenderer.color = new Color32(255,106,0,255);
                break;
            case ScShoot.GunType.katana:
                _spriteRenderer.color = new Color32(178,0,255,255);
                break;
            case ScShoot.GunType.shotgun:
                _spriteRenderer.color = Color.yellow;
                break;
            case ScShoot.GunType.burst:
                _spriteRenderer.color = Color.red;
                break;
            case ScShoot.GunType.machineGun:
                _spriteRenderer.color = Color.magenta; 
                break;
            case ScShoot.GunType.tripleShot:
                _spriteRenderer.color = Color.blue;
                break;
        }

         switch (type) {
            case Type.gem:
                _spriteRenderer.sprite = gem;
                break;
            case Type.heart:
                _spriteRenderer.sprite = heart;
                break;
            case Type.securityBomb:
                _spriteRenderer.sprite = securityBomb;
                _spriteRenderer.color = Color.white;
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScShoot shootScript)) {
            ScHeal scoreScript = collision.gameObject.GetComponent<ScHeal>();
            switch (type) {
                case Type.heart:
                    shootScript.gunType = weaponType;
                    ScHeal.Instance.Heal();
                    ScShoot.Instance.UpdateWeapon();
                    ScShoot.Instance.Reload();
                    ScShoot.Instance.ShowDisplay();
                    break;
                case Type.securityBomb:
                    ScShoot.Instance.AddBomb();
                    break;
                case Type.gem:
                    shootScript.gunType = weaponType;
                    ScShoot.Instance.UpdateWeapon();
                    ScShoot.Instance.Reload();
                    ScShoot.Instance.ShowDisplay();
                    break;
            }
            Destroy(this.gameObject);
        }
        
    }
}
