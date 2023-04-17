using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 _moveDirection = Vector2.zero;
    private Vector3 _mousePosition = Vector3.zero;
    private bool _rightMousePressed = false;
    private bool _leftMousePressed = false;
    private bool _interactPressed = false;
    private bool _submitPressed = false;
    private bool _jumpPressed = false;
    private bool _escapePressed = false;
    private bool _savePressed = false;
    private bool _loadPressed = false;
    private bool _upPressed = false;
    private bool _rightPressed = false;
    private bool _leftPressed = false;
    private bool _downPressed = false;
    private bool _shiftPressed = false;
    private bool _firePressed = false;
    private bool _lookPressed = false;
    private bool _reloadPressed = false;
    [SerializeField] private Camera _camera;
    public bool _useMainCam = true;

    private static InputManager _instance;

    
    private void Awake()
    {
        SceneManager.activeSceneChanged += SwitchCam;
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (_useMainCam)
        {
            _camera = Camera.main;
        }
    }

    public void SwitchCam(Scene current, Scene next)
    {
        _camera = Camera.main;
    }

    public static InputManager GetInstance()
    {
        return _instance;
    }

    /* Move */
    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
    }
    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    /* Mouse Move */
    public void MousePressed(InputAction.CallbackContext context)
    {
        // Debug.Log(context);
        if (context.performed)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }
    }
    public Vector2 GetMousePosition()
    {
        return _camera.ScreenToWorldPoint(_mousePosition);
    }
    public Vector2 GetRawMousePosition()
    {
        return _mousePosition;
    }

    /* Right Click */
    public void RightMouseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rightMousePressed = true;
        }
        else if (context.canceled)
        {
            _rightMousePressed = false;
        }
    }

    public bool GetRightMousePressed()
    {
        bool result = _rightMousePressed;
        _rightMousePressed = false;
        return result;
    }

    /* Left Click */
    public void LeftMouseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftMousePressed = true;
        }
        else if (context.canceled)
        {
            _leftMousePressed = false;
        }
    }
    public bool GetLeftMousePressed()
    {
        bool result = _leftMousePressed;
        _leftMousePressed = false;
        return result;
    }

    /* Interact */
    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactPressed = true;
        }
        else if (context.canceled)
        {
            _interactPressed = false;
        }
    }

    public bool GetInteractPressed()
    {
        bool result = _interactPressed;
        _interactPressed = false;
        return result;
    }


    /* Submit */
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _submitPressed = true;
        }
        else if (context.canceled)
        {
            _submitPressed = false;
        }
    }

    public bool GetSubmitPressed()
    {
        bool result = _submitPressed;
        _submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        //_submitPressed = false;
    }
    /* Jump */
    public void JumpButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpPressed = true;
        }
        else if (context.canceled)
        {
            _jumpPressed = false;
        }
    }

    public bool GetJumpPressed()
    {
        bool result = _jumpPressed;
        _jumpPressed = false;
        return result;
    }

    /* Escape */
    public void EscapeButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _escapePressed = true;
        }
        else if (context.canceled)
        {
            _escapePressed = false;
        }
    }

    public bool GetEscapePressed()
    {
        bool result = _escapePressed;
        _escapePressed = false;
        return result;
    }

    /* Save */
    public void SaveButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _savePressed = true;
        }
        else if (context.canceled)
        {
            _savePressed = false;
        }
    }

    public bool GetSavePressed()
    {
        bool result = _savePressed;
        _savePressed = false;
        return result;
    }

    /* Load */

    public void LoadButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _loadPressed = true;
        }
        else if (context.canceled)
        {
            _loadPressed = false;
        }
    }

    public bool GetLoadPressed()
    {
        bool result = _loadPressed;
        _loadPressed = false;
        return result;
    }

    /* Up */

    public void UpButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _upPressed = true;
        }
        else if (context.canceled)
        {
            _upPressed = false;
        }
    }

    public bool GetUpPressed()
    {
        bool result = _upPressed;
        //_upPressed = false;
        return result;
    }

    /* Right */
    public void RightButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rightPressed = true;
        }
        else if (context.canceled)
        {
            _rightPressed = false;
        }
    }

    public bool GetRightPressed()
    {
        bool result = _rightPressed;
        _rightPressed = false;
        return result;
    }

    /* Left */
    public void LeftButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftPressed = true;
        }
        else if (context.canceled)
        {
            _leftPressed = false;
        }
    }

    public bool GetLeftPressed()
    {
        bool result = _leftPressed;
        _leftPressed = false;
        return result;
    }

    /* Down */
    public void DownButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _downPressed = true;
        }
        else if (context.canceled)
        {
            _downPressed = false;
        }
    }

    public bool GetDownPressed()
    {
        bool result = _downPressed;
        //_downPressed = false;
        return result;
    }

    /* Shift */
    public void ShiftButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _shiftPressed = true;
        }
        else if (context.canceled)
        {
            _shiftPressed = false;
        }
    }

    // Hold
    public bool GetShiftPressed()
    {
        bool result = _shiftPressed;
        return result;
    }

    // Fire
    public void FireButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _firePressed = true;
        }
        else if (context.canceled)
        {
            _firePressed = false;
        }
    }

    public bool GetFirePressed()
    {
        bool result = _firePressed;
        _firePressed = false;
        return result;
    }


    // Look
    public void LookButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookPressed = true;
        }
        else if (context.canceled)
        {
            _lookPressed = false;
        }
    }

    public bool GetLookPressed()
    {
        bool result = _lookPressed;
        // _lookPressed = false;
        return result;
    }
    /* Reload */
    public void ReloadButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _reloadPressed = true;
        }
        else if (context.canceled)
        {
            _reloadPressed = false;
        }
    }

    public bool GetReloadPressed()
    {
        bool result = _reloadPressed;
        _reloadPressed = false;
        return result;
    }
}
