using UnityEngine;
using System.Collections;

public class PixelSizeOscillator : MonoBehaviour {

	public PixelationPost pixelCam;
	public float startValue;
	public float endValue;
	public float speed;

	void Update () {
		pixelCam._cellSize = Mathf.Lerp (startValue, endValue, (Mathf.Sin (Time.fixedTime * speed) + 1) / 2);
	}
}
