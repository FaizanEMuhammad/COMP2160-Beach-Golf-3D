using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    //Object references
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMove playerScript;
    [SerializeField] private Transform camera;

   //camera variables
    [Range(0, 1)] [SerializeField] private float moveSpeed;
    private Vector3 kickDirection;
    private Vector3 ballFollowVector;
    [SerializeField] private float kickHeightOffset = 4;
    private bool isGrabbing;

    void Start()
    {
        InitializeReferences();
    }

    void FixedUpdate()
    {
        kickDirection = playerScript.GetKickDirection();
        isGrabbing = playerScript.GetIsGrabbing();
        ballFollowVector = new Vector3(kickDirection.x, kickHeightOffset, kickDirection.z);
        UpdateCameraPosition();
    }
    private void InitializeReferences()
    {
        player = GameObject.Find("Player").transform;
        camera = GameObject.Find("Main Camera").transform;
        camera.SetParent(transform);
        playerScript = player.GetComponent<PlayerMove>();
    }
    private void UpdateCameraPosition()
    {
        if (isGrabbing)
        {
            FollowKickDirection();
        }
        else
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        Vector3 offset = Vector3.Lerp(transform.position, player.position, moveSpeed);
        transform.position = offset;
    }
    void FollowKickDirection()
    {
        Vector3 offset = Vector3.Lerp(transform.position, ballFollowVector, moveSpeed);
        transform.position = offset;
    }
}
