using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCamera : MonoBehaviour {
    [SerializeField] Transform _playerTransform;
    Transform _camTransform;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _smoothTime;
    Vector3 _targetPosition;
    Vector3 _velocity;

    void Awake() {
        _offset = new Vector3(0f, 0f, -10f);
        _camTransform =GetComponent<Transform>();
    }

    void FixedUpdate() {
        if (_playerTransform != null){
            _targetPosition.x = _camTransform.position.x;
            _targetPosition.y = _playerTransform.position.y + _offset.y;
            _targetPosition.z = _camTransform.position.z;
            _camTransform.position = Vector3.SmoothDamp(_camTransform.position, _targetPosition, ref _velocity, _smoothTime);
        }
    }
}
