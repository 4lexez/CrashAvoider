using UnityEngine;

public abstract class SetSkin : MonoBehaviour
{
    [SerializeField] protected MeshRenderer mesh;
    [SerializeField] protected string hash;
    protected SkinManager skinManager;
    public static int skinNumber;

    public void Load()=> skinNumber = PlayerPrefs.GetInt(hash);
    public void Save()=> PlayerPrefs.SetInt(hash, skinNumber);
    public abstract Skin Set(); 
    public SkinManager manager;

    public void Start()
    {
        skinManager = GameObject.Find("SkinManager").GetComponent<SkinManager>();
        Load();
        SetSkins();
    }

    public void SetSkins(Skin skin = null)
    {
        if (skin == null) skin = Set();
        mesh.sharedMaterials = skin.materials;
        Save();
    }
}
