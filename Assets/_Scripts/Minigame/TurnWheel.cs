using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This class effects the turning of the wheel
/// </summary>
public class TurnWheel : MonoBehaviour
{
    // Action Map Component
    private GameInputActions gameInputActions;
    private InputAction moveWheel;
    private InputAction switchToPlayer;

    // variables
    [SerializeField] private Transform spawnPos;
    [SerializeField] private DifficultyEnum difficulty;
    [SerializeField] GameObject key;
    [SerializeField] Slider scaleSlider;

    [SerializeField] private bool m_bIsMovingLeft;
    [SerializeField] private bool m_bIsMovingRight;

    [SerializeField] private float m_fSpeedModifier;
    [SerializeField] private bool m_bActiveLock;
    private Transform initialState;
    float m_fRotationValue;

    private Vector3 originalScale;
    public bool initializeStartup = false;

    private void OnEnable()
    {
        if (initializeStartup)
        {
            Debug.Log("BindZeDelegate2");

            scaleSlider.onValueChanged.AddListener(delegate { KeySizeChange(); });

            moveWheel = gameInputActions.Minigame.TurnWheel;
            moveWheel.performed += OnTurnWheel;
            moveWheel.canceled += OnTurnWheel;

            switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
            switchToPlayer.started += OnSwitchToPlayer;
            GameManager.Instance.StartWithDifficulty += Initialize;

        }
    }

    private void OnDisable()
    {
        scaleSlider.onValueChanged.RemoveAllListeners();

        moveWheel = gameInputActions.Minigame.TurnWheel;
        moveWheel.performed -= OnTurnWheel;
        moveWheel.canceled -= OnTurnWheel;

        switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
        switchToPlayer.started -= OnSwitchToPlayer;
        GameManager.Instance.StartWithDifficulty -= Initialize;

    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInputActions = InputManager.inputActions;
        moveWheel = gameInputActions.Minigame.TurnWheel;
        moveWheel.performed += OnTurnWheel;
        moveWheel.canceled += OnTurnWheel;

        switchToPlayer = gameInputActions.Minigame.SwitchToPlayer;
        switchToPlayer.started += OnSwitchToPlayer;

        scaleSlider.onValueChanged.AddListener(delegate { KeySizeChange(); });
        key = transform.Find("KeyObject").gameObject;
        initializeStartup = true;
        GameManager.Instance.StartWithDifficulty += Initialize;

        Initialize();

    }

    private void Initialize()
    {
        key.gameObject.SetActive(true);
        key.transform.position = spawnPos.position;
        originalScale = key.transform.localScale;

        m_bIsMovingLeft = false;
        m_bIsMovingRight = false;
        m_fRotationValue = 0;
        initialState = this.transform;
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

    /// <summary>
    /// Alters the scale of the key based on the slider value
    /// </summary>
    public void KeySizeChange()
    {
        Vector3 tempScale = key.transform.localScale;
        Vector3 alteredScale = originalScale * scaleSlider.value;
        
        key.transform.localScale = new Vector3(alteredScale.x, tempScale.y, alteredScale.z);
        GameManager.Instance.UpdateSliderSkill(scaleSlider.value);
        Debug.Log(key.transform.localScale);
    }

    /// <summary>
    /// Switch to a button prompt instead of a mouse click?
    /// </summary>
    /// <param name="context"></param>
    public void OnSwitchToPlayer(InputAction.CallbackContext context)
    {
        Debug.Log("Switch Back To Player");
        GameManager.Instance.ToggleCameras(); // will have to change depending on what difficulty
        InputManager.ToggleActionMap(gameInputActions.Player);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            // TO DO: reset functionality
            if (GameManager.Instance.lockpickAttempts > 1)
            {
                GameManager.Instance.RestartSession();
                GameManager.Instance.UpdateLockpickingAttempts();
                collision.gameObject.transform.position = spawnPos.transform.position;
            } else
            {
                // Lose the game
                GameManager.Instance.LoseSession();
                key.SetActive(false);
            }
        }
    }
}
