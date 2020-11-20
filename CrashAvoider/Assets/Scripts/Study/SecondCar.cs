using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour {
    
    private void OnDestroy() {
        PlayerPrefs.SetString("First Game", "No");
        PlayerPrefs.SetInt("NowMap", 1);
        SceneManager.LoadScene("Game");
        
    }
    
}
