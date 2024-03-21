using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.Search;
using UnityEditor.Experimental.GraphView;

public class ScShoot : MonoBehaviour{
    private Rigidbody2D _body;
    public enum GunType {normal,  laser, noppy, oppy, katana, shotgun, burst, machineGun, tripleShot}

    public TextMeshProUGUI weaponDisplay;
    [SerializeField] string _weaponName;

    [Header("~~~~~~Gun Stats~~~~~~")]
    public GunType gunType;
    public int magazineSize;
    public int bulletLeft;
    public int chargesUse;
    public int bulletShoot;
    public int bulletShooted = 1;

    [Header("~~~~~~Bullet Stats~~~~~~")]
    [SerializeField] public Transform shootPoint;
    [SerializeField] float _ballGravityScale;
    [SerializeField] float _ballSpeed;
    [SerializeField] int _bulletDamage;
    [SerializeField] List<GameObject> _bulletPrefab;
    int _bulletID;
    [SerializeField] int _horizontal;

    [Header("~~~~~~Security Bomb~~~~~~")]
    public GameObject bomb;
    public int maxBomb = 3;
    public int nbBomb;


    [Header("~~~~~~Laser~~~~~~")]
    public LineRenderer laserLine;
    public Transform laserFirePoint;
    public float laserRange = 100f;


    public static ScShoot Instance;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    void Start() {
        nbBomb = maxBomb;
        _body = GetComponent<Rigidbody2D>();
        bulletLeft = magazineSize;
        _horizontal = 1;
        _ballGravityScale = 1;
        HideDisplay();
        UpdateWeapon();
    }

    public void BombInput(InputAction.CallbackContext ctx){
        if (ctx.performed) {
            if (nbBomb > 0) {
                Instantiate(bomb, shootPoint.position, Quaternion.identity);
                nbBomb--;
            }
        }
    }

    public void Shoot() {
        if (bulletShooted == 1) { UpdateWeapon(); }
        if (bulletLeft > 0) {
            if (!ScPlayerMovement.Instance.IsAbleToJump) { _body.velocity = new Vector2(_body.velocity.x, 0.5f); }
            if (gunType != GunType.laser) {
                GameObject _bullet = Instantiate(_bulletPrefab[_bulletID], shootPoint.position, Quaternion.identity);
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
                    _scBalls.velocity = new Vector2(_bulletVelocityX * _horizontal, _bulletVelocityY);
                }
                else {
                    _scBalls.gravity = _ballGravityScale;
                }
                if ((gunType == GunType.burst || gunType == GunType.machineGun) && bulletShoot > bulletShooted) {
                    bulletShooted++;
                    Invoke("Shoot", 0.1f);
                } else {
                    if (bulletLeft % chargesUse == 0) { bulletLeft -= chargesUse; }
                    else { bulletLeft -= bulletLeft % chargesUse; }
                    bulletShooted = 0;
                }
                Destroy(_bullet, 2f);
            }
            else {
                laserLine.enabled = true;
                laserLine.SetPosition(0, laserFirePoint.position);

                Vector3 _maxLaserEndPoint = laserFirePoint.position + transform.up * -laserRange;

                RaycastHit2D[] _hits = Physics2D.RaycastAll(laserFirePoint.position, transform.up, -laserRange);
                foreach (var _hit in _hits) {
                    if (_hit.collider.gameObject.TryGetComponent(out ScGround groundScript)) {
                        if (groundScript.type == ScGround.BlockType.normal) {
                            _maxLaserEndPoint = _hit.point;
                            break;
                        }
                        else if (groundScript.type == ScGround.BlockType.crate) { groundScript.SpawnLoot(); }
                        else if(groundScript.type == ScGround.BlockType.breakable) { Destroy(_hit.collider.gameObject); }
                    }
                }
                laserLine.SetPosition(1, _maxLaserEndPoint);
                Invoke("DisableLaser", 0.1f);
                if (bulletLeft % chargesUse == 0) { bulletLeft -= chargesUse; }
                else { bulletLeft -= bulletLeft % chargesUse; }
            }
        }
    }

    void DisableLaser() {
        laserLine.enabled = false;
    }

    public void Reload() {
        bulletLeft = magazineSize;
        bulletShooted = 0;
    }

    public void HideDisplay() {
        weaponDisplay.text = default;
    }

    public void ShowDisplay() {
        weaponDisplay.SetText(_weaponName);
        Invoke("HideDisplay", 2f);
    }

    public void Weapon(int _chargesUse, int _bulletShoot,int _damage, int _ID, float _force, string _name) {
        chargesUse = _chargesUse;
        bulletShoot = _bulletShoot;
        _bulletDamage = _damage;
        _bulletID = _ID;
        _ballGravityScale = _force;
        _weaponName = _name;
    }

    public void UpdateWeapon() { 
        switch (gunType) {
            case GunType.normal:
                Weapon(1, 1, 2, 0, 1,"Normal");
                break;
            case GunType.laser:
                Weapon(4, 1, 8, 1, 1,"Laser");
                break;
            case GunType.noppy:
                Weapon(2, 1, 2, 2, 4,"Noppy");
                break;
            case GunType.oppy:
                Weapon(3, 1, 2, 3, 1,"Oppy");
                break;
            case GunType.katana:
                Weapon(3, 1, 4, 4, 2, "Katana");
                break;
            case GunType.shotgun:
                Weapon(6, 6, 4, 5, 1, "ShotGun");
                break;
            case GunType.burst:
                Weapon(4, 2, 3, 6, 2, "Burst");
                break;
            case GunType.tripleShot:
                Weapon(3, 1, 3, 7, 1, "Triple Shot");
                break;
            case GunType.machineGun:
                Weapon(3, 4, 1, 8, 4, "Machine Gun");
                break;
        }
    }

    public void AddBomb(){
        if (nbBomb < maxBomb) {nbBomb++;}
    }
}
