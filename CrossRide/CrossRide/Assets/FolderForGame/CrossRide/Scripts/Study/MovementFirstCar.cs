using System;
using UnityEngine;

public class MovementFirstCar : MonoBehaviour {

    public GameObject canvasFirst, secondCar, secondCanvas;
    private bool isFirst;
    private CarController _controller;


    private void Start() {
        _controller = GetComponent<CarController>();

    }

    private void Update() {
        if (transform.position.x < 8f && !isFirst) {
            isFirst = true;
            _controller.speed = 0;
            canvasFirst.SetActive(true);
        }
    }

    private void OnMouseDown() {
        if (!isFirst || transform.position.x > 9f) return;

        _controller.speed = 15f;
        PlayerPrefs.SetInt("Time", 3);
        canvasFirst.SetActive(false);
        secondCanvas.SetActive(true);
        secondCar.GetComponent<CarController>().speed = 12f;
    }
}
