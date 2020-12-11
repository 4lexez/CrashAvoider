
using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{
    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    public static Fps thisObject;
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
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }


    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 100, 100, 60, 20), fps);
        //GUI.Label(new Rect(Screen.width - 100, 50, 50, 20), fps);
    }
}