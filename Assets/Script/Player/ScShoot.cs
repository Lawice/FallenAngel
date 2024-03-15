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
    public int bulletShooted = 1;

    [Header("Bullet Stats")]
    [SerializeField] Transform _shootPoint;
    [SerializeField] float _ballGravityScale;
    [SerializeField] float _ballSpeed;
    [SerializeField] int _bulletDamage;
    [SerializeField] List<GameObject> _bulletPrefab;
    int _bulletID;
    [SerializeField] int _horizontal;

    public static ScShoot Instance;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        bulletLeft = magazineSize;
        _horizontal = 1;
        _ballGravityScale = 1;
    }

    public void Shoot() {
        if (bulletShooted == 1) { Weapon(); }
        if (bulletLeft > 0){
            if (!ScPlayerMovement.Instance.IsAbleToJump) { _body.velocity = new Vector2(_body.velocity.x, 0.5f); }
            if (gunType != GunType.laser) {
                GameObject _bullet = Instantiate(_bulletPrefab[_bulletID], _shootPoint.position, Quaternion.identity);
                ScBalls _scBalls = _bullet.GetComponent<ScBalls>();
                _scBalls.damage = _bulletDamage;
                if (gunType == GunType.noppy) {
                    if (ScPlayerMovement.Instance.facing == ScPlayerMovement.Facing.Right) {
                        _horizontal = 1;
                    } 
                    else {
                        _horizontal = -1;
                    }
                    float _bulletAngle = Mathf.Deg2Rad * 60f;
                    float _bulletVelocityX = Mathf.Cos(_bulletAngle) * _ballSpeed;
                    float _bulletVelocityY = -Mathf.Sin(_bulletAngle) * _ballSpeed;
                    _scBalls.velocity = new Vector2(_bulletVelocityX*_horizontal, _bulletVelocityY);
                }
                else {
                    _scBalls.gravity = _ballGravityScale;
                }
                if((gunType == GunType.burst  || gunType == GunType.machineGun)&& bulletShoot > bulletShooted) {
                    bulletShooted ++;
                    Invoke("Shoot",0.1f);
                }
                else {
                    bulletLeft -= chargesUse;
                    bulletShooted = 1;
                }
                Destroy(_bullet, 2f);
            }
        }
    }

    public void Reload() {
        bulletLeft = magazineSize;
        bulletShooted = 1;
    }

    public void Weapon() { 
        switch (gunType) {
            case GunType.normal:
                chargesUse = 1;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 0;
                _ballGravityScale = 1;
                break;
            case GunType.laser:
                chargesUse = 4;
                bulletShoot = 1;
                _bulletDamage = 8;
                _bulletID = 1;
                _ballGravityScale = 1;
                break;
            case GunType.noppy:
                chargesUse = 2;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 2;
                _ballGravityScale = 4;
                break;
            case GunType.oppy:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 3;
                _ballGravityScale = 1;
                break;
            case GunType.katana:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 4;
                _bulletID = 4;
                _ballGravityScale = 1;
                break;
            case GunType.shotgun:
                chargesUse = 6;
                bulletShoot = 6;
                _bulletDamage = 4;
                _bulletID = 5;
                _ballGravityScale = 1;
                break;
            case GunType.burst:
                chargesUse = 4;
                bulletShoot = 2;
                _bulletDamage = 3;
                _bulletID = 6;
                _ballGravityScale = 2;
                break;
            case GunType.tripleShot:
                chargesUse = 3;
                bulletShoot = 1;
                _bulletDamage = 2;
                _bulletID = 7;
                _ballGravityScale = 1;
                break;
            case GunType.machineGun:
                chargesUse = 3;
                bulletShoot = 4;
                _bulletDamage = 1;
                _bulletID = 8;
                _ballGravityScale = 4;
                break;
        }
    }
}
