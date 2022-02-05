using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HexGridProject.Camera
{
    public class CameraInput : MonoBehaviour
    {
        public static bool locked;

        [Header("Camera Position")]
        public Vector2 offset;

        public float startingZoomLevel;
        public float rotateMovementSpeed;
        public float screenMoveSpeed;

        private bool isRotating;
        private bool isTopDownView;
        private UnityEngine.Camera _camera;
        private Rigidbody _rigidBody;
        private float _mousePositionOnRotateStart;
        private float _currentZoomLevel;
        private float _minZoom = -12f;
        private float _maxZoom = 12f;
        private Coroutine _moveCamera;
        private Coroutine _rotateCamera;
        private Vector2 MoveDirection;
        private Vector3 _normalisedCameraPosition;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _rigidBody = GetComponent<Rigidbody>();
            _camera = GetComponentInChildren<UnityEngine.Camera>();
            _normalisedCameraPosition = new Vector3(
                0f,
                Mathf.Abs(offset.y),
                -Mathf.Abs(offset.x)).normalized;
            _currentZoomLevel = startingZoomLevel;
            PositionCamera();
        }

        public void OnCameraScroll(InputAction.CallbackContext context)
        {
            if (context.performed && !isTopDownView && !locked)
            {
                float value = context.ReadValue<float>() / 120f;

                if (value > 0f)
                {
                    if (_currentZoomLevel <= _minZoom) return;
                    _currentZoomLevel = Mathf.Max(_currentZoomLevel - value, _minZoom);
                    PositionCamera();
                }
                else if (value < 0f)
                {
                    if (_currentZoomLevel >= _maxZoom) return;
                    _currentZoomLevel = Mathf.Min(_currentZoomLevel - value, _maxZoom);
                    PositionCamera();
                }
            }
        }

        public void OnTopDownViewButton(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (!isTopDownView)
                {
                    _currentZoomLevel = startingZoomLevel;
                    PositionCamera();
                    _transform.rotation = Quaternion.Euler(45f, 0f, 0f);
                    _rotateCamera = null;

                    isTopDownView = true;
                }
                else
                {
                    _transform.rotation = Quaternion.identity;

                    isTopDownView = false;
                }
            }
        }

        public void OnCameraRotate(InputAction.CallbackContext context)
        {
            if (context.performed && !locked)
            {
                isRotating = !isRotating;
                _mousePositionOnRotateStart = Mouse.current.position.ReadValue().x;

                if (_rotateCamera == null && !isTopDownView)
                    _rotateCamera = StartCoroutine(Rotate());
            }
        }

        public void OnCameraMoveButton(InputAction.CallbackContext context)
        {
            if (context.performed && !locked)
            {
                MoveDirection = context.ReadValue<Vector2>();

                if (_moveCamera == null)
                    _moveCamera = StartCoroutine(Move());
            }
        }

        private IEnumerator Move()
        {
            while (MoveDirection != Vector2.zero && !locked)
            {
                _rigidBody.drag = 1f;
                Vector3 force = new Vector3(MoveDirection.x, 0, MoveDirection.y) * screenMoveSpeed * Time.deltaTime;
                _rigidBody.AddRelativeForce(force);

                yield return new WaitForFixedUpdate();
            }

            _rigidBody.drag = 3f;

            _moveCamera = null;
        }

        private IEnumerator Rotate()
        {
            while (isRotating && !locked)
            {
                float currentMousePosition = Mouse.current.position.ReadValue().x;

                if (_mousePositionOnRotateStart != currentMousePosition)
                {
                    float rotateAmount = currentMousePosition < _mousePositionOnRotateStart ? -1f : 1f;
                    float mouseSpeedRotation = Mathf.Abs(_mousePositionOnRotateStart - currentMousePosition) / Screen.width;
                    _mousePositionOnRotateStart = currentMousePosition;
                    rotateAmount *= rotateMovementSpeed * Time.deltaTime * mouseSpeedRotation;

                    _transform.rotation *= Quaternion.Euler(0f, rotateAmount, 0f);
                }
                yield return null;
            }

            _rotateCamera = null;
        }

        private void PositionCamera()
        {
            _camera.transform.localPosition = _normalisedCameraPosition * _currentZoomLevel;
            _camera.transform.localPosition = new Vector3(
                0f,
                _camera.transform.localPosition.y,
                _camera.transform.localPosition.z - 22f);
            _camera.transform.LookAt(new Vector3(_transform.position.x,
                0f,
                _transform.position.z));
        }
    }
}
