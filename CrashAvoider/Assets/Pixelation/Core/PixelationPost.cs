using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class PixelationPost : MonoBehaviour
{
    public Shader _shader;

	[Range(0.0001f, 100.0f)]
    public float _cellSize = 0.025f;

    [Range(0, 8)]
    public int _colorBits = 8;

    private static Material mat = null;

    protected Material material
    {
        get
        {
            if (mat == null)
            {
                mat = new Material(_shader);
				mat.hideFlags = HideFlags.HideAndDontSave;
            }
            return mat;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
		if (_shader == null)
		{
			return;
		}
        material.SetFloat("_CellSize", _cellSize * 0.01f);
        material.SetFloat("_ColorBits", _colorBits);
        Graphics.Blit(src, dest, material);
    }
}
