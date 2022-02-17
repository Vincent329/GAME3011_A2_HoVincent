using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnWheel : MonoBehaviour
{
    [SerializeField] private bool isMovingLeft;
    [SerializeField] private bool isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        isMovingLeft = false;
        isMovingRight = false;
    }

    public void OnTurnWheel(InputValue value)
    {
        if (GameManager.Instance.inGame)
        {
            float rotationValue = value.Get<float>(); // getting the float value
            Debug.Log(rotationValue);
        }
    }
}
