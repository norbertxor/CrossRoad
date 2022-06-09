using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour {
    public Sprite button, buttonPressed;

    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();
    }

    public void PlayGame() {
        if (PlayerPrefs.GetString("First Game") == "No") {
            StartCoroutine(LoadScene("Game"));
            Fading.fadingAvaleble = true;
        }
        else
            StartCoroutine(LoadScene("Study"));
    }

    public void RestartGame() {
        StartCoroutine(LoadScene("Game"));
        Fading.fadingAvaleble = true;
    }

    public void ShopScene() {
        StartCoroutine(LoadScene("Shop"));
        Fading.fadingAvaleble = true;
    }

    public void ExitShop() {
        StartCoroutine(LoadScene("Main"));
        Fading.fadingAvaleble = true;
    }

    public void SetDefaultButton() {
        _image.sprite = button;
        transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
    }

    public void SetPressedButton() {
        _image.sprite = buttonPressed;
        transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
    }

    IEnumerator LoadScene(string name) {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

}