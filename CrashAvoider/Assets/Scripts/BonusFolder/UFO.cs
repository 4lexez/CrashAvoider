using UnityEngine;
public class UFO : MonoBehaviour
{
    [SerializeField] private CarController car;
    public static bool isUfo;
    
    public void OnClick()
    {
        isUfo = true;
    }
}
