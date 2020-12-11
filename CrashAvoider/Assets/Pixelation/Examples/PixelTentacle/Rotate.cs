using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public enum axis
	{
		x,
		y,
		z
	}

    public float _speed = 45f;
	public axis rotationAxis = axis.y; 
	
	void Update ()
    {
		switch (rotationAxis) {
		case axis.x:
			transform.Rotate(new Vector3(Time.deltaTime * _speed, 0, 0));
			break;
		case axis.y:
			transform.Rotate(new Vector3(0, Time.deltaTime * _speed, 0));
			break;
		case axis.z:
			transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
			break;
		}
    }
}
