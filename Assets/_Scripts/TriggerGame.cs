using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{
    bool playerIsIn;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            Debug.Log("Enter The Area");
            // activate the text
            GameManager.Instance.ActivateDifficultyMenu(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerIsIn)
        {
            if (GameManager.Instance.inGame)
            {
                GameManager.Instance.ActivateDifficultyMenu(false);
            }
            else
            {
                GameManager.Instance.ActivateDifficultyMenu(true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Exit The Interface");
        GameManager.Instance.ActivateDifficultyMenu(false);

        // deactivate the text
    }
}
