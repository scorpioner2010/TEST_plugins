using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using UnityEngine;
using System.IO;

public class CopyPath : Editor
{
    [MenuItem("Assets/CopyPath")]
    public static void CopyPathObject()
    {
        string appP = Application.dataPath;
        string pathO = AssetDatabase.GetAssetPath(Selection.activeObject);
        string res = appP.Replace("Assets", pathO);
        EditorGUIUtility.systemCopyBuffer = res;
    }

    [MenuItem("Assets/MaterialName")]
    public static void CreateMaterial()
    {
        /*string appP = Application.dataPath;
        string pathO = AssetDatabase.GetAssetPath(Selection.activeObject);
        string res = appP.Replace("Assets", pathO);
        EditorGUIUtility.systemCopyBuffer = res;*/

        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      //  string pathO = AssetDatabase.GetAssetPath(Selection.activeObject);
        Debug.Log(GetClickedDirFullPath());

        if (Selection.activeObject)
        {
            AssetDatabase.CreateAsset(material, GetClickedDirFullPath() + "/" + Selection.activeObject.name + ".mat");
        }
        else
        {
            AssetDatabase.CreateAsset(material, GetClickedDirFullPath() + "/Material.mat");
        }    
        //
    }

    private static string GetClickedDirFullPath()
    {
        string clickedAssetGuid = Selection.assetGUIDs[0];
        string clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);

        FileAttributes attr = File.GetAttributes(clickedPath);
        return attr.HasFlag(FileAttributes.Directory) ? clickedPath : Path.GetDirectoryName(clickedPath);
    }
}
