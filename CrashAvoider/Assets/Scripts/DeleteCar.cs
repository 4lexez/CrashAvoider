using UnityEngine;

public class DeleteCar : MonoBehaviour
{
    public static DeleteCar thisObject;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (thisObject == null)
        {
            thisObject = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
