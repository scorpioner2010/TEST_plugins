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

        return;
        if (isMove)
        {
            
        }
        
        
        
        /*
        Vector3 c = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        if (c.magnitude > 1f)
        {
            lastDirectionRotate = _dir;
            animator.anim.transform.LookAt(lastDirectionRotate + transform.position);
        }*/
    }
    
    private void Update()
    {
        /*
        if (photonView.IsMine == false)
        {
            return;
        }*/

        isMove = _dir != Vector3.zero;
        _dir = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            _dir += transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir += -transform.forward;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _dir += -transform.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir += transform.right;
        }
        
        
        rb.velocity = new Vector3(_dir.x, _dir.y, _dir.z) * speedMove;
        
    }
}
