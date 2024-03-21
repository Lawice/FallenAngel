using UnityEngine;

public class ScEnemy : MonoBehaviour {
    public int PV;
    public int damage;


    public bool IsJumpable;

    public void ReceiveDamage(int _damage) {
        if (PV-damage > 0) { PV -= _damage; }
        else { Destroy(this.gameObject); }
    }
}
