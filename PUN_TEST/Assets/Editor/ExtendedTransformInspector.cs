using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Transform), true)]
public class ExtendedTransformInspector : Editor
{
	static public ExtendedTransformInspector instance;
	static bool UniformScaling = false;

	SerializedProperty mPos;
	SerializedProperty mRot;
	SerializedProperty mScale;

	static GameObject cam;
	static float lenght;

	/*static Object objS;

	[MenuItem("CONTEXT/Object/Copy")]
	static void CopyComp(Component command)
	{
		Debug.LogError(command);
		//objS = command.context;
		Rigidbody body = (Rigidbody)command.context;
		body.mass = body.mass * 2;
	}*/

	/*[MenuItem("CONTEXT/Object/Past")]
	static void PastComp(MenuCommand command)
	{
		command.context = objS;
		Debug.LogError(command.context);

		Rigidbody body = (Rigidbody)command.context;
		body.mass = body.mass * 2;
	}*/

	[MenuItem("Window/Custom Tools/ShowBB")]
	public static void Enable()
	{
		isShowBB =! isShowBB;
	}

	/*[MenuItem("Window/Custom Tools/Enable")]
	public static void Enable()
	{
		SceneView.duringSceneGui += OnSceneGUI;
	}

	[MenuItem("Window/Custom Tools/Disable")]
	public static void Disable()
	{
		SceneView.duringSceneGui -= OnSceneGUI;
	}

	private static void OnSceneGUI(SceneView sceneview)
	{
		Handles.BeginGUI();
	

		Handles.EndGUI();
	}*/

	void OnEnable ()
	{
		instance = this;

		mPos = serializedObject.FindProperty("m_LocalPosition");
		mRot = serializedObject.FindProperty("m_LocalRotation");
		mScale = serializedObject.FindProperty("m_LocalScale");
		originalValue = mScale.vector3Value;
        if (resszzzzz)
        {
			FindCam();
		}
		
	}

	void OnDestroy ()
	{
		/*if (cam != null && cam != Selection.activeGameObject)
		{
			if (cam.GetComponent<CameraTrack>())
			{
				cam.GetComponent<CameraTrack>().getCamera = false;
			}
			else if (cam.GetComponentInParent<CameraTrack>())
			{
				cam.GetComponentInParent<CameraTrack>().getCamera = false;
			}	
			
			//DestroyImmediate(cam.GetComponent<UniversalAdditionalCameraData>());
			//DestroyImmediate(cam.GetComponent<Camera>());
			
			cam = null;
		}
		instance = null;
		*/
	}

	public override void OnInspectorGUI ()
	{
        EditorGUIUtility.labelWidth = 15f;

		serializedObject.Update();

		bool widgets = false;

		DrawPosition();
		DrawRotation(widgets);
		DrawScale(widgets);

      /*  foreach (var item in Selection.gameObjects)
        {
			if (!item.GetComponent<PositionPivotPlayer>() && item.GetComponent<SkinnedMeshRenderer>())
			{
				if (item.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == "Shader Graphs/Standart_Pilot")
				{
					PositionPivotPlayer vv = item.AddComponent<PositionPivotPlayer>();
					vv.mat = item.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
				}
			}
		}*/

		foreach (var item in Selection.gameObjects)
        {
            foreach (var itema in item.GetComponentsInChildren<MeshRenderer>())
            {
				if (!itema.GetComponent<MeshCollider>() && itema)
				{
					if (itema.GetComponent<MeshRenderer>().gameObject.name.Contains("_COL"))
					{
						MeshCollider vv = itema.gameObject.AddComponent<MeshCollider>();
						//itema.GetComponent<MeshRenderer>().enabled = false;
						DestroyImmediate(itema.GetComponent<MeshFilter>());
						DestroyImmediate(itema.GetComponent<MeshRenderer>());
						
						//item.GetComponent<MeshCollider>().material = (PhysicMaterial)AssetDatabase.LoadAssetAtPath("Assets/Art/OLD/Materials/Physics Materials/Road.physicMaterial", typeof(PhysicMaterial));
					}
				}
			}
		}

		CopyPast();
		CopyPastButton();
		serializedObject.ApplyModifiedProperties();
		Dr();
	}

