using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
//using Managers;
//using Patterns;
using UnityEngine.SceneManagement;

public class ScenesOpen : EditorWindow
{
#if UNITY_EDITOR
	[MenuItem("SceneManager/OpenSceneWindow")]
	public static void Show() 
	{
		string[] s = EditorBuildSettings.scenes
			 .Where(scene => scene.enabled)
			 .Select(scene => scene.path)
			 .ToArray();
		float scale = 32f * (float)s.Length+1;
		var window = (ScenesOpen)EditorWindow.GetWindowWithRect(typeof(ScenesOpen), new Rect(0, 0, 150, scale));
		GetWindow<ScenesOpen>("ScenesOpener");
	}
	
	private string current = null;
	private string[] scenes;
	private string pathH = "Assets";
	private void GetLIst() 
	{
		scenes = EditorBuildSettings.scenes
				 .Where(scene => scene.enabled)
				 .Select(scene => scene.path)
				 .ToArray();
	}

	private void Update()
	{
		Load();
	}

	private void Load() 
	{
		if (!EditorApplication.isPlaying)
		{
			if (!string.IsNullOrEmpty(current))
			{
				EditorApplication.OpenScene(current);
				current = null;
			}
		}
	}

	private int idScene;
	private int iteration;
	public static int IdScene;
	
	private void OnGUI()
	{
		GetLIst();
		GUI.backgroundColor = Color.green;
		var style = new GUIStyle(GUI.skin.button);
		style.fontSize = 20;
		style.fontStyle = FontStyle.Bold;
		style.fixedHeight = 30;
		
		/*if (GUILayout.Button("Start", style))// GUILayout.Height(30)))
		{
			string path = "Assets/Scripts/.txt";
			current = EditorApplication.currentScene;
			idScene = EditorSceneManager.GetActiveScene().buildIndex;
			EditorApplication.SaveCurrentSceneIfUserWantsTo();
			EditorApplication.OpenScene(scenes[0]);
			EditorApplication.EnterPlaymode();
			iteration = 0;
			
			foreach (TrackDefinition track in Singleton<ResourceManager>.Instance.tracks)
			{
				if (track.buildIndex == idScene)
				{
					StreamWriter writer = new StreamWriter(path, false);
					writer.WriteLine(iteration);
					writer.Close();
				}
				iteration++;
			}
		}*/

		//GUILayout.BeginHorizontal();
		//{
		//	if (GUILayout.Button("Folder Scenes", style))// GUILayout.Height(30)))
		//	{
		//		//TextAsset text = new TextAsset();
		//		//AssetDatabase.CreateAsset(text, "Assets/" +  "" + ".asset");
		//		/*Type pwuType = typeof(ProjectWindowUtil);
		//		pathH = Convert.ToString(pwuType.GetMethod("GetActiveFolderPath", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null)); 
		//		
		//		string path = "Assets/Scenes/LaunchNewUI.unity";
		//
		//		if (path[path.Length - 1] == '/')
		//			path = path.Substring(0, path.Length - 1);
		//
		//		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
		//		EditorGUIUtility.PingObject(obj);*/
		//	}
		//
		//	if (GUILayout.Button("Back to Folder Prev", style))
		//	{
		//		if (pathH[pathH.Length - 1] == '/')
		//			pathH = pathH.Substring(0, pathH.Length - 1);
		//
		//		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(pathH, typeof(UnityEngine.Object));
		//		EditorGUIUtility.PingObject(obj);
		//	}
		//}
		//
		//GUILayout.EndHorizontal();
		GUI.backgroundColor = Color.white;
		var styleA = new GUIStyle(GUI.skin.button);
		styleA.fontSize = 15;
		styleA.fontStyle = FontStyle.Bold;
		styleA.fixedHeight = 30;
		
		foreach (var item in scenes)
		{
			string[] a = item.Split('/');
			string b = a[a.Length-1].Split('.')[0];
			if (GUILayout.Button(b, styleA))// GUILayout.Height(30)))
			{
				EditorSceneManager.OpenScene(item);
			}
		}
	}
#endif
}

/*[MenuItem("Example/Quit")]
	static void Init()
	{
		int option = EditorUtility.DisplayDialogComplex("Unsaved Changes",
			"Do you want to save the changes you made before quitting?",
			"Save",
			"Cancel",
			"Don't Save");

		switch (option)
		{
			// Save.
			case 0:
				EditorApplication.SaveScene(EditorApplication.currentScene);
				EditorApplication.Exit(0);
				break;

			// Cancel.
			case 1:
				break;

			// Don't Save.
			case 2:
				EditorApplication.Exit(0);
				break;

			default:
				Debug.LogError("Unrecognized option.");
				break;
		}
	}*/
