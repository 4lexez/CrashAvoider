using System;
using UnityEngine;

public class DeleteHorn : MonoBehaviour {

    [SerializeField] private float timeToDelete = 2f;

    private void Start() {
        Destroy(gameObject, timeToDelete);
    }

}
