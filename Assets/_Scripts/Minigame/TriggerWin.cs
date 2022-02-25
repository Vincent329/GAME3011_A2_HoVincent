using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Trigger Win");
            other.gameObject.SetActive(false);
            GameManager.Instance.WinSession();
        }
    }
}
