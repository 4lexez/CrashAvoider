using UnityEngine;

public class SimpleOscillator : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public float speed;

	void Update () {
		transform.position = Vector3.Lerp (startPosition, endPosition, (Mathf.Sin (Time.fixedTime * speed) + 1) / 2);
	}
}
