using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector {get; private set;}
    public Vector3 MousePosition {get; private set;}
    public Vector3 RawMousePosition {get; private set;}
    public Vector3 MousePositionGround {get; private set;}
    public bool LeftClick {get; private set;}
    public bool Shift {get; private set;}
    public bool Fire {get; private set;}
    public bool Look {get; private set;}
    public bool Reload {get; private set;}
    public bool Submit {get; private set;}
    public bool Up {get; private set;}
    public bool Down {get; private set;}
    private void Update()
    {
        Submit = InputManager.GetInstance().GetSubmitPressed();
        Up = InputManager.GetInstance().GetUpPressed();
        Down = InputManager.GetInstance().GetDownPressed();
        InputVector = InputManager.GetInstance().GetMoveDirection();
        // MousePosition = InputManager.GetInstance().GetMousePosition();
        // RawMousePosition = InputManager.GetInstance().GetRawMousePosition();
        // LeftClick = InputManager.GetInstance().GetLeftMousePressed();
        // Reload = InputManager.GetInstance().GetReloadPressed();
        // Shift = InputManager.GetInstance().GetShiftPressed();
        // Fire = InputManager.GetInstance().GetFirePressed();
        // Look = InputManager.GetInstance().GetLookPressed();
    }
}
