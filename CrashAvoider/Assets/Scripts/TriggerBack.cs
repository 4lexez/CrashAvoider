using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBack : MonoBehaviour
{
    CarController ThisController;
    private void Start()
    {
        ThisController = transform.parent.GetComponent<CarController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car")) {
            CarController otherCntrl = other.GetComponent<CarController>();
            if (!otherCntrl.carCrashed
            && !ThisController.carCrashed
            && !otherCntrl.nearCrash 
            && !ThisController.nearCrash)

            {
                ThisController.speed = otherCntrl.speed;
                ThisController.isMovingFast = true;
            }
        }
            
    }
}
