using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour {
    
    private void OnDestroy() {
        PlayerPrefs.SetString("First Game", "No");
        SceneManager.LoadScene("Game");
        
    }
    
}
