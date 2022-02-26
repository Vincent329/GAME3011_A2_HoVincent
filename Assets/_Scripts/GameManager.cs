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
    public float initialStartTimer;
    public int lockpickAttempts;

    [Header("List of Minigame Camera Positions")]
    [SerializeField] private List<GameObject> positions;

    // Delegates
    public delegate void DifficultySet();
    public event DifficultySet StartWithDifficulty;

    public delegate void WinGame();
    public event WinGame Win; 
    
    public delegate void LoseGame();
    public event WinGame Lose;

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
        lockpickingAttemptsText.GetComponent<TextMeshProUGUI>().text = "Lockpicks Left: " + lockpickAttempts;
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
            difficultySelectCanvas.SetActive(false);

            complexityText.GetComponent<TextMeshProUGUI>().text = "Difficulty: " + difficultySet.ToString();
        }
    }

    public void ActivateDifficultyMenu(bool activate)
    {
        difficultySelectCanvas.SetActive(activate);
    }

    public void DifficultyChange(DifficultyEnum newDifficulty)
    {
        difficultySet = newDifficulty;
        if (difficultySet == DifficultyEnum.EASY)
            initialStartTimer = 20;
        if (difficultySet == DifficultyEnum.MEDIUM)
            initialStartTimer = 30;
        if (difficultySet == DifficultyEnum.HARD)
            initialStartTimer = 40;
    }

    public void StartGameAtDifficulty()
    {
        lockpickAttempts = 3;
        lockpickingAttemptsText.GetComponent<TextMeshProUGUI>().text = "Lockpicks Left: " + lockpickAttempts;

        winText.SetActive(false);
        FailText.SetActive(false);
        StartWithDifficulty();
    }

    public void RestartSession()
    {
        Reset();
    }

    public void WinSession()
    {    
        Win();
    }

    public void LoseSession()
    {
        lockpickAttempts = 0;
        // update text here
        lockpickingAttemptsText.GetComponent<TextMeshProUGUI>().text = "Lockpicks Left: " + lockpickAttempts;

        FailText.SetActive(true);
        Lose();
    }

    public void UpdateLockpickingAttempts()
    {
        lockpickAttempts--;
        // update text here
        lockpickingAttemptsText.GetComponent<TextMeshProUGUI>().text = "Lockpicks Left: " + lockpickAttempts;
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
        if (skillValue >= 0.4f && skillValue < 0.7f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Inexperienced";
        } else if (skillValue >= 0.7f && skillValue < 1.05f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Normal";

        }
        else if (skillValue >= 1.05f && skillValue <= 1.4f)
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "Master";

        }
        else
        {
            skillText.GetComponent<TextMeshProUGUI>().text = "ERROR";

        }
    }
}
