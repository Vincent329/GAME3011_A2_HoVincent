using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Setting for a button
/// </summary>
public class DifficultySelect : MonoBehaviour
{
    [SerializeField] DifficultyEnum difficultySet;
    // Start is called before the first frame update
    [SerializeField] private Button difficultyButton;
    void Start()
    {
        difficultyButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
