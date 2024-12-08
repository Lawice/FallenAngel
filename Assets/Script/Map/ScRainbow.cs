using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScRainbow : MonoBehaviour {
    public float rainbowSpeed = 1f;
    public float saturation = 1f;

    private Image _image;
    private float _hue;

    void Start(){
        _image = GetComponent<Image>();
        _hue = Random.Range(0f, 1f);
    }

    void Update(){
        _hue += rainbowSpeed * Time.deltaTime;
        _hue = Mathf.Repeat(_hue, 1f);

        Color _newColor = Color.HSVToRGB(_hue, saturation, 1f);

        _image.color = _newColor;
    }
}
