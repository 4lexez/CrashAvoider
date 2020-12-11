using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Readme))]
[CanEditMultipleObjects]
public class ReadmeEditor : Editor {
	public override void OnInspectorGUI()
	{
		//---------------------------------------------------------------------
		//  README
		//  To enable the pixelation camera, make sure your project has 
		//  a Pixelation render layer. To do so go to 'Edit/Project Settings/
		//  Tags and Layers' and add a new layer named Pixelation to the User
		//  Layers.
		//	Then make sure your main camera has it's Culling Mask set to
		//  everything but the Pixelation layer and your PixelCamera has the
		//  Culling Mask set to nothing but the Pixelation layer.
		//	If unsure consult the PDF manual or contact the author via the
		//  Asset Store.
		//---------------------------------------------------------------------
		EditorGUILayout.HelpBox ("README\nTo enable the pixelation camera, make sure your project has a Pixelation render layer. To do so go to 'Edit/Project Settings/Tags and Layers' and add a new layer named Pixelation to the User Layers.\nThen make sure your main camera has it's Culling Mask set to everything but the Pixelation layer and your PixelCamera has the Culling Mask set to nothing but the Pixelation layer.\nIf unsure consult the PDF manual or contact the author via the Asset Store.", MessageType.Info);
	}
}
#endif
