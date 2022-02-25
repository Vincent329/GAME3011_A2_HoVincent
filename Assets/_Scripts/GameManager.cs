using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
    }

    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private Camera lockpickCam;

    // ACTIVATES ON GAME STATE BEING TOGGLED
    public bool inGame;

    [Header("Game Elements")]
    public DifficultyEnum difficultySet;

    [Header("Text Fields")]
    [SerializeField] private GameObject difficultySelectCanvas;
    [SerializeField] private GameObject lockpickingCanvas;

    [SerializeField] private GameObject lockpickingAttemptsText;
    [SerializeField] private GameObject complexityText;
    [SerializeField] private GameObject skillText;
    
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject FailText;
    [SerializeField] private GameObject Timer;
    public int lockpickAttempts;

    [Header("List of Minigame Camera Positions")]
    [SerializeField] private List<GameObject> positions;

    // Delegates
    public delegate void DifficultySet(DifficultyEnum difficulty);
    public event DifficultySet StartWithDifficulty;

    public delegate void WinGame();
    public event WinGame Win;

    public delegate void ResetGame();
    public event ResetGame Reset;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        skillText.GetComponent<TextMeshProUGUI>().text = "Alter Slider to change skill level";

        inGame = false;
        playerCam.enabled = true;
        lockpickCam.enabled = false;
        lockpickingCanvas.SetActive(false);
        difficultySelectCanvas.SetActive(false);
        winText.SetActive(false);
        FailText.SetActive(false);
    }

    private void OnEnable()
    {
        Win += EnableWinText;
    }

    private void OnDisable()
    {
        Win -= EnableWinText;
    }

    public void ToggleCameras()
    {
        // current test, check if the minigame camera is working
        inGame = !inGame;
        if (!inGame)
        {
            playerCam.enabled = true;
            lockpickCam.enabled = false; 
            lockpickingCanvas.SetActive(false);
        } else if (inGame)
        {
            // sets the position to be at the difficulty
            lockpickCam.transform.position = positions[(int)difficultySet].transform.position;
            playerCam.enabled = false;
            lockpickCam.enabled = true;
            lockpickingCanvas.SetActive(true);
        }
    }

    public void DifficultyChange(DifficultyEnum newDifficulty)
    {
        difficultySet = newDifficulty;
    }

    public void StartGameAtDifficulty()
    {
        StartWithDifficulty(difficultySet);
    }

    public void RestartSession()
    {
        Reset();
    }

    public void WinSession()
    {    
        Win();
    }

    public void UpdateLockpickingAttempts()
    {
        lockpickAttempts--;
        // update text here
    }

    private void UpdateTimer()
    {

    }

    private void EnableWinText()
    {
        winText.SetActive(true);
    }

    private void EnableLoseText()
    {
        FailText.SetActive(true);
    }

    public void UpdateSliderSkill(float skillValue)
    {
        if (skillValue >= 0.3f && skillValue < 0.5f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Inexperienced";
        } else if (skillValue >= 0.5f && skillValue < 0.75f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Normal";

        }
        else if (skillValue >= 0.75f && skillValue <= 1.0f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Master";

        }
        else
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "ERROR";

        }
    }
}
