using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour {
    private void OnDestroy() {
        PlayerPrefs.SetString("First Game", "No");
        PlayerPrefs.SetInt("Quality", 3);
        PlayerPrefs.SetInt("NowMap", 1);
        PlayerPrefs.SetInt("Time", 3);
        SceneManager.LoadScene("Game");

    }
}
