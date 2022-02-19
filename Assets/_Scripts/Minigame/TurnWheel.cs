using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnWheel : MonoBehaviour
{
    [SerializeField] private DifficultyEnum difficulty;

    [SerializeField] private bool m_bIsMovingLeft;
    [SerializeField] private bool m_bIsMovingRight;

    [SerializeField] private float m_fSpeedModifier;
    [SerializeField] private bool m_bActiveLock;
    float m_fRotationValue;

    // Start is called before the first frame update
    void Start()
    {
        m_bIsMovingLeft = false;
        m_bIsMovingRight = false;
        m_fRotationValue = 0;
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (m_fRotationValue * m_fSpeedModifier));
    }
    public void OnTurnWheel(InputValue value)
    {
        if (GameManager.Instance.inGame)
        {
            m_fRotationValue = value.Get<float>(); // getting the float value
            Debug.Log(m_fRotationValue);
            if (m_fRotationValue < 0)
            {
                m_bIsMovingLeft = true;
            } else if (m_fRotationValue > 0)
            {
                m_bIsMovingRight = true;
            } else
            {
                m_bIsMovingLeft = false;
                m_bIsMovingRight = false;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            // TO DO: reset functionality
            Debug.Log("Key Touch, Restart");
        }
    }
}