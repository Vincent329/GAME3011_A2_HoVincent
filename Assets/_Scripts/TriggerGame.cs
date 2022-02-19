using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter The Area");
        // activate the text
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit The Interface");
        // deactivate the text
    }
}
