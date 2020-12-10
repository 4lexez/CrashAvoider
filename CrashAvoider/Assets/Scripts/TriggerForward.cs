
using UnityEngine;

public class TriggerForward : MonoBehaviour
{
    CarController ThisController;
    private void Start()
    {
        ThisController = transform.parent.GetComponent<CarController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            CarController otherCntrl = other.GetComponent<CarController>();
            if (otherCntrl.carCrashed || otherCntrl.nearCrash)
                ThisController.CallCoroutine();
        }
    }
}
