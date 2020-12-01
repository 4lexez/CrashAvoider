using UnityEngine;

public class UfoBonus : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (UFO.isUfo == true)
        {
            print("UFO");
            Rigidbody obg = gameObject.GetComponent<Rigidbody>();
            obg.useGravity = false;
            obg.AddForce(new Vector3(0, 25f, 0), ForceMode.Impulse);
            UFO.isUfo = false;
            Destroy(obg.gameObject.GetComponent<CarController>());
        }
    }
}
