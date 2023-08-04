using System;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
	public CinemachineVirtualCamera virtualCamera;
	public Rigidbody rb;
	public float speedAlign = 10;
	public LayerMask mask;
	public float gravityForce = 25;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
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
		Vector3 direction = transform.position - Earth.In.transform.position;
		rb.AddForce(direction.normalized * gravityForce);
	}

	private void Update()
	{
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, Mathf.Infinity, ~mask))
		{
			AlignKart(hit.normal);
		}
	}
}