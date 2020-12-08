using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {

    [SerializeField] private AudioClip crash, TurnSignal;
    [SerializeField] private AudioClip[] accelerates;
    public bool rightTurn, leftTurn, moveFromUp;
    public float speed = 15f, force = 50f;
    [SerializeField] private Rigidbody carRb;
    [SerializeField] private float originRotationY, rotateMultRight = 6f, rotateMultLeft = 4.5f;
    [SerializeField] private LayerMask carsLayer;
    public bool isMovingFast, carCrashed, nearCrash;
    [NonSerialized] public bool carPassed;
    [NonSerialized] public static bool isLose;
    [SerializeField] private GameObject turnLeftSignal, turnRightSignal, explosion, exhaust;
    [NonSerialized] public static int countCars;


    private void Start() 
    {
        originRotationY = transform.eulerAngles.y;
        carRb = GetComponent<Rigidbody>(); 

        if (rightTurn)
            StartCoroutine(TurnSignals(turnRightSignal));
        else if (leftTurn)
            StartCoroutine(TurnSignals(turnLeftSignal));
    }

    IEnumerator TurnSignals(GameObject turnSignal) {
        while (!carPassed) {
            turnSignal.SetActive(!turnSignal.activeSelf);
            /*if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = TurnSignal;
                GetComponent<AudioSource>().Play();
            }*/
            yield return new WaitForSeconds(0.5f);
        }
        turnSignal.SetActive(false);
    }

    private void FixedUpdate() {
        carRb.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
    private void OnMouseDown()
    {
        string carName = transform.gameObject.name;
        if (!isMovingFast && !carCrashed && !nearCrash)
        {
            GameObject vfx = Instantiate(exhaust, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
            Destroy(vfx, 2f);
            speed *= 2f;
            isMovingFast = true;
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = accelerates[Random.Range(0, accelerates.Length)];
                GetComponent<AudioSource>().Play();
            }
        }
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Car") && !carCrashed) {
            carCrashed = true;
            IsDead();

            //other.gameObject.GetComponent<CarController>().speed = 0f;

            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfx, 5f);
            
            if (isMovingFast) force *= 1.2f;

            carRb.AddRelativeForce(Vector3.right * force * (speed * 0.1f));
            speed = 0f;
            if (PlayerPrefs.GetString("music") != "No") 
            {
                GetComponent<AudioSource>().clip = crash;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (carCrashed)
            return;
        
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            RotateCar(rotateMultRight);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            RotateCar(rotateMultLeft, -1);
    }

    private void OnTriggerExit(Collider other) {
        if (carCrashed)
            return;
        
        if (other.transform.CompareTag("Trigger Pass")) {
            if(carPassed)
                return;

            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;
            
            countCars++;
        }
        
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY + 90f, 0);
            StopCoroutine(TurnSignals(turnRightSignal));
        }

        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY - 90f, 0);
            StopCoroutine(TurnSignals(turnLeftSignal));
        }

        else if (other.transform.CompareTag("Delete Trigger"))
            Destroy(gameObject);
        }

    private void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed)
            return;

        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotationY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;
        if (dir == 1 && transform.localRotation.eulerAngles.y > originRotationY + 90f)
        {
            return;
        }


            float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
        if (carRb.rotation * deltaRotation == Quaternion.Euler(0, originRotationY + 90f, 0))
        {
            
            return;
        }
        if (carRb.rotation * deltaRotation == Quaternion.Euler(0, originRotationY - 90f, 0))
        {
            return;
        }
        carRb.MoveRotation(carRb.rotation * deltaRotation);

    }
    private void IsDead() { GameController.ActionDead?.Invoke(); }
    public void CallCoroutine(int NeedSpeed = 0, bool IsCrash = true)
    {
        StartCoroutine(NearCar(NeedSpeed, IsCrash));
    }
    private IEnumerator NearCar(int NeedSpeed = 0, bool IsCrash = true)
    {
        while (speed >= NeedSpeed)
        {
            speed = speed * 0.8f - Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        nearCrash = IsCrash;
    }
}