	/*Gizmos.color = new Color(0, 0, 1, 0.5f);
		Gizmos.DrawCube(Selection.activeGameObject.transform.position, Vector3.on);*/

	static Vector3 posCopyButton;
	static Quaternion rotCopyButton;
	static Vector3 scaleCopyButton;

	public enum OPTIONS
	{
		Y = 0,
		gY = 1
	}
	public static OPTIONS op = (OPTIONS)0;
	public static Color ccccc;
	public static Vector3 ccacasc;
	public static bool ress;
	public static bool resszzzzz;
	public static bool rest;
	float hBack = 4;
	void CopyPastButton() 
	{
		/*GUILayout.BeginHorizontal();
		ccccc = EditorGUILayout.ColorField(ccccc, GUILayout.Height(20f));
		GUILayout.EndHorizontal();*/
		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		GUI.backgroundColor = new Color(1,1,0);
		bool cL = GUILayout.Button("Copy Local", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		bool pL = GUILayout.Button("Past Local", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		GUI.backgroundColor = new Color(0, 1, 1);
		bool cW = GUILayout.Button("Copy World", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		bool pW = GUILayout.Button("Past World", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		GUI.backgroundColor = new Color(0, 1, 0);
		GUILayout.BeginHorizontal();
		bool pprY = GUILayout.Button("PosRot Y", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		GUI.backgroundColor = new Color(1, 0, 1);
		bool pprYa = GUILayout.Button("Get H", GUILayout.Height(20f), GUILayout.Width(50), GUILayout.MinWidth(30f));
		GUILayout.EndHorizontal();
		GUI.backgroundColor = new Color(0, 1, 0);
		GUILayout.BeginHorizontal();
		bool prY = GUILayout.Button("Rot Y", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		bool ppY = GUILayout.Button("Pos Y", GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		GUI.backgroundColor = new Color(1,1,1);
		if (Selection.activeGameObject) {
			GUILayout.BeginHorizontal();
			lenght =  EditorGUILayout.FloatField("H", lenght, GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
			rest = GUILayout.Toggle(rest, "h_ByS", GUILayout.Height(20f), GUILayout.Width(55f), GUILayout.MinWidth(30f));
			GUILayout.EndHorizontal();
		}
		op = (OPTIONS)EditorGUILayout.EnumPopup("Up", op, GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));
		this.Repaint();
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		bool res = GUILayout.Button("reset", GUILayout.Height(20f), GUILayout.Width(45f), GUILayout.MinWidth(30f));
		
		ress = GUILayout.Toggle(ress, "s", GUILayout.Height(20f), GUILayout.Width(45f), GUILayout.MinWidth(30f));
		if (res)
        {
			ccacasc = Vector3.zero;

		}
		resszzzzz = GUILayout.Toggle(resszzzzz, "F", GUILayout.Height(20f), GUILayout.Width(45f), GUILayout.MinWidth(30f));
		if (Selection.activeGameObject)
		{
			ccacasc = EditorGUILayout.Vector3Field("", ccacasc, GUILayout.Height(20f), GUILayout.ExpandWidth(true), GUILayout.MinWidth(30f));

		}
		GUILayout.EndHorizontal();

		if (cL)
        {
			Undo.RecordObject((Transform)target, "SnapObject");
			var va = (Transform)target;
			posCopyButton = va.localPosition;
			rotCopyButton = va.localRotation;
			scaleCopyButton = va.localScale;
		}

		if (pL)
		{
			Undo.RecordObject((Transform)target, "SnapObject");
			var va = (Transform)target;
			va.localPosition = posCopyButton;
			va.localRotation = rotCopyButton;
			va.localScale = scaleCopyButton;
		}

		if (cW)
		{
			Undo.RecordObject((Transform)target, "SnapObject");
			var va = (Transform)target;
			posCopyButton = va.position;
			rotCopyButton = va.rotation;
			scaleCopyButton = va.localScale;
		}

		if (pW)
		{
			Undo.RecordObject((Transform)target, "SnapObject");
			var va = (Transform)target;
			va.position = posCopyButton;
			va.rotation = rotCopyButton;
			va.localScale = scaleCopyButton;
		}

        if (pprY || prY || ppY || ress || pprYa)
        {
			Dictionary<Collider, bool> colliders = new Dictionary<Collider, bool>();

			foreach (GameObject selected in Selection.gameObjects)
			{
				foreach (var item in selected.GetComponentsInChildren<Collider>())
				{
					colliders.Add(item, item.enabled);
					item.enabled = false;
				}
			}

			foreach (GameObject selected in Selection.gameObjects)
			{
				Undo.RecordObject(selected.transform, "SnapObject");

				bool isConnect = Physics.Raycast(selected.transform.position +( op == OPTIONS.gY? Vector3.up * hBack : selected.transform.up * hBack), (op == OPTIONS.gY ? -Vector3.up : -selected.transform.up), out RaycastHit hit, 50);

				if (isConnect)
				{
					Vector3 normal = hit.normal;
					Vector3 right = Vector3.Cross(normal, selected.transform.forward).normalized;
					Vector3 forward = Vector3.Cross(right,normal).normalized;
                    if (pprY || prY || ress)
                    {
						selected.transform.rotation = (op == OPTIONS.gY ? Quaternion.LookRotation(forward, normal) * Quaternion.Euler(ccacasc) : Quaternion.LookRotation(forward, normal) * Quaternion.Euler(ccacasc));
					}
					
                    if (pprY || ppY || ress)
                    {
						selected.transform.position = hit.point + (rest?(hit.normal * lenght ) *(Vector3.Dot(selected.transform.localScale, Vector3.one) / 3) : (hit.normal * lenght));
					}
                    if (pprYa)
                    {
						lenght = rest ? (hit.distance - hBack) / (Vector3.Dot(selected.transform.localScale, Vector3.one) / 3) : hit.distance - hBack;

					}
				}
				else
				{
					Debug.LogError("Not connect!");
				}
			}

			foreach (var item in colliders)
			{
				item.Key.enabled = item.Value;
			}
		}

		/*GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("P", GUILayout.Width(43f));

			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));

			if (reset) mPos.vector3Value = Vector3.zero;
		}
		GUILayout.EndHorizontal();*/
	}
	void FindCam()
	{
		//Selection.activeGameObject = Camera.main.gameObject;
		SceneView.FrameLastActiveSceneView();
	}
	void OnSceneGUI()
    {
		CopyPast();
		Dr();

		Handles.BeginGUI();

		/*if (GUILayout.Button("Button")) 
		{
			a = !a;
		}*/
	//	Handles.Button(Selection.activeGameObject.transform.position, "Hello World!",1);
		
		Handles.EndGUI();

	/*	Transform buttonExample = (Transform)target;

		Vector3 position = buttonExample.position + Vector3.up * 2f;
		float size = 2f;
		float pickSize = size * 2f;

		if (Handles.Button(position, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
			Debug.Log("The button was pressed!");*/
	}



	static bool isShowBB;

	void Dr() 
	{
		Color cl = new Color(1,1,0,0.5f);
		if (Selection.activeGameObject && isShowBB)
		{
			foreach (var obj in Selection.gameObjects)
			{
				var r = obj.GetComponentsInChildren<Renderer>();
				foreach (var item in r)
				{
					if (item == null)
						return;
					var bounds = item.bounds;

					Vector3 bEx = bounds.extents;
					Vector3 bCe = bounds.center;

					Debug.DrawLine(bCe + new Vector3(bEx.x, bEx.y, bEx.z), bCe + new Vector3(-bEx.x, bEx.y, bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(bEx.x, -bEx.y, bEx.z), bCe + new Vector3(-bEx.x, -bEx.y, bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(bEx.x, -bEx.y, -bEx.z), bCe + new Vector3(-bEx.x, -bEx.y, -bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(bEx.x, bEx.y, -bEx.z), bCe + new Vector3(-bEx.x, bEx.y, -bEx.z), cl);

					Debug.DrawLine(bCe + new Vector3(bEx.x, bEx.y, bEx.z), bCe + new Vector3(bEx.x, -bEx.y, bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(-bEx.x, bEx.y, bEx.z), bCe + new Vector3(-bEx.x, -bEx.y, bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(-bEx.x, bEx.y, -bEx.z), bCe + new Vector3(-bEx.x, -bEx.y, -bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(bEx.x, bEx.y, -bEx.z), bCe + new Vector3(bEx.x, -bEx.y, -bEx.z), cl);

					Debug.DrawLine(bCe + new Vector3(bEx.x, bEx.y, bEx.z), bCe + new Vector3(bEx.x, bEx.y, -bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(-bEx.x, bEx.y, bEx.z), bCe + new Vector3(-bEx.x, bEx.y, -bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(-bEx.x, -bEx.y, bEx.z), bCe + new Vector3(-bEx.x, -bEx.y, -bEx.z), cl);
					Debug.DrawLine(bCe + new Vector3(bEx.x, -bEx.y, bEx.z), bCe + new Vector3(bEx.x, -bEx.y, -bEx.z), cl);
					
				}
			}
		}
	}
	
	static Vector3 posCopy;
	static Quaternion rotCopy;
	static Vector3 scaleCopy;
	void CopyPast() 
	{
		Event e = Event.current;
		switch (e.type)
		{
			case EventType.KeyDown:
				{
					if (Event.current.keyCode == (KeyCode.B))
					{
						var va = (Transform)target;
						posCopy = va.position;
						rotCopy = va.rotation;
						scaleCopy = va.localScale;
						e.Use();
					}
					if ( Event.current.keyCode == (KeyCode.N))
					{
						var va = (Transform)target;
						va.position = posCopy;
						va.rotation = rotCopy;
						va.localScale = scaleCopy;
						e.Use();
					}
					if (Event.current.keyCode == (KeyCode.U))
					{
						var va = (Transform)target;

						bool isConnect = Physics.Raycast(va.position, -va.up, out RaycastHit hit, 50);

						if (isConnect)
						{
							Vector3 normal = hit.normal;
							Vector3 right = Vector3.Cross(normal, va.forward).normalized;
							Vector3 forward = Vector3.Cross(normal, right).normalized;

							va.rotation = Quaternion.LookRotation(forward, normal);

							va.position = hit.point + (hit.normal * 0.5f);
						}
						else
						{
							Debug.LogError("Not connect!");
						}
					}
					break;
				}
		}
	}

	void DrawPosition ()
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("P", GUILayout.Width(43f));

			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));

			if (reset) mPos.vector3Value = Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	Vector3 originalValue;
	void DrawScale (bool isWidget)
	{
		GUILayout.BeginHorizontal();
		{
			GUIContent gUIContent = new GUIContent(EditorGUIUtility.FindTexture(UniformScaling ? "LockIcon-On": "LockIcon"));

			if (GUILayout.Button(gUIContent, GUILayout.Width(20f), GUILayout.ExpandHeight(true))) 
			{
				UniformScaling = !UniformScaling; 
			}

			if (isWidget) GUI.color = new Color(0.7f, 0.7f, 0.7f);
			
			if (UniformScaling)
			{
				Vector3 newValue = mScale.vector3Value;

				if (newValue.x != originalValue.x)
				{
					float difference = newValue.x / originalValue.x;
					newValue.y = originalValue.y * difference;
					newValue.z = originalValue.z * difference;
					originalValue = newValue;
				}
				mScale.vector3Value = newValue;
			}
			originalValue = mScale.vector3Value;

			bool reset = GUILayout.Button("S", GUILayout.Width(20f));
			if (reset) mScale.vector3Value = Vector3.one;


			/*if (EditorGUI.EndChangeCheck())
			{
				
			}*/
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("x"));
			EditorGUI.BeginDisabledGroup(UniformScaling);
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("y"));
			EditorGUI.EndDisabledGroup();
			EditorGUI.BeginDisabledGroup(UniformScaling);
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("z"));
			EditorGUI.EndDisabledGroup();

			if (isWidget) GUI.color = Color.white;

			
		}
		GUILayout.EndHorizontal();
	}

	public static Vector3 Vector3InputField(Vector3 value, bool lockX, bool lockY, bool lockZ)
	{

		Vector3 originalValue = value;
		Vector3 newValue = value;

		newValue.x = EditorGUILayout.FloatField("X", newValue.x);
		newValue.y = EditorGUILayout.FloatField("Y", newValue.y);
		newValue.z = EditorGUILayout.FloatField("Z", newValue.z);

		float difference = newValue.x / originalValue.x;
		if (lockY)
		{
			newValue.y = originalValue.y * difference;
		}
		if (lockZ)
			newValue.z = originalValue.z * difference;

		return newValue;
	}

	#region Rotation is ugly as hell... since there is no native support for quaternion property drawing
	enum Axes : int
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4,
		All = 7,
	}

	Axes CheckDifference (Transform t, Vector3 original)
	{
		Vector3 next = t.localEulerAngles;

		Axes axes = Axes.None;

		if (Differs(next.x, original.x)) axes |= Axes.X;
		if (Differs(next.y, original.y)) axes |= Axes.Y;
		if (Differs(next.z, original.z)) axes |= Axes.Z;

		return axes;
	}

	Axes CheckDifference (SerializedProperty property)
	{
		Axes axes = Axes.None;

		if (property.hasMultipleDifferentValues)
		{
			Vector3 original = property.quaternionValue.eulerAngles;

			foreach (Object obj in serializedObject.targetObjects)
			{
				axes |= CheckDifference(obj as Transform, original);
				if (axes == Axes.All) break;
			}
		}
		return axes;
	}

	static bool FloatField (string name, ref float value, bool hidden, bool greyedOut, GUILayoutOption opt)
	{
		float newValue = value;
		GUI.changed = false;

		if (!hidden)
		{
			if (greyedOut)
			{
				GUI.color = new Color(0.7f, 0.7f, 0.7f);
				newValue = EditorGUILayout.FloatField(name, newValue, opt);
				GUI.color = Color.white;
			}
			else
			{
				newValue = EditorGUILayout.FloatField(name, newValue, opt);
			}
		}
		else if (greyedOut)
		{
			GUI.color = new Color(0.7f, 0.7f, 0.7f);
			float.TryParse(EditorGUILayout.TextField(name, "--", opt), out newValue);
			GUI.color = Color.white;
		}
		else
		{
			float.TryParse(EditorGUILayout.TextField(name, "--", opt), out newValue);
		}

		if (GUI.changed && Differs(newValue, value))
		{
			value = newValue;
			return true;
		}
		return false;
	}

	static bool Differs (float a, float b) { return Mathf.Abs(a - b) > 0.0001f; }

	void DrawRotation (bool isWidget)
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("R", GUILayout.Width(43f));
			Undo.RecordObject((serializedObject.targetObject as Transform), "ChangeScale");
			Vector3 visible = (serializedObject.targetObject as Transform).localEulerAngles;

            visible.x = WrapAngle(visible.x);
            visible.y = WrapAngle(visible.y);
            visible.z = WrapAngle(visible.z);

            Axes changed = CheckDifference(mRot);
			Axes altered = Axes.None;

			GUILayoutOption opt = GUILayout.MinWidth(30f);

			if (FloatField("X", ref visible.x, (changed & Axes.X) != 0, isWidget, opt)) altered |= Axes.X;
			if (FloatField("Y", ref visible.y, (changed & Axes.Y) != 0, isWidget, opt)) altered |= Axes.Y;
			if (FloatField("Z", ref visible.z, (changed & Axes.Z) != 0, false, opt)) altered |= Axes.Z;

			if (reset)
			{
				mRot.quaternionValue = Quaternion.identity;
			}
			else if (altered != Axes.None)
			{

				foreach (Object obj in serializedObject.targetObjects)
				{
					Transform t = obj as Transform;
					Vector3 v = t.localEulerAngles;

					if ((altered & Axes.X) != 0) v.x = visible.x;
					if ((altered & Axes.Y) != 0) v.y = visible.y;
					if ((altered & Axes.Z) != 0) v.z = visible.z;

					t.localEulerAngles = v;
				}
			}
		}
		GUILayout.EndHorizontal();
	}
    #endregion

    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static float WrapAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
#endif