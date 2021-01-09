using System;
using UnityEngine;
#pragma warning disable 0649
public class MovementFirstCar : MonoBehaviour {

    [SerializeField] private GameObject  secondCar;
    [SerializeField] private Canvas canvasFirst, secondCanvas;
    private bool isFirst;
    private CarController _controller;

    private void Start() {
        _controller = GetComponent<CarController>();
    }

    private void Update() {
        if (transform.position.x < 8f && !isFirst) {
            isFirst = true;
            _controller.speed = 0;
            canvasFirst.enabled = true;
        }
    }

    private void OnMouseDown() {
        if (!isFirst || transform.position.x > 9f) return;

        _controller.speed = 28f;
        canvasFirst.enabled = false;
        secondCanvas.enabled = true;
        secondCar.GetComponent<CarController>().speed = 12f;
    }
}
