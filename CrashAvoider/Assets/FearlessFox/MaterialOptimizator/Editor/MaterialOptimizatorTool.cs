using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialOptimizatorTool : EditorWindow
{
  private class MaterialSetting
  {
    public Material material;
    public bool moreInfo;
    public List<GameObject> objects = new List<GameObject>();
    public List<int> indexMaterial = new List<int>();
    public Shader newShader;
    public Material newMaterial;

    public MaterialSetting(Material material)
    {
      this.material = material;
      moreInfo = false;
    }

    public void AddGameObject(GameObject newObj, int index)
    {
      objects.Add(newObj);
      indexMaterial.Add(index);
    }
  }

  private List<MaterialSetting> materialsScene = new List<MaterialSetting>();

  private MeshRenderer[] meshesScene;
  private SkinnedMeshRenderer[] skinneMeshesScene;

  private Vector2 scrollPos;

  [MenuItem("Window/FearlessFox/Material Optimizator")]
  public static void AutoWardrobeSettings()
  {
    EditorWindow editorWindow = GetWindow(typeof(MaterialOptimizatorTool));

    editorWindow.title = "Material Optimizator";
    editorWindow.minSize = new Vector2(500, 500);
  }

  private MaterialSetting ContainsMaterial(Material newMaterial)
  {
    for (int i = 0; i < materialsScene.Count; i++)
    {
      if (materialsScene[i].material == newMaterial)
      {
        return materialsScene[i];
      }
    }
    return null;
  }

  private void BuildListMaterials()
  {
#pragma warning disable
    for (int i = 0; i < materialsScene.Count; i++)
    {
      GUILayout.BeginHorizontal(@"Box");

      if (materialsScene[i].material != null)
      {
        GUILayout.Label("Current", EditorStyles.boldLabel);
        Material material = EditorGUILayout.ObjectField(materialsScene[i].material, typeof(Material), false)as Material;
        Shader shader = EditorGUILayout.ObjectField(materialsScene[i].material.shader, typeof(Shader), false)as Shader;
      }

      GUILayout.Label("New", EditorStyles.boldLabel);

      materialsScene[i].newMaterial = EditorGUILayout.ObjectField(materialsScene[i].newMaterial, typeof(Material), false)as Material;

      if (GUILayout.Button("Apply"))
      {
        if (materialsScene[i].newMaterial != null && materialsScene[i].objects.Count > 0)
        {
          for (int j = 0; j < materialsScene[i].objects.Count; j++)
          {
            MeshRenderer meshRenderer = materialsScene[i].objects[j].GetComponent<MeshRenderer>();
            int index = materialsScene[i].indexMaterial[j];
            if (meshRenderer != null)
            {
              Material[] auxArray = meshRenderer.sharedMaterials;
              auxArray[index] = materialsScene[i].newMaterial;
              meshRenderer.sharedMaterials = auxArray;
            }
            else
            {
              SkinnedMeshRenderer skinedmeshRenderer = materialsScene[i].objects[j].GetComponent<SkinnedMeshRenderer>();
              Material[] auxArray = skinedmeshRenderer.sharedMaterials;
              auxArray[index] = materialsScene[i].newMaterial;
              meshRenderer.sharedMaterials = auxArray;
            }
          }
          SearchMaterials();
        }
      }

      materialsScene[i].newShader = EditorGUILayout.ObjectField(materialsScene[i].newShader, typeof(Shader), false)as Shader;

      if (GUILayout.Button("Apply"))
      {
        if (materialsScene[i].newShader != null)
        {
          materialsScene[i].material.shader = materialsScene[i].newShader;
          SearchMaterials();
        }
      }

      GUILayout.EndHorizontal();
      materialsScene[i].moreInfo = EditorGUILayout.Foldout(materialsScene[i].moreInfo, "");

      if (materialsScene[i].moreInfo == true)
      {
        for (int j = 0; j < materialsScene[i].objects.Count; j++)
        {
          GameObject obj = EditorGUILayout.ObjectField(materialsScene[i].objects[j], typeof(GameObject), false)as GameObject;
        }
      }
    }
#pragma warning restore
  }

  private void SearchMaterials()
  {
    materialsScene.Clear();

    meshesScene = FindObjectsOfType<MeshRenderer>();
    skinneMeshesScene = FindObjectsOfType<SkinnedMeshRenderer>();

    if (meshesScene.Length > 0)
    {
      for (int i = 0; i < meshesScene.Length; i++)
      {
        for (int j = 0; j < meshesScene[i].sharedMaterials.Length; j++)
        {
          MaterialSetting mat = ContainsMaterial(meshesScene[i].sharedMaterials[j]);
          if (mat == null)
          {
            MaterialSetting aux = new MaterialSetting(meshesScene[i].sharedMaterials[j]);
            materialsScene.Add(aux);
            aux.AddGameObject(meshesScene[i].gameObject, j);
          }
          else
            mat.AddGameObject(meshesScene[i].gameObject, j);
        }
      }
    }

    if (skinneMeshesScene.Length > 0)
    {
      for (int i = 0; i < skinneMeshesScene.Length; i++)
      {
        for (int j = 0; j < skinneMeshesScene[i].sharedMaterials.Length; j++)
        {
          MaterialSetting mat = ContainsMaterial(skinneMeshesScene[i].sharedMaterials[j]);
          if (mat == null)
          {
            MaterialSetting aux = new MaterialSetting(skinneMeshesScene[i].sharedMaterials[j]);
            materialsScene.Add(aux);
            aux.AddGameObject(skinneMeshesScene[i].gameObject, j);
          }
          else
            mat.AddGameObject(skinneMeshesScene[i].gameObject, j);
        }
      }
    }
  }

  private void OnGUI()
  {
    scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
    GUILayout.Space(10.0f);
    GUILayout.BeginVertical(@"Box");
    GUILayout.BeginHorizontal(@"Box");

    if (GUILayout.Button("Search"))
    {
      SearchMaterials();
    }

    if (GUILayout.Button("Clear"))
    {
      materialsScene.Clear();
    }

    GUILayout.EndHorizontal();

    GUILayout.Space(5f);

    if (materialsScene.Count > 0)
    {
      GUI.color = Color.blue;
      GUILayout.Label("Materials: " + materialsScene.Count.ToString());
      GUI.color = Color.white;

      GUILayout.Space(5f);
      BuildListMaterials();
    }

    GUILayout.EndVertical();
    GUILayout.EndScrollView();
  }
}