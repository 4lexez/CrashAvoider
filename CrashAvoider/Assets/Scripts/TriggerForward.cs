using System.Collections;
using System.Collections.Generic;
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
            /*if (!otherCntrl.carCrashed
            && !ThisController.carCrashed
            && !otherCntrl.nearCrash)
            {
                ThisController.speed = otherCntrl.speed;
                ThisController.isMovingFast = true;
            }*/
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
