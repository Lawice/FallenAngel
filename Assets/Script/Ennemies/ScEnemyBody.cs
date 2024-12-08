using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEnemyBody : MonoBehaviour {
    [SerializeField] private GameObject _enemy;
    public int _enemydamege;
    
    private void Awake() {
        if(_enemy != null) { _enemydamege = _enemy.GetComponent<ScEnemy>().damage; }
    }

    private void OnCollisionEnter2D(Collision2D _collision){
        if ( _collision.gameObject.TryGetComponent(out ScHeal healScript)){
            Debug.Log("AAAAA");
            healScript.TakeDamage(_enemydamege);
        }
    }

}
