using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
	public CinemachineVirtualCamera virtualCamera;
	public Rigidbody rb;
	public float speedAlign = 10;
	public LayerMask mask;

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

	private void Update()
	{
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, Mathf.Infinity, ~mask))
		{
			AlignKart(hit.normal);
		}
	}
}