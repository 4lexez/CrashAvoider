using UnityEngine;

[ExecuteInEditMode]
public class SnapToMainCam : MonoBehaviour {

    private Camera mainCam = null;
    private Camera thisCam = null;

	void Awake () {
        mainCam = Camera.main;
        thisCam = GetComponent<Camera>();
		if (Application.isPlaying && (thisCam == null || mainCam == null || thisCam == mainCam))
		{
			Destroy(this);
			return;
		}
		transform.parent = mainCam.transform;
    }
	
	void Update () {
		if (thisCam == null || mainCam == null)
		{
			return;
		}
		thisCam.clearFlags = CameraClearFlags.Nothing;
        thisCam.projectionMatrix = mainCam.projectionMatrix;
		thisCam.orthographic = mainCam.orthographic;
		thisCam.orthographicSize = mainCam.orthographicSize;
        thisCam.fieldOfView = mainCam.fieldOfView;
        thisCam.nearClipPlane = mainCam.nearClipPlane;
        thisCam.farClipPlane = mainCam.farClipPlane;
		thisCam.rect = mainCam.rect;
		thisCam.depth = mainCam.depth + 1;
        thisCam.renderingPath = RenderingPath.Forward;
        thisCam.useOcclusionCulling = mainCam.useOcclusionCulling;
        thisCam.hdr = mainCam.hdr;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}
}
