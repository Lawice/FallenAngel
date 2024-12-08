using System;
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
            int randomType = UnityEngine.Random.Range(0, 8);
            int randomWeaponType = UnityEngine.Random.Range(0, 9);
            GameObject _newLoot = Instantiate(upgradeLoot, transform.position, Quaternion.identity);
            ScLoot _lootScript = _newLoot.GetComponent<ScLoot>();
            switch(randomType) {
                case 0:
                case 1:
                case 2:
                case 3:
                    _lootScript.type = ScLoot.Type.gem;
                    break;
                case 4:
                case 5:
                case 6:
                    _lootScript.type = ScLoot.Type.heart;
                    break;
                case 7:
                    if (ScShoot.Instance.nbBomb < ScShoot.Instance.maxBomb) { _lootScript.type = ScLoot.Type.securityBomb; }
                    else { _lootScript.type = ScLoot.Type.gem; }
                    break;
            }
            if (_lootScript.type != ScLoot.Type.securityBomb){
                ScShoot.GunType newType = RandomUpgrade();
                do { newType = RandomUpgrade();
                } while (ScShoot.Instance.gunType == newType);

                _lootScript.weaponType = newType;
            }

            Destroy(this.gameObject);
        }
    }

    public ScShoot.GunType RandomUpgrade() {
        System.Random random = new System.Random();
        ScShoot.GunType[] gunTypes = (ScShoot.GunType[])System.Enum.GetValues(typeof(ScShoot.GunType));
        int randomID = random.Next(gunTypes.Length);
        return gunTypes[randomID];
    }
}



