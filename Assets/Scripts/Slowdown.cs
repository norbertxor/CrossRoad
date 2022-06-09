using System;
using UnityEngine;
using UnityEngine.UI;

public class Slowdown : MonoBehaviour {
    [NonSerialized] public static bool isSlowed = false;
    public GameObject timer;
    public Text count;
    public Sprite timerIsAvailable, timerIsUnavailable;

    private AudioSource _slowedSound;
    private Image _nowImage;
    private Text _timerText;
    private float _timerValue;
    private int _timerCount;

    private void Start() {
        _timerCount = PlayerPrefs.GetInt("Slowdown");
        _timerValue = 5f;
        _nowImage = GetComponent<Image>();
        _timerText = timer.GetComponent<Text>();
        _slowedSound = GetComponent<AudioSource>();
        
        if (_timerCount > 0)
            _nowImage.sprite = timerIsAvailable;
        else if (_timerCount <= 0)
            _nowImage.sprite = timerIsUnavailable; 
    }

    private void Update() {
        if (isSlowed) {
            _timerValue -= Time.deltaTime;
            _timerText.text = "SLOWDOWN: " + _timerValue.ToString("F2");
            if (_timerValue <= 0) {
                isSlowed = false;
                _timerValue = 5f;
                timer.SetActive(false);
            }
            
        }
        count.text = _timerCount.ToString();
    }

    public void SetSlow() {
        transform.localScale = new Vector3(1,1,1);
        if (_timerCount <= 0)
            return;
        timer.SetActive(true);
        isSlowed = true;
        _timerCount--;
        if(_timerCount < 1)
            _nowImage.sprite = timerIsUnavailable;
        _slowedSound.Play();
        PlayerPrefs.SetInt("Slowdown", _timerCount);
    }

    public void SetPressed() {
        transform.localScale = new Vector3(1.1f,1.1f,1.1f); 
    }
    
        
    }

