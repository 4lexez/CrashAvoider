using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFade : MonoBehaviour
{
    [SerializeField] private Texture2D fading;
    public float fadeSpeed = 0.8f, alpha = 1f, fadeDir = -1;
    private int drawDepth = -1000;

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fading);
    }

    public float Fade(float dir)
    {
        fadeDir = dir;
        return fadeSpeed;
    }
    IEnumerator GuiStatus(float dir, float needAlpha)
    {
        fadeDir = dir;

        while (alpha != needAlpha)
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            yield return new WaitForEndOfFrame();
        }

        
    }
}
