using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement VAriables")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 playerVelocity;
    private Rigidbody rb;

    [Header("Minigame Activation Variables")]
    private bool inRange;
    public bool elementIsActive;
    public bool activeGame;

    private GameInputActions gameInputActions;
    private PlayerInput playerInput;
    private void Awake()
    {
        gameInputActions = new GameInputActions();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        gameInputActions.Enable();
        if (elementIsActive)
        {
            Debug.Log("Actions goooo");

            gameInputActions.Player.Move.performed += OnMove;
            gameInputActions.Player.Move.canceled += OnMove;
            gameInputActions.Player.SwitchToMinigame.started += OnSwitchToMinigame;
        }
    }

    private void OnDisable()
    {
        gameInputActions.Disable();
        gameInputActions.Player.Move.performed -= OnMove;
        gameInputActions.Player.Move.canceled -= OnMove;
        gameInputActions.Player.SwitchToMinigame.started -= OnSwitchToMinigame;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        elementIsActive = true;
        gameInputActions.Player.Move.performed += OnMove;
        gameInputActions.Player.Move.canceled += OnMove;
        gameInputActions.Player.SwitchToMinigame.started += OnSwitchToMinigame;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerVelocity * playerSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Input for movement
    /// </summary>
    /// <param name="value"></param>
    private void OnMove(InputAction.CallbackContext obj)
    {
        if (!GameManager.Instance.inGame)
        {
            Debug.Log("Moving");
            Vector2 moveValue = obj.ReadValue<Vector2>();
            playerVelocity = new Vector3(moveValue.x, 0, moveValue.y);
        }
    }

    /// <summary>
    /// Pressing the interact button
    /// </summary>
    /// <param name="value"></param>
    private void OnSwitchToMinigame(InputAction.CallbackContext obj)
    {
        Debug.Log("Interacting");
        GameManager.Instance.ToggleCameras(); // will have to change depending on what difficulty
        playerVelocity = Vector3.zero; // zero out the velocity of the player
    }
}
