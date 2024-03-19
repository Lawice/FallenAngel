using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLoot : MonoBehaviour {
    public enum Type {gem, heart, securityBomb };
    public Type type;

    public ScShoot.GunType weaponType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out ScShoot shootScript)) {
            ScHeal scoreScript = collision.gameObject.GetComponent<ScHeal>();
            shootScript.gunType = weaponType;
            ScShoot.Instance.Weapon();
            switch (type) {
                case Type.gem:

                    break;
                 

            }
            Destroy(this.gameObject);
        }
    }
}
