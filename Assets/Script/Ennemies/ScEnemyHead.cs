using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEnemyHead : MonoBehaviour{
    public GameObject enemy;

    private void OnCollisionEnter2D(Collision2D _collision){
        if (_collision.gameObject.TryGetComponent(out ScPlayerMovement _playerMovementScript)){
            Debug.Log("BBBB");
            Destroy(enemy);
        }
    }
}
