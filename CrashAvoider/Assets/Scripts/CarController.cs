using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
#pragma warning disable 0649
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {
    #region Animator
    private Animator BlinkLight;
    #endregion
    #region Layers
    [SerializeField] private LayerMask carsLayer;
    #endregion
    #region Audio

    [SerializeField] private AudioClip[] accelerates, crash;
    #endregion
    #region Bools
    [NonSerialized] public bool carPassed;
    [NonSerialized] public static bool isLose;
    public bool isMovingFast, carCrashed, nearCrash;
    public bool rightTurn, leftTurn, moveFromUp;
    public bool IsPerkUsed;
    #endregion
    #region Rigidbody
    [SerializeField] private Rigidbody carRb;
    #endregion
    #region Floats
    [SerializeField] private float originRotationY, rotateMultRight = 6f, rotateMultLeft = 4.5f;
    public float speed = 15f, force = 50f;
    [NonSerialized] public static int countCars;
    #endregion
    #region GameObjects
    [SerializeField] private GameObject explosion, exhaust;
    [SerializeField] private GameObject TurnLights, EngineParticle;
    #endregion
    #region MeshReformer
    /*public bool useDamage = true;
    public float damageRadius = 0.5f;
    private Vector3 localVector;
    public float maximumDamage = 0.5f;
    private float minimumCollisionForce = 5f;
    public float randomizeVertices = 1f;
    public float damageMultiplier = 1f;
    public MeshFilter[] deformableMeshFilters;
    private CarController.originalMeshVerts[] originalMeshData;
    public LayerMask damageFilter = -1;
    public bool repaired = true;
    private Quaternion rot = Quaternion.identity;*/
    #endregion
    public SkinManager managerSkin;



    private void Start() 
    {
        #region GetComponents
        BlinkLight = GetComponent<Animator>();
        carRb = GetComponent<Rigidbody>();
        #endregion
        originRotationY = transform.eulerAngles.y;
        #region Blinks
        if (rightTurn)
            BlinkLight.SetBool("Right", rightTurn);
        else if (leftTurn)
            BlinkLight.SetBool("Left", leftTurn);
        #endregion
    }

    private void FixedUpdate()
    {
        carRb.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
    #region OnCarClick
    private void OnMouseDown()
    {
        if (!isMovingFast && !carCrashed && !nearCrash)
        {
            GameObject vfx = Instantiate(exhaust, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
            Destroy(vfx, 0.75f);
            speed *= 2f;
            isMovingFast = true;
            IsPerkUsed = true;
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = accelerates[Random.Range(0, accelerates.Length)];
                GetComponent<AudioSource>().Play();
            }
        }
    }
    #endregion



    private void OnTriggerStay(Collider other) {
        if (carCrashed)
            return;
        #region MakeTurn
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            RotateCar(rotateMultRight);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            RotateCar(rotateMultLeft, -1);
        #endregion
    }

    private void OnTriggerExit(Collider other) {
        if (carCrashed)
            return;
        #region CarHadDoneTheWay
        if (other.transform.CompareTag("Trigger Pass"))
        {
            if (carPassed)
                return;

            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;

            countCars++;
        }
        #endregion
        #region CarTurned
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY + 90f, 0);
            BlinkLight.SetBool("Right", false);
        }

        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY - 90f, 0);
            BlinkLight.SetBool("Left", false);
        }
        #endregion
        else if (other.transform.CompareTag("Delete Trigger"))
            Destroy(gameObject);
        }
    #region Rotating
    private void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed)
            return;




        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
        Quaternion Rotation = carRb.rotation * deltaRotation;
        //Pizda, eto vse proverki na ygol povorota
        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotationY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;
        if (dir == 1 && transform.localRotation.eulerAngles.y > originRotationY + 90f)
        {
            return;
        }
        /*if (Rotation == Quaternion.Euler(0, originRotationY + 90f, 0))
        {

            return;
        }
        if (Rotation == Quaternion.Euler(0, originRotationY - 90f, 0))
        {
            return;
        }*/
        carRb.MoveRotation(Rotation);

    }
    #endregion

    #region DeadOrNearCrash
    private void IsDead() { GameController.ActionDead?.Invoke(); }
    public void CallCoroutine(int NeedSpeed = 0, bool IsCrash = true)
    {
        StartCoroutine(NearCar(NeedSpeed, IsCrash));
    }
    private IEnumerator NearCar(int NeedSpeed = 0, bool IsCrash = true)
    {
        while (speed >= NeedSpeed)
        {
            speed = isMovingFast ? (speed * 0.6f) - Time.deltaTime : (speed * 0.8f) - Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        nearCrash = IsCrash;
        if(IsCrash) speed = 0;
        StartCoroutine(BecameStatic(10));
    }

    private IEnumerator BecameStatic(int time)
    {
        yield return new WaitForSeconds(time);
        TurnLights.SetActive(false);
        EngineParticle.SetActive(false);

        carRb.IsSleeping();
        gameObject.isStatic = true;

    }
    #endregion
    #region Deform
    /*private void DeformMesh(Mesh mesh, Vector3[] originalMesh, Collision collision, float cos, Transform meshTransform, Quaternion rot)
    {
        print("deformed");
        Vector3[] vertices = mesh.vertices;
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 point = meshTransform.InverseTransformPoint(contact.point);
            for (int i = 0; i < vertices.Length; i++)
            {
                if ((point - vertices[i]).magnitude < this.damageRadius)
                {
                    vertices[i] += rot * (this.localVector * (this.damageRadius - (point - vertices[i]).magnitude) / this.damageRadius * cos + new Vector3(Mathf.Sin(vertices[i].y * 1000f), Mathf.Sin(vertices[i].z * 1000f), Mathf.Sin(vertices[i].x * 100f)).normalized * (this.randomizeVertices / 500f));
                    if (this.maximumDamage > 0f && (vertices[i] - originalMesh[i]).magnitude > this.maximumDamage)
                    {
                        vertices[i] = originalMesh[i] + (vertices[i] - originalMesh[i]).normalized * this.maximumDamage;
                    }
                }
            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void LoadOriginalMeshData()
    {
        this.originalMeshData = new CarController.originalMeshVerts[this.deformableMeshFilters.Length];
        for (int i = 0; i < this.deformableMeshFilters.Length; i++)
        {
            this.originalMeshData[i].meshVerts = this.deformableMeshFilters[i].mesh.vertices;
        }
    }
    private struct originalMeshVerts
    {
        // Token: 0x040009AE RID: 2478
        public Vector3[] meshVerts;
    }*/
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        #region CollisionWithCar+Deform
        if (collision.gameObject.CompareTag("Car") && !carCrashed)
        {
            #region DeformCarAfterCrash
            /*if (other.contacts.Length < 1 || other.relativeVelocity.magnitude < this.minimumCollisionForce)
            {
                return;
            }

            if (this.useDamage && (1 << collision.gameObject.layer & this.damageFilter) != 0)
            {
                Vector3 colRelVel = collision.relativeVelocity;
                colRelVel *= 1f - Mathf.Abs(Vector3.Dot(base.transform.up, collision.contacts[0].normal));
                float cos = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, colRelVel.normalized));
                //if (colRelVel.magnitude * cos >= this.minimumCollisionForce)
                //{
                this.repaired = false;
                this.localVector = base.transform.InverseTransformDirection(colRelVel) * (this.damageMultiplier / 50f);
                if (this.originalMeshData == null)
                {
                    this.LoadOriginalMeshData();
                }
                for (int i = 0; i < this.deformableMeshFilters.Length; i++)
                {
                    this.DeformMesh(this.deformableMeshFilters[i].mesh, this.originalMeshData[i].meshVerts, collision, cos, this.deformableMeshFilters[i].transform, this.rot);
                }

                }
            }*/
    #endregion
    carCrashed = true;
            IsDead();
            
            GameObject vfx = Instantiate(explosion, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(vfx, 5f);

            if (isMovingFast) force *= 1.2f;
            TurnLights.SetActive(false);
            EngineParticle.SetActive(false);
            carRb.AddRelativeForce(Vector3.right * force * (speed * 0.1f));
            speed = 0f;
            StartCoroutine(BecameStatic(15));
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = crash[Random.Range(0, crash.Length)];
                GetComponent<AudioSource>().Play();
            }
        }
        if (collision.gameObject.CompareTag("Delete"))
        {
            Destroy(this.gameObject);
        }
        #endregion

    }
    public delegate void onRCCPlayerCollision(CarController RCC, Collision collision);
}
