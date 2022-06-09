using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {
    [NonSerialized] public static bool fadingAvaleble;
    public Texture2D fading;
    private float _fadeSpeed = 0.8f, _alpha = 1f, _fadeDirection = -1;
    private int _drawDepth = -1000;

    private void OnGUI() {
        if (fadingAvaleble) {
            _alpha += _fadeDirection * _fadeSpeed * Time.deltaTime;
            _alpha = Mathf.Clamp01(_alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, _alpha);
            GUI.depth = _drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fading);
        }
    }

    public float Fade(float dir) {
        _fadeDirection = dir;
        return _fadeSpeed;
    }
}