using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICameraRotator, ICharacterMover
{
    [Space]
    [Header("Camera")]

    [SerializeField] 
    private Transform rotationObject;

    
    [SerializeField]
    [Range(0,10)]
    private float mouseSensitivity = 2f;

    [Header("Vertical")]
    [SerializeField] 
    [Range(0, 90)]
    private float maxAngleVerticalCamera = 90;
    
    [SerializeField] 
    [Range(0, 90)]
    private float minAngleVerticalCamera = 90;
    
    [Header("Move")]
    [SerializeField] 
    private Transform moveObject;

    [SerializeField] 
    private float moveSpeed = 1;
    
    [SerializeField]
    private float sprintSpeed = 10;


    private float _currentMoveSpeed;
    
    private float _cameraVerticalRotation = 0;
    private float _cameraHorizontalRotation = 0;

    private void Awake()
    {
        if (rotationObject == null)
            rotationObject = transform;
        
        if (moveObject == null)
            moveObject = transform;
        
    }


    void Start ()
    {   
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
    }
    
    
    public void Rotate(Vector2 rotationInput)
    {
        rotationInput.x *= mouseSensitivity;
        rotationInput.y *= mouseSensitivity;
        
        _cameraVerticalRotation -= rotationInput.y;
        _cameraHorizontalRotation += rotationInput.x;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -minAngleVerticalCamera, maxAngleVerticalCamera);
        
        rotationObject.localEulerAngles = (Vector3.right * _cameraVerticalRotation) + (Vector3.up * _cameraHorizontalRotation);
    }

    public void Move(Vector2 move)
    {
        // Получаем вращение без наклона камеры
        Quaternion yawRotation = Quaternion.Euler(0, rotationObject.eulerAngles.y, 0);

        // Вычисляем вектор движения
        Vector3 moveVector = yawRotation * new Vector3(move.x, 0, move.y);
        
        // Перемещаем объект
        moveObject.Translate(moveVector * _currentMoveSpeed * Time.deltaTime, Space.World); 
    }

    public void Sprint(bool isSprint)
    {
        _currentMoveSpeed = isSprint ? sprintSpeed : moveSpeed;
    }
}
