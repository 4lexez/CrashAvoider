using UnityEngine.UI;
using UnityEngine;

public class CountCoins : MonoBehaviour {
    private void Start() {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
