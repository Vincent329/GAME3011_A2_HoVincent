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
        difficultyButton.onClick.AddListener(ActivateGame);
    }

    private void ActivateGame()
    {
        GameManager.Instance.DifficultyChange(difficultySet);
        GameManager.Instance.StartGameAtDifficulty();
    }
}
