using System;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
	public CinemachineVirtualCamera virtualCamera;
	public Rigidbody rb;
	public float speedAlign = 10;
	public LayerMask mask;
	public float gravityScale = 1;
	
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void AlignKart(Vector3 normal)
	{
		Quaternion rotationT = transform.rotation;
		Quaternion toT = Quaternion.FromToRotation(transform.up * 2, normal) * rotationT;
		rotationT = Quaternion.Lerp(rotationT, toT, /*20*/speedAlign * Time.deltaTime); // speed rotation to normal
		transform.rotation = rotationT;
	}

	private void FixedUpdate()
	{
		Vector3 direction = transform.position - Earth.In.transform.position;
		rb.AddForce(direction.normalized * gravityScale);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, Earth.In.transform.position);
	
	}

	private void Update()
	{
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, Mathf.Infinity, ~mask))
		{
			AlignKart(hit.normal);
		}
	}
}
