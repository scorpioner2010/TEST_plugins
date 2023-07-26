using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviourPun
{
    public Rigidbody rb;
    public float speedMove = 1000;
    private Vector3 _dir;
    public Vector3 lastDirectionRotate;
    public bool isMove;

    public PlayerAnimator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        
        _dir = _dir.normalized * Time.fixedDeltaTime * speedMove;
        rb.velocity = new Vector3(_dir.x, rb.velocity.y, _dir.z);
        
        if (rb.velocity.magnitude > 1f)
        {
            lastDirectionRotate = _dir;
            animator.anim.transform.LookAt(lastDirectionRotate + transform.position);
        }
    }
    
    private void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        isMove = _dir != Vector3.zero;
        _dir = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            _dir += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir += Vector3.back;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _dir += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir += Vector3.right;
        }
    }
}
