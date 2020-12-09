using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable 0649
public class Fading : MonoBehaviour
{

    [SerializeField] private Texture2D fading;
    private float fadeSpeed = 0.8f, alpha = 1f, fadeDir = -1;
    private int drawDepth = -1000;

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), fading);
    }

    public float Fade(float dir)
    {
        fadeDir = dir;
        return fadeSpeed;
    }

}
