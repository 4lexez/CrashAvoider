using System;
using UnityEngine;

public class DeleteHorn : MonoBehaviour {

    public float timeToDelete = 2f;

    private void Start() {
        Destroy(gameObject, timeToDelete);
    }

}
