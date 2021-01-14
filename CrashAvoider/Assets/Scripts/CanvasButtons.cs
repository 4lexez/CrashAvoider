using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma warning disable 0649
public class CanvasButtons : MonoBehaviour {

    [SerializeField] private Sprite btn, btnPressed;
    [SerializeField] private Canvas canvasSettings, canvasDynamicPanel, canvasStaticPanel;
    private Image image;
    private Camera MainCam;
    [SerializeField] private MixerController AudioController;
    [SerializeField] private Dropdown QualityDropdown;
    [SerializeField] private Slider Volume;

    void Start() {
        if(Volume)
            Volume.value = PlayerPrefs.GetFloat("Volume");
        if (QualityDropdown != null)
            QualityDropdown.value = PlayerPrefs.GetInt("Quality");
        AudioController = GetComponent<MixerController>();
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        image = GetComponent<Image>();
    }

    public void VolumeChange(float volume)
    {
        if(volume != PlayerPrefs.GetFloat("Volume"))
        {
            AudioController.SetVolume(volume);
            PlayerPrefs.SetFloat("Volume", volume);

        }
    }
    public void QualityChange(int QualityValue)
    {
        QualitySettings.SetQualityLevel(QualityValue);
        PlayerPrefs.SetInt("Quality", QualityValue);
    }

    public void SettingsStatus()
    {
        canvasSettings.enabled = !canvasSettings.enabled;
        canvasStaticPanel.enabled = !canvasStaticPanel.enabled;
        canvasDynamicPanel.enabled = !canvasDynamicPanel.enabled;
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
