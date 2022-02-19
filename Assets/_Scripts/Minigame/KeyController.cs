using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject keyToUse;
    [SerializeField]
    private Camera miniGameCamera;
    [SerializeField]
    private Vector2 mousePos;

    void Start()
    {
        keyToUse = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyToUse != null)
        {
            Vector3 mousePositionOnScreen = new Vector3(mousePos.x, mousePos.y, miniGameCamera.WorldToScreenPoint(keyToUse.transform.position).z);
            Vector3 keyWorldPosition = miniGameCamera.ScreenToWorldPoint(mousePositionOnScreen);
            keyToUse.transform.position = new Vector3(keyWorldPosition.x, keyWorldPosition.y, keyToUse.transform.position.z);

            keyToUse.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    // mouse click events;
    public void OnControlLock(InputValue value)
    {
        if (GameManager.Instance.inGame)
        {
            Debug.Log("ControlEventTest");
            RaycastHit hit = CastRay();

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Key"))
                {
                    keyToUse = hit.collider.gameObject;
                } else
                {
                    keyToUse = null;
                }
            }           
        }
    }

    public void OnMousePosition(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    private RaycastHit CastRay()
    {
        // MUST USE THE INPUT SYSTEM FROM THE 
        Vector3 screenMousePosFar = new Vector3(mousePos.x, mousePos.y, miniGameCamera.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(mousePos.x, mousePos.y, miniGameCamera.nearClipPlane);

        Vector3 worldMousePosFar = miniGameCamera.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = miniGameCamera.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
        
    }

    private void ResetState()
    {
        if (keyToUse != null)
        {
            
            keyToUse = null;
        }
    }
}
