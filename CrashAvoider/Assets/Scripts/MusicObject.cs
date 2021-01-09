
using UnityEngine;
#pragma warning disable 0649
public class MusicObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] ListOfMusic;
    //[SerializeField] private AudioMixer mixer;
    private bool IsSpawned;
    public static MusicObject thisObject;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (thisObject == null)
        {
            thisObject = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ListOfMusic[Random.Range(0, ListOfMusic.Length)];
        GetComponent<AudioSource>().Play();
    }

}



