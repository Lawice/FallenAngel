using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLoot : MonoBehaviour {
    public enum Type {gem, heart, securityBomb };
    public Type type;

    public enum WeaponType {laser, noppy, oppy, katana, shotgun, burst, machineGun, tripleShot }
    public WeaponType weaponType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScShoot shootScript)) {
            ScScore scoreScript = collision.gameObject.GetComponent<ScScore>();
            switch (type) {
                case Type.gem:

                    break;
                    

            }
        }
    }
}
