
using UnityEngine;

public class NewFade : MonoBehaviour
{
    Texture2D blk;
    public bool fade;
    public float alph;
    void Start()
    {
        blk = new Texture2D(1, 1);
        blk.SetPixel(0, 0, new Color(0, 0, 0, 0));
        blk.Apply();
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
    }

    void Update()
    {
        if (!fade)
        {
            if (alph > 0)
            {
                alph -= Time.deltaTime * .2f;
                if (alph < 0) { alph = 0f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
            }
        }
        if (fade)
        {
            if (alph < 1)
            {
                alph += Time.deltaTime * .2f;
                if (alph > 1) { alph = 1f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
            }
        }
    }
    public void FadeChanger()
    {
        fade = !fade;
    }
}
