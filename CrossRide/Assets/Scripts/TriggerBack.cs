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
        if (other.gameObject.CompareTag("Car") 
            && !other.GetComponent<CarController>().carCrashed 
            && !ThisController.carCrashed 
            && !other.GetComponent<CarController>().nearCrash)
        {
            ThisController.speed = other.GetComponent<CarController>().speed;
            ThisController.isMovingFast = true;
        }
    }
}
