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
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (elementIsActive)
        {
            Debug.Log("Actions goooo");
            gameInputActions.Player.Move.performed += OnMove;
            gameInputActions.Player.Move.canceled += OnMove;
        }
    }

    private void OnDisable()
    {
        gameInputActions.Player.Move.performed -= OnMove;
        gameInputActions.Player.Move.canceled -= OnMove;

        GameManager.Instance.StartWithDifficulty -= SwitchToMinigame;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        elementIsActive = true;
        gameInputActions = InputManager.inputActions;
        gameInputActions.Player.Move.performed += OnMove;
        gameInputActions.Player.Move.canceled += OnMove;

        GameManager.Instance.StartWithDifficulty += SwitchToMinigame;
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

    private void SwitchToMinigame()
    {
        if (!GameManager.Instance.inGame)
        {
            GameManager.Instance.ToggleCameras(); // will have to change depending on what difficulty
            playerVelocity = Vector3.zero; // zero out the velocity of the player
            InputManager.ToggleActionMap(gameInputActions.Minigame);
        }
    }

}
