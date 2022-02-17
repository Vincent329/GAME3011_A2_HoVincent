using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement VAriables")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 playerVelocity;
    private Rigidbody rb;

    [Header("Minigame Activation Variables")]
    private bool inRange;
    public bool activeGame;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerVelocity * playerSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Input for movement
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputValue value)
    {
        if (!GameManager.Instance.inGame)
        {
            Debug.Log("Moving");
            Vector2 moveValue = value.Get<Vector2>();
            playerVelocity = new Vector3(moveValue.x, 0, moveValue.y);
        }
    }

    /// <summary>
    /// Pressing the interact button
    /// </summary>
    /// <param name="value"></param>
    public void OnInteract(InputValue value)
    {
        Debug.Log("Interacting");
        GameManager.Instance.ToggleCameras(); // will have to change depending on what difficulty
        playerVelocity = Vector3.zero; // zero out the velocity of the player
    }
}
