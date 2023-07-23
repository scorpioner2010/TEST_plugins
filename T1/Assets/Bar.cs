using UnityEngine;

public class Bar : MonoBehaviour
{
    public float sizeBar;
    public Transform bar;
    public float xSize = 1;

    private void Update()
    {
        bar.localScale = new Vector3(sizeBar, 1, 1);
        bar.localPosition = new Vector3(xSize-sizeBar * xSize, 0, 0);
        transform.LookAt(Camera.main.transform.position);
    }
}