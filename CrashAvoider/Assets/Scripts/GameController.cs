using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#pragma warning disable 0649
public class GameController : MonoBehaviour {
    #region Canvases
    [SerializeField] private Canvas canvasLosePanel, canvasButtonPanel;
    #endregion
    #region Actions
    public static Action ActionDead;
    #endregion
    #region Vectors
    [SerializeField] private GameObject environment;
    private Vector3[] vectors = new Vector3[] {
        new Vector3(-1.06f, -0.15f, -22.7f),
        new Vector3(-82.9f, -0.15f, 3.2f),
        new Vector3(26.4f, -0.15f, 9.86f),
        new Vector3(-8.11f, -0.15f, 58f)
        };
    #endregion
    #region Bools
    private bool IsFromUp;
    [SerializeField] private bool isLoseOnce;
    public static bool IsAdd;
    [SerializeField] private bool isMainScene;
    public static bool IsStartTimeAdded;
    #endregion
    #region FloatsAndInts
    [SerializeField] private float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    private float rotateCarTo;
    [SerializeField] private int countCars;
    [SerializeField] private int decide;
    #endregion
    #region Scripts
    private TimeChanger timeChanger;
    [SerializeField] private CarController[] cars;
    #endregion
    #region Texts
    [SerializeField] private Text nowScore, topScore, coinsCount;
    #endregion
    #region Coroutines
    [SerializeField] private Coroutine bottomCars, leftCars, rightCars, upCars;
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = -1;
#if UNITY_ANDROID
        Screen.SetResolution(1920, 1080, true);

        QualitySettings.vSyncCount = 0;
        QualitySettings.antiAliasing = 0;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowDistance = 70;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#endif

#if UNITY_STANDALONE_WIN
         
         Application.targetFrameRate = -1;
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
        /*if (environment != null)
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
        }*/
        CarController.isLose = false;
        CarController.countCars = 0;
        
        if (isMainScene) {
            timeToSpawnFrom = 2f;
            timeToSpawnTo = 3f;
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
        CountCoins.Coin = CountCoins.Coin + CarController.countCars;
        CountCoins.Save();
        coinsCount.text = CountCoins.Coin.ToString();
        canvasLosePanel.enabled = true;
        canvasButtonPanel.enabled = true;
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

            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);

            yield return new WaitForSeconds(timeToSpawn);
            SpawnCar(vectors[random], rotateCarTo, IsFromUp);
            random++;
        }
    }
    void SpawnCar(Vector3 pos, float rotateCarTo, bool isMoveFromUp = false)
    {
        var newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotateCarTo, 0));
        //var newObj = Instantiate(cars[decide], pos, Quaternion.Euler(0, rotateCarTo, 0));


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