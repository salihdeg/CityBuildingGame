using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentMoveSpeed;

    [SerializeField] private float _minXRot;
    [SerializeField] private float _maxXRot;

    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _currentXRot;
    private float _currentZoom;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _currentZoom = _camera.transform.localPosition.y;
        _currentXRot = -50;
    }

    private void Start()
    {
        _currentMoveSpeed = _moveSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _currentMoveSpeed = _moveSpeed * 2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _currentMoveSpeed = _moveSpeed;

        // Camera Zoom
        _currentZoom += Input.GetAxis("Mouse ScrollWheel") * -_zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

        _camera.transform.localPosition = Vector3.up * _currentZoom;


        // Camera Rotation
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            _currentXRot += -y * _rotationSpeed;
            _currentXRot = Mathf.Clamp(_currentXRot, _minXRot, _maxXRot);
            transform.eulerAngles = new Vector3(_currentXRot, transform.eulerAngles.y + (x * _rotationSpeed), 0f);

            //Cursor.lockState = CursorLockMode.Locked;
        }

        //if(Input.GetMouseButtonUp(1))
        //    Cursor.lockState = CursorLockMode.None;

        // Camera Movement
        Vector3 forward = _camera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = _camera.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = (forward * moveZ) + (right * moveX);

        dir.Normalize();

        dir *= _currentMoveSpeed * Time.deltaTime;

        transform.position += dir;
    }
}
