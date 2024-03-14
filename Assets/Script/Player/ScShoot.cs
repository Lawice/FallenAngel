using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShoot : MonoBehaviour{
    private Rigidbody2D _body;
    public enum GunType {normal,  laser, noppy, oppy, katana, shotgun, burst, machineGun, tripleShot}

    [Header("Gun Stats")]
    public GunType gunType;
    public int magazineSize;
    public int bulletLeft;
    public int chargesUse;
    public int bulletShoot;

    [Header("Bullet Stats")]
    [SerializeField] Transform _shootPoint;
    [SerializeField] float _bulletSpeed;
    [SerializeField] int _bulletDamage;
    [SerializeField] List<GameObject> _bulletPrefab;
    int _bulletID;

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
            GameObject _bullet = Instantiate(_bulletPrefab[_bulletID], _shootPoint.position,Quaternion.identity);
            _bullet.GetComponent<ScBalls>().damage = _bulletDamage;
            _bullet.GetComponent <ScBalls>().speed = _bulletSpeed;
            Destroy(_bullet, 2f);
            bulletLeft -= chargesUse;
        }
    }

    public void Reload() {
        bulletLeft = magazineSize;
    }

    public void Weapon() { 
        switch (gunType) {
            case GunType.normal:
                chargesUse = 1;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 0;
                break;
            case GunType.laser:
                chargesUse = 4;
                bulletShoot = 1;
                _bulletDamage = 8;
                _bulletID = 1;
                break;
            case GunType.noppy:
                chargesUse = 2;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 2;
                break;
            case GunType.oppy:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 3;
                break;
            case GunType.katana:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 4;
                _bulletID = 4;
                break;
            case GunType.shotgun:
                chargesUse = 6;
                bulletShoot = 6;
                _bulletDamage = 4;
                _bulletID = 5;
                break;
            case GunType.burst:
                chargesUse = 4;
                bulletShoot = 2;
                _bulletDamage = 3;
                _bulletID = 6;
                break;
            case GunType.tripleShot:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 7;
                break;
            case GunType.machineGun:
                chargesUse = 3;
                bulletShoot = 4;
                _bulletDamage = 1;
                _bulletID = 8;
                break;
        }
    }
}
