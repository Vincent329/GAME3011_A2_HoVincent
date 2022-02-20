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

    public bool inGame;

    [Header("Game Elements")]
    public DifficultyEnum difficultySet;
    [SerializeField] private GameObject lockpickingCanvas;
    [SerializeField] private GameObject promptText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject FailText;


    public int lockpickAttempts;

    // Delegates
    public delegate void DifficultySet(int difficulty);
    public event DifficultySet Toggle;

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
        inGame = false;
        playerCam.enabled = true;
        lockpickCam.enabled = false;
        lockpickingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            playerCam.enabled = false;
            lockpickCam.enabled = true;
            lockpickingCanvas.SetActive(true);
        }
    }

    public void RestartSession()
    {
        Reset();
    }

}
