using System.Collections;
using UnityEngine;
using UnityEditor;

public class MakeOfVehicle
{
    [MenuItem("Assets/Create/My_Scriptable_Object")]
    public static void CreateMyAsset()
    {
        Vehicle asset = ScriptableObject.CreateInstance<Vehicle>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    
}
