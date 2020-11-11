using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{
    public int howMuchItCanBeUsed;
    public Sprite btn, btnDisabled;
    public Image timeImage;
    public GameObject TimeBtn;
    public Text Counter;
    public AudioSource TimeSound;
    private bool isLose;
    private void Start()
    {
        howMuchItCanBeUsed = 3;
        timeImage = GetComponent<Image>();
        Counter.text = howMuchItCanBeUsed.ToString();
    }

    public void OnBtnPressed()
    {
        if(howMuchItCanBeUsed > 0 && !CarController.isLose && Time.timeScale == 1f)
        { 
            howMuchItCanBeUsed -= 1;
            StartCoroutine(TimeFrozing());
            if (PlayerPrefs.GetString("music") != "No")
                TimeSound.Play();
            Counter.text = howMuchItCanBeUsed.ToString();
        }
        if(howMuchItCanBeUsed < 1)
        {
            timeImage.sprite = btnDisabled;
        }
    }
    public void WhenCarWrecked(bool isLose)
    {
        if (isLose)
        {
            TimeBtn.SetActive(false);
            StopCoroutine(TimeFrozing());
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
    IEnumerator TimeFrozing()
    {
        Time.timeScale *= 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSeconds(2f);
        Time.timeScale /= 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
