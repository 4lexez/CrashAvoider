using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#pragma warning disable 0649
public class GameController : MonoBehaviour {

    [SerializeField] private bool isMainScene;
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject canvasLosePanel;
    [SerializeField] private float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    [SerializeField] private int countCars;
    [SerializeField] private Coroutine bottomCars, leftCars, rightCars, upCars;
    [SerializeField] private bool isLoseOnce;
    [SerializeField] private Text nowScore, topScore, coinsCount;
    //[SerializeField] private GameObject AdManager;
    private TimeChanger timeChanger;
    public static bool IsAdd;
    public static bool IsStartTimeAdded;
    [SerializeField] private GameObject environment;
    private Vector3[] vectors = new Vector3[] {
        new Vector3(-1.06f, -0.15f, -22.7f),
        new Vector3(-82.9f, -0.15f, 3.2f),
        new Vector3(26.4f, -0.15f, 9.86f),
        new Vector3(-8.11f, -0.15f, 58f)
        };
    private float rotateCarTo;
    private bool IsFromUp;
    public static Action ActionDead;


    private void Awake()
    {
#if UNITY_ANDROID

        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        QualitySettings.antiAliasing = 0;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowDistance = 70;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#endif

#if UNITY_STANDALONE_WIN
         
         Application.targetFrameRate = 60;
         QualitySettings.vSyncCount = 1; 
         QualitySettings.antiAliasing = 8;
#endif
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
    }
    private void Start() {
        timeChanger = GameObject.Find("TimeChanging")?.GetComponent<TimeChanger>();
        ActionDead = Dead;
        int EnCount = PlayerPrefs.GetInt("NowMap") - 1;
        if (environment != null)
        {
            for (int i = environment.transform.childCount -1; i > 0; i--)
            {
                if (i != EnCount)
                    environment.transform.GetChild(i).gameObject.SetActive(false);
                else if (EnCount < 0)
                {
                    if (i != 0)
                        environment.transform.GetChild(i).gameObject.SetActive(false);
                }

        }
     }


            //environment.transform.GetChild(PlayerPrefs.GetInt("NowMap") - 1).gameObject.SetActive(true);
        
            //environment.transform.GetChild(0).gameObject.SetActive(true);
        CarController.isLose = false;
        CarController.countCars = 0;
        
        if (isMainScene) {
            timeToSpawnFrom = 2f;
            timeToSpawnTo = 4f;
        }

        StartCoroutine(Spawning());
    }
    public void Dead()
    {
        StopCoroutine(Spawning());
        timeChanger.WhenCarWrecked();
        nowScore.text = "<color=#F65757>Score:</color> " + CarController.countCars.ToString();
        if (PlayerPrefs.GetInt("Score") < CarController.countCars)
            PlayerPrefs.SetInt("Score", CarController.countCars);

        topScore.text = "<color=#F65757>Top:</color> " + PlayerPrefs.GetInt("Score").ToString();

        //PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
        CountCoins.Coin = CountCoins.Coin + CarController.countCars;
        CountCoins.Save();
        //coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
        coinsCount.text = CountCoins.Coin.ToString();

        canvasLosePanel.SetActive(true);
        isLoseOnce = true;
    }
    IEnumerator Spawning()
    {
        int random = 0;
        while (!isLoseOnce)
            {
            
            if (random > 3) random = 0;
            IsFromUp = false;

            switch (random)
            {
                case 0:
                    rotateCarTo = 270f;
                    break;
                case 1:
                    rotateCarTo = 0f;
                    break;
                case 2:
                    rotateCarTo = 180f;
                    break;
                case 3:
                    rotateCarTo = 90f;
                    IsFromUp = true;
                    break;
            }
            SpawnCar(vectors[random], rotateCarTo, IsFromUp);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            random++;
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    void SpawnCar(Vector3 pos, float rotateCarTo, bool isMoveFromUp = false)
    {
        CarController newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotateCarTo, 0)).GetComponent<CarController>();
        newObj.name = $"Car - {++countCars}";

        int random = isMainScene ? 1 : Random.Range(1, 4);
        if (isMainScene)
        {
            newObj.speed = 10f;
        }


        switch (random)
        {
            case 1:
                // Move right
                newObj.rightTurn = true;
                break;
            case 3:
                // Move left
                newObj.leftTurn = true;
                if (isMoveFromUp)
                    newObj.moveFromUp = true;
                break;
            case 5:
                // Move forward
                break;
        }
    }
}
