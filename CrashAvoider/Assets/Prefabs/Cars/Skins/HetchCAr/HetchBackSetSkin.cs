using UnityEngine;

public class HetchBackSetSkin : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private bool isNotActive;
    public static int skinNumber;

    private void Start()
    {
        if (!isNotActive)
        {
            SkinManager skinManager = GameObject.Find("SkinManager").GetComponent<SkinManager>();
            SetSkins(skinManager.skins[skinNumber]);
            Debug.Log(skinNumber);
        }
    }

    public void SetSkins(Skin skin)
    {
            mesh.sharedMaterials = skin.materials;
    }
}