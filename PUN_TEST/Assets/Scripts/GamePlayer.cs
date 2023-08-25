using Photon.Pun;
using UnityEngine;

public class GamePlayer : MonoBehaviourPun
{
    public GameCamera gameCamera;
    public Animator animator;
    public PhotonAnimatorView animatorView;
    public Rigidbody body;

    public AnimationState animationState = AnimationState.Idle;
    public AnimationState lastState = AnimationState.Idle;
    
    public float lerpSpeed = 1;
    
    public float moveSpeed = 1;
    public float rotateSpeed = 10;
    
    public Vector2 inputRotate;
    public Vector2 inputMove;

    private Vector3 _lastPosition;
    private Vector3 _lastDirection;
    
    private Vector3 _directionMove;
    private bool IsMove => _directionMove != Vector3.zero;
    
    
    private float GetObjectSpeed()
    {
        _lastDirection = transform.position - _lastPosition;
        _lastPosition = transform.position;
        float speed = _lastDirection.magnitude / Time. deltaTime;
        return speed;
    }
    
    public void GetInputRotate(Vector2 parameters)
    {
        inputRotate = parameters;
    }
    
    public void GetInputMove(Vector2 parameters)
    {
        inputMove = parameters;
    }

    private void Start()
    {
        GameplayController.OnMove += GetInputMove;
        GameplayController.OnRotate += GetInputRotate;
    }

    private void OnDestroy()
    {
        GameplayController.OnMove -= GetInputMove;
        GameplayController.OnRotate -= GetInputRotate;
    }

    private void Joystick()
    {
        if (inputMove != Vector2.zero)
        {
            _directionMove = transform.forward * inputMove.y;
            _directionMove += transform.right * inputMove.x;
        }
    }

    private void Keyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _directionMove = transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _directionMove = -transform.forward;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _directionMove = -transform.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _directionMove = transform.right;
        }
    }
    
    private void FixedUpdate()
    {
        Vector3 direction = _directionMove.normalized * moveSpeed * Time.fixedDeltaTime;
        body.velocity = new Vector3(direction.x, body.velocity.y, direction.z);
        //body.angularVelocity = new Vector3(0, inputRotate.x * rotateSpeed * Time.fixedDeltaTime, 0);
    }

    private void Update()
    {
        AnimationSelector();
        
        if (photonView.IsMine == false)
        {
            return;
        }

        animator.transform.localPosition = Vector3.Lerp(animator.transform.localPosition, Vector3.zero, lerpSpeed * Time.deltaTime);
        animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
        
        _directionMove = Vector3.zero;
        
        Keyboard();
        Joystick();
    }
    
    private void AnimationSelector()
    {
        animationState = IsMove ? AnimationState.Run : AnimationState.Idle;

        if (lastState != animationState)
        {
            lastState = animationState;
            string trigger = lastState.ToString();
            animator.SetTrigger(trigger);
        }
    }

}

public enum AnimationState
{
    Idle,
    Run,
}
