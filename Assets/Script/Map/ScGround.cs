using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGround : MonoBehaviour {
    public enum BlockType { normal, semi, breakable, wall, crate}
    public BlockType type;

    [Header("~~~~~~Sprite~~~~~~")]
    public Sprite normal;
    public Sprite semi;
    public Sprite breakable;
    public Sprite wall;
    public Sprite crate;
    SpriteRenderer spriteRenderer;

    [Header("~~~~~~Sprite~~~~~~")]
    public GameObject upgradeLoot;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start() {
        UpdateColor();
    }

    void UpdateColor() {
        switch (type) {
            case BlockType.normal:
                spriteRenderer.sprite = normal;
                break;
            case BlockType.semi:
                spriteRenderer.sprite = semi;
                break;
            case BlockType.breakable:
                spriteRenderer.sprite = breakable;
                break;
            case BlockType.wall:
                spriteRenderer.sprite = wall;
                break;
            case BlockType.crate:
                spriteRenderer.sprite = crate;
                break;

        }
    }

    public void SpawnLoot(){
        if (type == BlockType.crate) {
            int randomType = Random.Range(0, 8);
            int randomWeaponType = Random.Range(0, 9);
            GameObject _newLoot = Instantiate(upgradeLoot, transform.position, Quaternion.identity);
            ScLoot lootScript = _newLoot.GetComponent<ScLoot>();
            switch(randomType) {
                case 0:
                case 1:
                case 2:
                case 3:
                    lootScript.type = ScLoot.Type.gem;
                    break;
                case 4:
                case 5:
                case 6:
                    lootScript.type = ScLoot.Type.heart;
                    break;
                case 7:
                    lootScript.type= ScLoot.Type.securityBomb;
                    break;
            }
            if (randomType != 7) {
                switch (randomWeaponType) {
                    case 0:
                        lootScript.weaponType = ScShoot.GunType.laser;
                        break;
                    case 1:
                        lootScript.weaponType = ScShoot.GunType.noppy;
                        break;
                    case 2:
                        lootScript.weaponType = ScShoot.GunType.oppy;
                        break;
                    case 3:
                        lootScript.weaponType = ScShoot.GunType.katana;
                        break;
                    case 4:
                        lootScript.weaponType = ScShoot.GunType.shotgun;
                        break;
                    case 5:
                        lootScript.weaponType = ScShoot.GunType.burst;
                        break;
                    case 6:
                        lootScript.weaponType = ScShoot.GunType.machineGun;
                        break;
                    case 7:
                        lootScript.weaponType = ScShoot.GunType.tripleShot;
                        break;
                }
                Destroy(this.gameObject);
            }
        }   
    }
}

