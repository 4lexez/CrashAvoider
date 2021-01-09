using UnityEngine;

public class RotateCarDemo : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0,0.15f,0);
    }
}
