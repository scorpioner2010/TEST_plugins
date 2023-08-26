using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform rig;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPosition.position, Time.deltaTime * 5);
        transform.rotation = Quaternion.LookRotation(rig.position - transform.position);

        /*
        Vector3 dot = GameObject.Find("Dot").transform.position;
        Vector3 direction = dot - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        */
    }
}