using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;

    [Header("Components Reference")]
    [SerializeField] private CharacterController characterController = null;

    [Header("Fake Gravity Attributes")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private float groundDistance = 0f;
    [SerializeField] private LayerMask groundMask = 0;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Beads Attributes")]
    [SerializeField] private List<Transform> tailTransformReferences = new List<Transform>();
    [SerializeField] private List<PlayerBeadFollower> playerBeadFollowers = new List<PlayerBeadFollower>();

    private bool isGrounded = false;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        movementJS = UIPackSingleton.Instance.GetGameplayCanvasHandler.GetMovementJS;
        BeadsInitialSetup();

        EnablePlayerMovement(false);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        movementDirection = new Vector3(movementJS.Horizontal, 0, 1).normalized;
        characterController.Move(movementDirection * Time.deltaTime * moveSpeed);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region Private Core Functions
    private void BeadsInitialSetup()
    {
        int tailTransformIndex = 0;
        foreach (PlayerBeadFollower playerBeadFollower in playerBeadFollowers)
        {
            playerBeadFollower.TargetTransform = tailTransformReferences[tailTransformIndex];
            tailTransformIndex++;
        }
    }
    #endregion

    #region Public Core Functions
    public void EnablePlayerMovement(bool value)
    {
        foreach (PlayerBeadFollower playerBeadFollower in playerBeadFollowers)
        {
            playerBeadFollower.enabled = value;
        }
        this.enabled = value;
    }

    public void Jump()
    {
        print("Working");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    #endregion
}
