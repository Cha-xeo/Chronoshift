using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(PlayerInput))]
//namespace Chronoshift.Input
//{


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
    private bool _anyKeyPressed = false;
    private bool _journalPressed = false;
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

    // Move 
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

    // Mouse Move 
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

    // Right Mouse Click
    public void RightMouseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rightMousePressed = true;
        }
    }

    public bool GetRightMousePressed()
    {
        bool result = _rightMousePressed;
        return result;
    }
    // Left Mouse Hold
    private bool _leftMouseHold = false;
    public void LeftMouseButtonHolded(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftMouseHold = true;
        }
        else
        {
            _leftMouseHold = false;
        }
    }
    public bool GetLeftMouseHolded()
    {
        bool result = _leftMouseHold;
        return result;
    }
    // Left Mouse Click
    public void LeftMouseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftMousePressed = true;
        }
    }
    public bool GetLeftMousePressed()
    {
        bool result = _leftMousePressed;
        return result;
    }

    // Submit
    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactPressed = true;
        }
        else if(context.canceled)
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

    /*AnyKey */
    public void AnyKeyButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _anyKeyPressed = true;
        }
        else if (context.canceled)
        {
            _anyKeyPressed = false;
        }
    }

    public bool GetAnyKeyPressed()
    {
        bool result = _anyKeyPressed;
        _anyKeyPressed = false;
        return result;
    }


    // Submit
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _submitPressed = true;
        }
        /*else if (context.canceled)
        {
            _submitPressed = false;
        }*/
    }

    public bool GetSubmitPressed()
    {
        bool result = _submitPressed;
        //_submitPressed = false;
        return result;
    }

    //Journal
    
    public void JournalPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _journalPressed = true;
        }
        else if (context.canceled)
        {
            _journalPressed = false;
        }
    }

    public bool GetJournalPressed()
    {
        bool result = _journalPressed;
        _journalPressed = false;
        return result;
    }

    // Jump
    public void JumpButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpPressed = true;
        }
    }

    public bool GetJumpPressed()
    {
        bool result = _jumpPressed;
        return result;
    }

    // Escape
    public void EscapeButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _escapePressed = true;
        }
    }

    public bool GetEscapePressed()
    {
        bool result = _escapePressed;
        return result;
    }

    // Save
    public void SaveButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _savePressed = true;
        }
    }

    public bool GetSavePressed()
    {
        bool result = _savePressed;
        return result;
    }

    // Load

    public void LoadButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _loadPressed = true;
        }
    }

    public bool GetLoadPressed()
    {
        bool result = _loadPressed;
        return result;
    }

    // Up

    public void UpButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _upPressed = true;
        }
    }

    public bool GetUpPressed()
    {
        bool result = _upPressed;
        //_upPressed = false;
        return result;
    }

    // Right
    public void RightButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rightPressed = true;
        }
    }

    public bool GetRightPressed()
    {
        bool result = _rightPressed;
        return result;
    }

    // Left
    public void LeftButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftPressed = true;
        }
    }

    public bool GetLeftPressed()
    {
        bool result = _leftPressed;
        return result;
    }

    // Down 
    public void DownButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _downPressed = true;
        }
    }

    public bool GetDownPressed()
    {
        bool result = _downPressed;
        //_downPressed = false;
        return result;
    }

    // Shift 
    public void ShiftButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _shiftPressed = true;
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
    }

    public bool GetFirePressed()
    {
        bool result = _firePressed;
        return result;
    }


    // Look
    public void LookButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookPressed = true;
        }
    }

    public bool GetLookPressed()
    {
        bool result = _lookPressed;
        // _lookPressed = false;
        return result;
    }
    // Reload 
    public void ReloadButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _reloadPressed = true;
        }
    }

    public bool GetReloadPressed()
    {
        bool result = _reloadPressed;
        return result;
    }
    private bool _spellBPressed = false;
    public void BButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _spellBPressed = true;
        }
        /*else if (context.canceled)
        {
            _spellBPressed = false;
        }*/
    }

    public bool GetBPressed()
    {
        bool result = _spellBPressed;
        // _spellBPressed = false;
        return result;
    }
    private bool _spellAPressed = false;
    public void AButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _spellAPressed = true;
        }
        /*else if (context.canceled)
        {
            _spellAPressed = false;
        }*/
    }

    public bool GetAPressed()
    {
        bool result = _spellAPressed;
        //_spellAPressed = false;
        return result;
    }

    private void LateUpdate()
    {
        _rightMousePressed = false;
        _leftMousePressed = false;
        //_interactPressed = false;
        _submitPressed = false;
        _jumpPressed = false;
        _escapePressed = false;
        _savePressed = false;
        _loadPressed = false;
        _upPressed = false;
        _rightPressed = false;
        _leftPressed = false;
        _downPressed = false;
        _shiftPressed = false;
        _firePressed = false;
        _lookPressed = false;
        _reloadPressed = false;
    }
}
//}
