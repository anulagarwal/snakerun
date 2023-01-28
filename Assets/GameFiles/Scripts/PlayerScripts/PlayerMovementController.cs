using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float directionalSpeed = 0f;
    [SerializeField] private float knockBackDelay = 0f;
    [SerializeField] private float knockbackSpeed = 0f;


    [Header("Components Reference")]
    [SerializeField] private CharacterController headCharacterController = null;
    [SerializeField] private PlayerBeadsManager playerBeadsManager = null;
    [SerializeField] private Transform mainParent = null;
    [SerializeField] private Transform targetParent = null;
    [SerializeField] private PlayerFollowLastBead playerFollowLastBead = null;

    [Header("Fake Gravity Attributes")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private float groundDistance = 0f;
    [SerializeField] private LayerMask groundMask = 0;
    [SerializeField] private float jumpHeight = 3f;

    private bool isGravityActive = false;
    private bool isGrounded = false;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    private CharacterController tailCharacterController = null;
    private CharacterController activeCharacterController = null;
    private bool isPlayerKnocked = false;
    private float oldX;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        SwitchCrawlDirection(SnakeCrawlDirection.Forward);
        movementJS = UIPackSingleton.Instance.GetGameplayCanvasHandler.GetMovementJS;
        IsHeadActive = true;

        SwapActiveCharacterControllerToHead();
        EnablePlayerMovement(false);
    }

    private void Update()
    {
        float x = 0;

        if (!PlayerSingleton.Instance.ForceStopPlayerMovement && !isPlayerKnocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                oldX = Input.mousePosition.x;
            }
            if (Input.GetMouseButton(0))
            {
                x = (Input.mousePosition.x - oldX);
                oldX = Input.mousePosition.x;
            }
            if (ActiveCrawlDirection == SnakeCrawlDirection.Forward)
            {
                movementDirection = new Vector3(x * directionalSpeed, 0, 1).normalized;
            }
            else if (ActiveCrawlDirection == SnakeCrawlDirection.Up)
            {
                movementDirection = new Vector3(x * directionalSpeed, 1, 0).normalized;
            }

            activeCharacterController.Move(movementDirection * Time.deltaTime * moveSpeed);

            //check if grounded
            if (isGravityActive)
            {
                FakeGravity();
            }
        }
        else if (isPlayerKnocked)
        {
            mainParent.transform.Translate(Vector3.back * Time.smoothDeltaTime * knockbackSpeed);
        }
    }
    #endregion
    
    #region Getter And Setter
    public SnakeCrawlDirection ActiveCrawlDirection { get; set; }

    public CharacterController SetTailCharacterController { set { tailCharacterController = value; } }

    public bool IsHeadActive { get; set; }
    #endregion

    #region Private Core Functions
    private void FakeGravity()
    {
        isGrounded = Physics.CheckSphere(activeCharacterController.transform.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        activeCharacterController.Move(velocity * Time.deltaTime);
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

    public void SwapActiveCharacterControllerToTail()
    {
        IsHeadActive = false;
        activeCharacterController = tailCharacterController;
        headCharacterController.transform.parent = targetParent;

    }

    public void SwapActiveCharacterControllerToHead()
    {
        IsHeadActive = true;
        activeCharacterController = headCharacterController;
        headCharacterController.transform.parent = mainParent;
    }

    public void KnockBackPlayer()
    {
        isPlayerKnocked = true;
        Invoke("Invoke_ResetPlayerMovement", knockBackDelay);
    }
    #endregion

    #region Invoke Functions
    private void Invoke_ResetPlayerMovement()
    {
        PlayerSingleton.Instance.ForceStopPlayerMovement = false;
        isPlayerKnocked = false;
    }
    #endregion
}
