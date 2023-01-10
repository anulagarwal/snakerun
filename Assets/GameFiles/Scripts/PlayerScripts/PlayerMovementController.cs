using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float directionalSpeed = 0f;

    [Header("Components Reference")]
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private PlayerBeadsManager playerBeadsManager = null;

    [Header("Fake Gravity Attributes")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private float groundDistance = 0f;
    [SerializeField] private LayerMask groundMask = 0;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Beads Attributes")]
    [SerializeField] private List<Transform> tailTransformReferences = new List<Transform>();
    [SerializeField] private List<PlayerBeadFollower> playerBeadFollowers = new List<PlayerBeadFollower>();

    private bool isGravityActive = false;
    private bool isGrounded = false;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        SwitchCrawlDirection(SnakeCrawlDirection.Forward);
        movementJS = UIPackSingleton.Instance.GetGameplayCanvasHandler.GetMovementJS;
        BeadsInitialSetup();

        EnablePlayerMovement(false);
    }

    private void Update()
    {
        if (!PlayerSingleton.Instance.ForceStopPlayerMovement)
        {
            if (ActiveCrawlDirection == SnakeCrawlDirection.Forward)
            {
                movementDirection = new Vector3(movementJS.Horizontal * directionalSpeed, 0, 1).normalized;
            }
            else if (ActiveCrawlDirection == SnakeCrawlDirection.Up)
            {
                movementDirection = new Vector3(movementJS.Horizontal * directionalSpeed, 1, 0).normalized;
            }

            characterController.Move(movementDirection * Time.deltaTime * moveSpeed);

            if (isGravityActive)
            {
                FakeGravity();
            }
        }
    }
    #endregion
    //
    #region Getter And Setter
    public SnakeCrawlDirection ActiveCrawlDirection { get; set; }
    #endregion

    #region Private Core Functions
    private void BeadsInitialSetup()
    {
        int tailTransformIndex = 0;
        foreach (PlayerBeadFollower playerBeadFollower in playerBeadFollowers)
        {
            playerBeadFollower.NormalMovementTargetTransform = tailTransformReferences[tailTransformIndex];
            tailTransformIndex++;
        }
    }

    private void FakeGravity()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region Public Core Functions
    public void EnablePlayerMovement(bool value)
    {
        playerBeadsManager.EnablePlayerBeadsMovementMechanism(value);
        this.enabled = value;
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void SwitchCrawlDirection(SnakeCrawlDirection direction)
    {
        switch (direction)
        {
            case SnakeCrawlDirection.Up:
                ActiveCrawlDirection = direction;
                isGravityActive = false;
                break;
            case SnakeCrawlDirection.Forward:
                ActiveCrawlDirection = direction;
                isGravityActive = true;
                break;
        }
    }
    #endregion
}
