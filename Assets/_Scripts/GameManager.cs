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
    private Camera lockCam;

    public bool inGame;

    [Header("Game Elements")]
    [SerializeField] private GameObject lockpickingCanvas;
    [SerializeField] private GameObject promptText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject FailText;

    public int lockpickAttempts;



    // Delegates
    public delegate void ToggleGame();
    public event ToggleGame Toggle;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
