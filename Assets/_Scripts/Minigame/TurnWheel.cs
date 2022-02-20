using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnWheel : MonoBehaviour
{
    // Action Map Component
    private GameInputActions gameInputActions;
    private InputAction moveWheel;
    private InputAction switchToPlayer;
    //[SerializeField] private DifficultyEnum difficulty;

    [SerializeField] private bool m_bIsMovingLeft;
    [SerializeField] private bool m_bIsMovingRight;

    [SerializeField] private float m_fSpeedModifier;
    [SerializeField] private bool m_bActiveLock;
    private Transform initialState;
    float m_fRotationValue;

    public bool initializeStartup = false;

    private void OnEnable()
    {
        if (initializeStartup)
        {
            Debug.Log("BindZeDelegate2");
            moveWheel = gameInputActions.Minigame.TurnWheel;
            moveWheel.performed += OnTurnWheel;
            moveWheel.canceled += OnTurnWheel;

            switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
            switchToPlayer.started += OnSwitchToPlayer;
            GameManager.Instance.Reset += RestartState;
        }
    }

    private void OnDisable()
    {
        moveWheel = gameInputActions.Minigame.TurnWheel;
        moveWheel.performed -= OnTurnWheel;
        moveWheel.canceled -= OnTurnWheel;

        switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
        switchToPlayer.started -= OnSwitchToPlayer;

        GameManager.Instance.Reset -= RestartState;
    }

    private void Awake()
    {
        gameInputActions = InputManager.inputActions;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveWheel = gameInputActions.Minigame.TurnWheel;
        moveWheel.performed += OnTurnWheel;
        moveWheel.canceled += OnTurnWheel;

        switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
        switchToPlayer.started += OnSwitchToPlayer;

        m_bIsMovingLeft = false;
        m_bIsMovingRight = false;
        m_fRotationValue = 0;
        initialState = this.transform;

        initializeStartup = true;
        GameManager.Instance.Reset += RestartState;

    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (m_fRotationValue * m_fSpeedModifier));
    }

    public void OnTurnWheel(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.inGame)
        {
            m_fRotationValue = obj.ReadValue<float>(); // getting the float value
            if (m_fRotationValue < 0)
            {
                m_bIsMovingLeft = true;
            } else if (m_fRotationValue > 0)
            {
                m_bIsMovingRight = true;
            } else
            {
                m_bIsMovingLeft = false;
                m_bIsMovingRight = false;
            }
        }
    }

    public void OnSwitchToPlayer(InputAction.CallbackContext context)
    {
        Debug.Log("Switch Back To Player");
        InputManager.ToggleActionMap(gameInputActions.Player);
    }

    private void RestartState()
    {
        Debug.Log("SHOULD RESET");
        transform.localEulerAngles = initialState.localEulerAngles;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            // TO DO: reset functionality
            Debug.Log("Key Touch, Restart");
            GameManager.Instance.RestartSession();
        }
    }
}
