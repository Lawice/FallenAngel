using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLaser : MonoBehaviour {
    public float speed;
    public int damage;
    private LineRenderer _lineRenderer;
    private Transform _transform;
    void Start(){
        _lineRenderer = GetComponent<LineRenderer>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        _lineRenderer.SetPosition(0, ScShoot.Instance.shootPoint.position);
        RaycastHit2D groundHit = Physics2D.Raycast(ScShoot.Instance.shootPoint.position, transform.up, -Mathf.Infinity);
        if (groundHit.transform.gameObject.TryGetComponent(out ScGround ground)) {
            if(ground.type == ScGround.BlockType.normal) { _lineRenderer.SetPosition(1, groundHit.point); }
            else if(ground.type == ScGround.BlockType.breakable || ground.type == ScGround.BlockType.crate) { _lineRenderer.SetPosition(1, groundHit.point); Destroy(groundHit.collider.gameObject); }
            
        }
        _lineRenderer.SetPosition(1, ScShoot.Instance.shootPoint.position + transform.up * -100f);
    }
}
