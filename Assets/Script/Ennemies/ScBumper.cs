using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBumper : MonoBehaviour {
    public GameObject player;
    private Transform _playerTrans;
    [SerializeField] private float _speed;
    [SerializeField] private float _detectionRange;
    Transform _transform;

    private void Start() {
        _playerTrans = player.GetComponent<Transform>();
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        if(_playerTrans != null) { Chase(); }
    }

    private void Chase() {
        Debug.Log(_playerTrans);
        float _currentDistanceFromPlayer = Vector2.Distance(_playerTrans.position, _transform.position);
        if(_currentDistanceFromPlayer < _detectionRange) {  _transform.position = Vector2.MoveTowards(_transform.position, _playerTrans.position,_speed*Time.deltaTime);}
    }
}
