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
            int randomType = Random.Range(0, 4);
            int randomWeaponType = Random.Range(0, 9);
            GameObject _newLoot = Instantiate(upgradeLoot, transform.position, Quaternion.identity);
            ScLoot lootScript = _newLoot.GetComponent<ScLoot>();
            switch(randomType) {
                case 0:
                    lootScript.type = ScLoot.Type.gem;
                    break;
                case 1:
                    lootScript.type = ScLoot.Type.heart;
                    break;
                case 2:
                    lootScript.type= ScLoot.Type.securityBomb;
                    break;
            }
            switch(randomWeaponType){
                case 0:
                    lootScript.weaponType = ScLoot.WeaponType.laser;
                    break;
                case 1:
                    lootScript.weaponType = ScLoot.WeaponType.noppy;
                    break;
                case 2:
                    lootScript.weaponType = ScLoot.WeaponType.oppy;
                    break;
                case 3:
                    lootScript.weaponType = ScLoot.WeaponType.katana;
                    break;
                case 4:
                    lootScript.weaponType = ScLoot.WeaponType.shotgun;
                    break;
                case 5:
                    lootScript.weaponType = ScLoot.WeaponType.burst;
                    break;
                case 6:
                    lootScript.weaponType = ScLoot.WeaponType.machineGun;
                    break;
                case 7:
                    lootScript.weaponType = ScLoot.WeaponType.tripleShot;
                    break;
            }
            Destroy(this.gameObject);
        }   
    }
}

