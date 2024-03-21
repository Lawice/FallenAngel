using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEnemyBody : MonoBehaviour {
    public GameObject enemy;
    private ScEnemy _enemyScript;
    private void Awake() {
        ScEnemy enemyScript = enemy.GetComponent<ScEnemy>();
    }

    private void OnCollisionEnter2D(Collision2D _collision){
        if(_collision.gameObject.TryGetComponent(out ScHeal healScript)) {
            Debug.Log("AAAAA");
            healScript.TakeDamage(_enemyScript.damage);
        }
    }
}
