using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //All keyboard inputs are handled by the Input System package
    private PlayerActions playerActions;
    private InputAction walkAction;
    private InputAction sprintAction;
    private InputAction kickAction;
    private InputAction grabAction;

    //movement
    [Range(0, 10)][SerializeField] float speed; //move speed
    [Range(0, 10)][SerializeField] float sprintMultiplier;
    private float moveSpeed;
    private Vector3 walkDirection;
    private float rotateSpeed;
    private Quaternion targetRotation;

    //kicking
    [SerializeField] float kickForce = 5;
    [SerializeField] GameObject ball;
    [SerializeField] private float grabRadius = 1.5f;
    [SerializeField] private float ballRotationSpeed = 35f;
    [SerializeField] private float kickSideAngleLimit = 45f;
    [SerializeField] private float kickUpAngleLimit = 90f;
    [SerializeField] Color trajectoryColor = Color.yellow;
    [SerializeField] int maxTrajectoryPoints = 100;
    [SerializeField] float trajectoryTimeStep = 0.1f;
    private float kickUpAngle;
    private float kickSideAngle;
    private float kickInputModifier = 10f;
    private Vector3 kickDirection;
    private bool isGrabbing = false;
    private float kickInputY;
    private float kickInputX;

    //Rigidbodies
    private Rigidbody ballRigidbody;
    private Rigidbody playerRigidbody;

    void Awake()
    {
        playerActions = new PlayerActions();
        walkAction = playerActions.Movement.Walk;
        sprintAction = playerActions.Movement.Sprint;
        kickAction = playerActions.Movement.Kick;
        grabAction = playerActions.Movement.Grab;


        sprintAction.performed += OnSprint;
        sprintAction.canceled += EndSprint;
        grabAction.performed += OnGrab;
        grabAction.canceled += OnKick;
    }
    void OnGrab(InputAction.CallbackContext context)
    {
        if (Vector3.Distance(transform.position, ball.transform.position) < grabRadius)
        {
            walkAction.Disable();

            //rotate player to face the ball
            Quaternion lookRotation = Quaternion.LookRotation(ball.transform.position - transform.position);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            //Stopping the ball from moving when grabbed
            ballRigidbody.isKinematic = true;
            isGrabbing = true;
            GameManager.Instance.Kick();
        }
    }
    void OnKick(InputAction.CallbackContext context)
    {
        //Exiting the grab mode and performing the kick
        if (isGrabbing)
        {
            ballRigidbody.isKinematic = false;
            ballRigidbody.AddForce(kickDirection * kickForce, ForceMode.Impulse);
        }
        isGrabbing = false;
        walkAction.Enable();
        kickUpAngle = 0f;
        kickSideAngle = 0f;
    }
    void OnSprint(InputAction.CallbackContext context)
    {
        moveSpeed = speed * sprintMultiplier;

    }

    void EndSprint(InputAction.CallbackContext context)
    {
        moveSpeed = speed;
    }


    void OnEnable()
    {
        walkAction.Enable();
        sprintAction.Enable();
        kickAction.Enable();
        grabAction.Enable();
    }

    void OnDisable()
    {
        walkAction.Disable();
        sprintAction.Disable();
        kickAction.Disable();
        grabAction.Disable();
    }

    void Start()
    {
        ball = GameObject.Find("Bouncy Ball");
        if (ball == null)
        {
            ball = GameObject.Find("Ball");
        }
        moveSpeed = speed;
        rotateSpeed = 10;
        playerRigidbody = GetComponent<Rigidbody>();
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isGrabbing)
        {
            //Calculating kick direction
            kickInputY = kickAction.ReadValue<Vector2>().y * kickInputModifier;
            kickInputX = kickAction.ReadValue<Vector2>().x * kickInputModifier;

            kickUpAngle += kickInputY * ballRotationSpeed * Time.deltaTime;
            kickUpAngle = Mathf.Clamp(kickUpAngle, -kickUpAngleLimit, kickUpAngleLimit);

            kickSideAngle += kickInputX * ballRotationSpeed * Time.deltaTime;
            kickSideAngle = Mathf.Clamp(kickSideAngle, -kickSideAngleLimit, kickSideAngleLimit);

            kickDirection = Quaternion.Euler(kickUpAngle, kickSideAngle, 0f) * transform.forward;
        }

        walkDirection = new Vector3(walkAction.ReadValue<Vector2>().x, 0, walkAction.ReadValue<Vector2>().y);

        if (walkDirection != Vector3.zero)
        {
            //rotate player to face the direction of movement
            targetRotation = Quaternion.LookRotation(walkDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        //Moving the player
        playerRigidbody.MovePosition(transform.position + walkDirection * moveSpeed * Time.fixedDeltaTime);
        //handling Ball movement using physics
        playerRigidbody.AddForce(walkDirection * moveSpeed * Time.fixedDeltaTime * kickForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (isGrabbing)
        {
            //Drawing Trajectory Gizmos here
            Gizmos.color = trajectoryColor;
            Vector3 initialPosition = ball.transform.position;
            Vector3 initialVelocity = kickDirection.normalized * kickForce * 10;
            float currentTime = 0f;

            for (int i = 0; i < maxTrajectoryPoints; i++)
            {
                Vector3 point = initialPosition + initialVelocity * currentTime + 0.5f * Physics.gravity * currentTime * currentTime;

                Gizmos.DrawSphere(point, 0.05f);
                currentTime += trajectoryTimeStep;
            }

        }

    }
    public Vector3 GetKickDirection()
    {
        return kickDirection;
    }
    public bool GetIsGrabbing()
    {
        return isGrabbing;
    }
}
