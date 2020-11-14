using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {

    [SerializeField] private bool isMainScene;
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject canvasLosePanel;
    [SerializeField] private float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    [SerializeField] private int countCars;
    [SerializeField] private Coroutine bottomCars, leftCars, rightCars, upCars;
    [SerializeField] private bool isLoseOnce;
    [SerializeField] private Text nowScore, topScore, coinsCount;
    [SerializeField] private GameObject AdManager;
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

    private void Start() {
        ActionDead = Dead;
        if (environment != null)
            environment.transform.GetChild(PlayerPrefs.GetInt("NowMap") - 1).gameObject.SetActive(true);
        if (!IsAdd && PlayerPrefs.GetString("NoAds") != "yes")

        {
            Instantiate(AdManager, Vector3.zero, Quaternion.identity);
            IsAdd = true;
            
        }
        
        CarController.isLose = false;
        CarController.countCars = 0;
        
        if (isMainScene) {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }

        StartCoroutine(Spawning());
    }

    /*private void Update()
    { // НЕ ОБРАЩАЙ ВНИМАНИЯ, Я ЕЩЕ НЕ УСПЕЛ УБРАТЬ ЕГО
        if (CarController.isLose && !isLoseOnce)
        {
            StopCoroutine(Spawning());
            nowScore.text = "<color=#F65757>Score:</color> " + CarController.countCars.ToString();
            if (PlayerPrefs.GetInt("Score") < CarController.countCars)
                PlayerPrefs.SetInt("Score", CarController.countCars);

            topScore.text = "<color=#F65757>Top:</color> " + PlayerPrefs.GetInt("Score").ToString();

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
            coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();

            canvasLosePanel.SetActive(true);
            isLoseOnce = true;
        }
    }*/
    public void Dead()
    {
        print("Crash");
        StopCoroutine(Spawning());
        nowScore.text = "<color=#F65757>Score:</color> " + CarController.countCars.ToString();
        if (PlayerPrefs.GetInt("Score") < CarController.countCars)
            PlayerPrefs.SetInt("Score", CarController.countCars);

        topScore.text = "<color=#F65757>Top:</color> " + PlayerPrefs.GetInt("Score").ToString();

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
        coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();

        canvasLosePanel.SetActive(true);
        isLoseOnce = true;
    }
    IEnumerator Spawning()
    {
        while (!isLoseOnce)
        {
            int random = Random.Range(0, vectors.Length);
            //int random = 0;
            IsFromUp = false;

            switch (random)
            {
                case 0:
                    rotateCarTo = 180f;
                    break;
                case 1:
                    rotateCarTo = 270f;
                    break;
                case 2:
                    rotateCarTo = 90f;
                    break;
                case 3:
                    rotateCarTo = 0f;
                    IsFromUp = true;
                    break;
            }
            SpawnCar(vectors[random], rotateCarTo, IsFromUp);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    void SpawnCar(Vector3 pos, float rotateCarTo, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotateCarTo, 0)) as GameObject;
        newObj.name = $"Car - {++countCars}";

        int random = isMainScene ? 1 : Random.Range(1, 4);
        if (isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;

        switch (random)
        {
            case 1:
                // Move right
                newObj.GetComponent<CarController>().rightTurn = true;
                break;
            case 3:
                // Move left
                newObj.GetComponent<CarController>().leftTurn = true;
                if (isMoveFromUp)
                    newObj.GetComponent<CarController>().moveFromUp = true;
                break;
            case 5:
                // Move forward
                break;

        }
    }
}
