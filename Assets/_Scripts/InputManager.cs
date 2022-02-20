using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static GameInputActions inputActions;
    public static event Action<InputActionMap> actionMapChange;
    // Start is called before the first frame update
    
    void Awake()
    {
        inputActions = new GameInputActions();
        ToggleActionMap(inputActions.Player);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
