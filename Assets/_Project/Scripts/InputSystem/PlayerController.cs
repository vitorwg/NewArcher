using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private MouseSettings _mouseSettings;
    
    private InputReader _inputReader;
    private Rigidbody _playerRb;
    private Vector3 _newPosition;
    private float _rotationVelocity;
    private float _cinemachineTargetPitch;
    private float _topClamp = 80.0f;
    private float _bottomClamp = -80.0f;


    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        HandleCameraRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnLook(Vector2 look)
    {
        _cinemachineTargetPitch += look.y * _mouseSettings.Sensitivity * Time.deltaTime;
        _rotationVelocity = look.x * _mouseSettings.Sensitivity * Time.deltaTime;
    }

    private void OnMove(Vector2 move)
    {
        _newPosition = transform.forward * move.y + transform.right * move.x;
    }

    private void HandleMovement()
    {
        Vector3 nexPos = transform.localPosition + (_newPosition * Time.deltaTime * _moveSpeed);
        _playerRb.MovePosition(nexPos);
    }

    private void HandleCameraRotation()
    {
         _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        _followTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

        transform.Rotate(Vector3.up * _rotationVelocity);
    }
    private void OnEnable()
    {
        _inputReader.OnMoveEvent += OnMove;
        _inputReader.OnLookEvent += OnLook;
    }


    private void OnDisable()
    {
        _inputReader.OnMoveEvent -= OnMove;
        _inputReader.OnLookEvent -= OnLook;
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f)
        {
            lfAngle += 360f;
        }
        if (lfAngle > 360f)
        {
            lfAngle -= 360f;
        }
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}