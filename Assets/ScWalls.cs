using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWalls : MonoBehaviour {
    Transform _wallTransform;
    [SerializeField] Transform _playerTransform;
    Vector3 _targetPosition;
    void Start() {
        _wallTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (_playerTransform != null){
            _targetPosition.x = _wallTransform.position.x;
            _targetPosition.y = _playerTransform.position.y;
            _targetPosition.z = _wallTransform.position.z;
            _wallTransform.position = _targetPosition;
        }
    }
}
