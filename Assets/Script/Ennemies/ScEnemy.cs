using UnityEngine;

public class ScEnemy : MonoBehaviour {
    public int PV;
    public int damage;

    public bool IsJumpable;

    private void Update()
    {
        if(PV<=0) Destroy(gameObject);
    }

    public void ReceiveDamage(int _damage) {
        if (PV-damage > 0) { PV -= _damage; }
        else { Destroy(this.gameObject); }
    }
}
