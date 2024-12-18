using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInputListener : MonoBehaviour
{ 
    private ICameraRotator _cameraRotator;
    private ICharacterJumper _characterJumper;
    private ICharacterMover _characterMover;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        if (TryGetComponent(typeof(ICameraRotator), out var rotator))
        {
            _cameraRotator = (ICameraRotator)rotator;
        }
        
        if (TryGetComponent(typeof(ICharacterJumper), out var jumper))
        {
            _characterJumper = (ICharacterJumper)jumper;
        }   
        
        if (TryGetComponent(typeof(ICharacterMover), out var mover))
        {
            _characterMover = (ICharacterMover)jumper;
        }
    }

    private void OnEnable()
    {
        if (_cameraRotator != null)
        {
            _gameInput.Player.Look.performed += LookOnPerformed;
        }   
    }
    
    private void OnDisable()
    {
        _gameInput.Player.Look.performed -= LookOnPerformed;
    }

    private void LookOnPerformed(InputAction.CallbackContext obj)
    {
        Vector2 rotation = obj.ReadValue<Vector2>();

        rotation.x = Mathf.Clamp(rotation.x, -1, 1);
        rotation.y = Mathf.Clamp(rotation.y, -1, 1);

        _cameraRotator.Rotate(rotation);
    }
}
