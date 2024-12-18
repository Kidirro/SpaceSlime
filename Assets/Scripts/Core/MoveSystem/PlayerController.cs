using UnityEngine;

public class PlayerController : MonoBehaviour, ICameraRotator
{
    [SerializeField] 
    private Transform rotationObject;

    [Space]
    [Header("Camera")]
    
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
    
    

    private float _cameraVerticalRotation = 0;
    private float _cameraHorizontalRotation = 0;
    
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
}
