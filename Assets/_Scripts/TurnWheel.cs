using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnWheel : MonoBehaviour
{
    [SerializeField] private bool isMovingLeft;
    [SerializeField] private bool isMovingRight;
    float rotationValue;
    // Start is called before the first frame update
    void Start()
    {
        isMovingLeft = false;
        isMovingRight = false;
        rotationValue = 0;
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotationValue);
    }
    public void OnTurnWheel(InputValue value)
    {
        if (GameManager.Instance.inGame)
        {
            rotationValue = value.Get<float>(); // getting the float value
            Debug.Log(rotationValue);
            if (rotationValue < 0)
            {
                isMovingLeft = true;
            } else if (rotationValue > 0)
            {
                isMovingRight = true;
            } else
            {
                isMovingLeft = false;
                isMovingRight = false;
            }

        }
    }
}
