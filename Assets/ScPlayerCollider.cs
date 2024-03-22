using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerCollider : MonoBehaviour {
    private void OnCollisionStay2D(Collision2D _collision) {
        if (_collision.gameObject.TryGetComponent(out ScGround groundScript)){
            if (groundScript.type != ScGround.BlockType.wall) { ScPlayerMovement.Instance.CheckGround(); }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision) {
        if(_collision.gameObject.TryGetComponent(out ScGround groundScript)) {
            if(groundScript.type != ScGround.BlockType.wall) { ScPlayerMovement.Instance.CheckGround(); }
        }
        if (_collision.gameObject.TryGetComponent(out ScEnemy ennemyScript)) {
            ScPlayerMovement.Instance.CheckGround();
            Destroy(_collision.gameObject);
        }
    }
}
