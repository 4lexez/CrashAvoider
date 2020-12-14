using UnityEngine;

public class RotateCarDemo : MonoBehaviour
{
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        transform.Rotate(0,0.15f,0);
    }
}
