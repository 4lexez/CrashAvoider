using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{
    [SerializeField] private int howMuchItCanBeUsed;
    [SerializeField] private Sprite btn, btnDisabled;
    [SerializeField] private Image timeImage;
    [SerializeField] private GameObject TimeBtn;
    [SerializeField] private Text Counter;
    [SerializeField] private AudioSource TimeSound;
    [SerializeField] private bool isLose;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Time") == 0)
            timeImage.sprite = btnDisabled;

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
            
        }
        if(howMuchItCanBeUsed < 1 && !btnDisabled)
        {
            timeImage.sprite = btnDisabled;
        }
    }
    public void WhenCarWrecked(bool isLose)
    {
        if (isLose && TimeBtn.activeSelf)
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
