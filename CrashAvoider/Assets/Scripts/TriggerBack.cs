
using UnityEngine;

public class TriggerBack : MonoBehaviour
{
    CarController ThisController;
    CarController OtherController;
    private bool IsCarPerkUsed;
    private void Start()
    {
        ThisController = transform.parent.GetComponent<CarController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            OtherController = other.GetComponent<CarController>();
            IsCarPerkUsed = OtherController.IsPerkUsed;
            if (!OtherController.carCrashed
            && !ThisController.carCrashed
            && !OtherController.nearCrash
            && !ThisController.nearCrash)

            {
                //ThisController.speed = otherCntrl.speed;
                //ThisController.CallCoroutine(Mathf.RoundToInt(otherCntrl.speed), false);
                OtherController.CallCoroutine(Mathf.RoundToInt(ThisController.speed), false);
                OtherController.isMovingFast = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(!IsCarPerkUsed && other.gameObject.CompareTag("Car"))
            OtherController.isMovingFast = false;
    }
}
