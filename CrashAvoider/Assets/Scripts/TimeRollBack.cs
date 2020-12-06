using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRollBack : MonoBehaviour
{

    [SerializeField] private GameObject TimeBtn;
    [SerializeField] private AudioSource TimeSound;
    private void Start()
    {
        TimeSound = GetComponent<AudioSource>();
    }

    public void OnBtnPressed()
    {
        StartCoroutine(TimeRoller());
        if (PlayerPrefs.GetString("music") != "No")
            TimeSound.Play();
    }
    public void WhenCarWrecked()
        {
            if (TimeBtn.activeSelf)
            {
                TimeBtn.SetActive(false);
                StopCoroutine(TimeRoller());
                changeTime(1);
            }
        }
        IEnumerator TimeRoller()
        {
            changeTime(-1);
            yield return new WaitForSeconds(2f);
            changeTime(1);
        }
        private void changeTime(float time)
        {
            Time.timeScale = time;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

}

