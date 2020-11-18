using UnityEngine;
using System.Collections;

public class MoveBuzzer : MonoBehaviour {

    public int factor = 1;
    private Vector3 startingPos;
    void Start() {
        startingPos = transform.position;
    }

	void Update () {
        transform.position = startingPos + new Vector3(0, factor*Mathf.Sin(Time.time), 0);
	}
}
