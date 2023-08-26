using System;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera camera;
	public Rigidbody rb;
	public float speedAlign = 10;
	public LayerMask mask;
	public float gravityForce = 25;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		camera = GetComponentsInChildren<Camera>().First();
	}

	private void AlignKart(Vector3 normal)
	{
		Quaternion rotationT = transform.rotation;
		Quaternion toT = Quaternion.FromToRotation(transform.up * 2, normal) * rotationT;
		rotationT = Quaternion.Lerp(rotationT, toT,speedAlign * Time.deltaTime); // speed rotation to normal
		transform.rotation = rotationT;
	}


	private void FixedUpdate()
	{
		Vector3 f = GameObject.Find("Map").transform.position - transform.position;
		rb.AddForce(f.normalized * gravityForce);
	}

	private void Update()
	{
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, Mathf.Infinity, ~mask))
		{
			AlignKart(hit.normal);
		}


		if (Input.GetMouseButtonDown(2))
		{
			
		}
	}
}