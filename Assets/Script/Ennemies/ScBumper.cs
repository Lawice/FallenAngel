using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBumper : MonoBehaviour {
    [SerializeField] public Transform playerTrans;
    [SerializeField] private float _speed;
    [SerializeField] private float _detectionRange;
    Transform _transform;

    private void Start() {
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        if(playerTrans != null) { Chase(); }
    }

    private void Chase() { 
        float _currentDistanceFromPlayer = Vector2.Distance(playerTrans.position, _transform.position);
        if(_currentDistanceFromPlayer < _detectionRange) {  _transform.position = Vector2.MoveTowards(_transform.position, playerTrans.position,_speed*Time.deltaTime);}
    }
}
