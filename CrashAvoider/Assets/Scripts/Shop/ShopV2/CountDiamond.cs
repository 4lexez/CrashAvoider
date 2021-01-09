using UnityEngine;
using UnityEngine.UI;

public class CountDiamond : MonoBehaviour
{
    [SerializeField] private Text thisT;
    public static int Diamond;
    private const string hash = "SettingWgasgcvbttfhgjtJGFdfh";

    // private const int hashInt = 42819;

    private void Awake()
    {
        Diamond = PlayerPrefs.GetInt(hash);
        thisT.text = $"{Diamond}";
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(hash, Diamond);
    }
}
