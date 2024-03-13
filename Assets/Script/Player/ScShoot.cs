using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShoot : MonoBehaviour{
    private Rigidbody2D _body;
    public enum GunType {normal, shotgun, laser, burst, katana, multishot, tripleshot, noppy, oppy}

    [Header("Bullet Jump")]
    [SerializeField] Transform _shootPoint;
    [SerializeField] GameObject _bulletPrefab;

    [Header("Gun Stats")]
    public int magazineSize;
    public int bulletLeft;
    public int bulletShoot;
    public float bulletSpeed;
    public int bulletDamage;
    public GunType gunType;

    public static ScShoot Instance;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        bulletLeft = magazineSize;
    }

    public void Shoot() {
        Weapon();
        if (bulletLeft > 0){
            if (!ScPlayerMovement.Instance.IsAbleToJump ) { _body.velocity = new Vector2(_body.velocity.x, 0.5f); }
            GameObject _bullet = Instantiate(_bulletPrefab, _shootPoint.position,Quaternion.identity);
            Destroy(_bullet, 2f);
            bulletLeft -= bulletShoot;
        }
    }

    public void Reload() {
        bulletLeft = magazineSize;
    }

    public void Weapon() { 
        switch (gunType) {
            case GunType.normal:
                bulletShoot = 1;
                bulletDamage = 2;
                break;
            case GunType.shotgun:
                bulletShoot = 6;
                bulletDamage = 1;
                break;
            case GunType.laser:
                bulletShoot = 4;
                bulletDamage = 8;
                break;
            case GunType.burst:
                bulletShoot = 3;
                bulletDamage = 3;
                break;
            case GunType.tripleshot:
                bulletShoot = 3;
                bulletDamage = 2;
                break;
            case GunType.katana:
                bulletShoot = 2;
                bulletDamage = 4;
                break;
            case GunType.multishot:
                bulletShoot = 3;
                bulletDamage = 1;
                break;
            case GunType.noppy:
                bulletShoot = 2;
                bulletDamage = 2;
                break;
            case GunType.oppy:
                bulletShoot = 3;
                bulletDamage = 2;
                break;
        }
    }
}
