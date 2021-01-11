using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma warning disable 0649
public class CanvasButtons : MonoBehaviour {

    [SerializeField] private Sprite btn, btnPressed, musicOn, musicOff;
    private Image image;
    private Camera MainCam;
    [SerializeField] private MixerController AudioController;
    public Gui guiScript;

    void Start() {
        guiScript = GetComponent<Gui>();
        AudioController = GetComponent<MixerController>();
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        image = GetComponent<Image>();
        if (gameObject.name == "Music Button") {
            if (PlayerPrefs.GetString("music") == "No")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
                AudioController.SetVolume(-80);
            }

        }
    }

    public void MusicButton() {
        if (AudioController.VolumeValue == -80)
        { // Turn on
            PlayerPrefs.SetString("music", "Yes");
            AudioController.SetVolume(0);
            transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
        }
        else if(AudioController.VolumeValue == 0)
        { // Turn off
            PlayerPrefs.SetString("music", "No");
            AudioController.SetVolume(-80);
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }

        PlayButtonSound();
    }

    public void ShopScene() {
        StartCoroutine(LoadScene("Shop"));
        PlayButtonSound();
    }
    
    public void ExitShopScene() {
        StartCoroutine(LoadScene("Main"));
        PlayButtonSound();
    }

    public void PlayGame() {
        if(PlayerPrefs.GetString("First Game") == "No")
            StartCoroutine(LoadScene("Game"));
        else
            StartCoroutine(LoadScene("Study"));
        PlayButtonSound();
    }

    public void RestartGame() {
        StartCoroutine(LoadScene("Game"));
        PlayButtonSound();
    }
    
    public void SetPressedButton() {
        image.sprite = btnPressed;
        if (transform.childCount != 0 && !transform.GetChild(0).gameObject.isStatic)
            transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
    }
    
    public void SetDefaultButton() {
        image.sprite = btn;
        if(transform.childCount != 0 && !transform.GetChild(0).gameObject.isStatic)
            transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
    }

    IEnumerator LoadScene(string name)
    {
        float fadeTime = MainCam.GetComponent<Fading>().Fade(1f);
        //float fadeTime = fading.Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    private void PlayButtonSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
    }
    public void OnExitButton()
    {
        Application.Quit();
    }

}
