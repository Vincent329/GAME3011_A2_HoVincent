using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter The Area");
        // activate the text
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit The Interface");
        // deactivate the text
    }
}
