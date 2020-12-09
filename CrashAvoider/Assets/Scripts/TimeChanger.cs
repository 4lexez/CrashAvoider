using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
public class TimeChanger : MonoBehaviour
{
    [SerializeField] private int howMuchItCanBeUsed;
    [SerializeField] private Sprite btn, btnDisabled;
    [SerializeField] private Image timeImage;
    [SerializeField] private GameObject TimeBtn;
    [SerializeField] private Text Counter;
    [SerializeField] private AudioSource TimeSound;
    //[SerializeField] private bool isLose;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Time") == 0)
            timeImage.sprite = btnDisabled;
        changeTime(1);
        howMuchItCanBeUsed = PlayerPrefs.GetInt("Time");
        timeImage = GetComponent<Image>();
        Counter.text = howMuchItCanBeUsed.ToString();
        TimeSound = GetComponent<AudioSource>();
    }

    public void OnBtnPressed()
    {
        if(howMuchItCanBeUsed > 0 && !CarController.isLose && Time.timeScale == 1f)
        { 
            howMuchItCanBeUsed -= 1;
            PlayerPrefs.SetInt("Time", howMuchItCanBeUsed);
            StartCoroutine(TimeFrozing());
            if (PlayerPrefs.GetString("music") != "No")
                TimeSound.Play();
            Counter.text = howMuchItCanBeUsed.ToString();
            if (howMuchItCanBeUsed <= 0 && timeImage.sprite != btnDisabled)
            {
                timeImage.sprite = btnDisabled;
            }
        }
    }
    public void WhenCarWrecked()
    {
        if (TimeBtn.activeSelf)
        {
            TimeBtn.SetActive(false);
            PlayerPrefs.SetInt("Time", howMuchItCanBeUsed);
            StopCoroutine(TimeFrozing());
            changeTime(1);
        }
    }
    IEnumerator TimeFrozing()
    {
        changeTime(0.5f);
        yield return new WaitForSeconds(2f);
        changeTime(1);
    }
    private void changeTime(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
