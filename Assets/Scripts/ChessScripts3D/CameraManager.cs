using ChessScripts3D.InputSystem;
using ChessScripts3D.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using WarpSquareEngine;
using Color = WarpSquareEngine.Color;

namespace ChessScripts3D
{
    class CameraManager : SingleTon<CameraManager>
    {
        public delegate void SetHomePos(Color color);
        public SetHomePos setHomePos;
        
        private InputFeedback _input; 
    
        [SerializeField] private float sensitivity = 5.0f;
        
        [SerializeField] private float wheelMax = 120.0f;
        [SerializeField] private float wheelMin = 20.0f;
        [SerializeField] private float wheelSpeed = 4.0f;

        private Camera _camera;
        private Transform _camTransform;
        
        private Quaternion _currentRotation;
        private float _mouseX;
        private float _mouseY;

        private Vector3 _movePos;
        
        private Vector3 _homePoint;
        private Quaternion _homeRotate;

        void Awake()
        {
            _camera = GetComponent<Camera>();
            _camTransform = transform;
            _input = InputFeedback.Instance;
            
            setHomePos += SetInitPos;
        }
    
        void Update()
        {
            if (_input.goHome)
            {
                _camTransform.position = _homePoint;
                _camTransform.rotation = _homeRotate;
                _input.goHome = false;
            }

            CameraRotation();

            CameraZooming();

            CameraMoving();
        }
        
        private void SetInitPos(Color color)
        {
            if (color == Color.Black)
            {
                _homePoint = new Vector3(8, 17, 0);
                _homeRotate = Quaternion.Euler(65, 270, 0);
            }
            else if (color == Color.White)
            {
                _homePoint = new Vector3(-8, 7, 0);
                _homeRotate = Quaternion.Euler(65, 90, 0);
            } 
            
            _camTransform.position = _homePoint;
            _camTransform.rotation = _homeRotate;
        }

        private void CameraMoving()
        {
            _movePos = _input.cameraMovePos;
            if (_movePos == Vector3.zero) return;
            
            var camFocusX = 0f;
            var camFocusZ = 0f;
             
            var camForward = _camTransform.forward;
            var camRight = _camTransform.right;
            
            if (_movePos.x < 0)
                camFocusX = camForward.x;
            else if (_movePos.x > 0)
                camFocusX = -camForward.x;

            if (_movePos.z > 0)
                camFocusZ = camRight.z;
            else if (_movePos.z < 0)
                camFocusZ = -camRight.z;

            var camAngleY = _camTransform.eulerAngles.y;
            Vector3 moveDirection;
            
            if ( camAngleY is > 360 or < 180)
                moveDirection = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * new Vector3(-camFocusZ, _movePos.y, camFocusX);
            else
                moveDirection = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * new Vector3(camFocusZ, _movePos.y, -camFocusX);

            transform.position += moveDirection.normalized * (sensitivity * Time.deltaTime);
        }

        private void CameraZooming()
        {
            var inputWheel = _input.cameraZoom;
            if (inputWheel > 0 && wheelMin < _camera.fieldOfView)
                _camera.fieldOfView -= wheelSpeed;
            if (inputWheel < 0 && wheelMax > _camera.fieldOfView)
                _camera.fieldOfView += wheelSpeed;
        }

        private void CameraRotation()
        {
            if (!_input.isRightButton) return;
            
            _mouseY = _input.cameraYRotate;

            _currentRotation = _camTransform.rotation;
                
            _currentRotation.eulerAngles = new Vector3(
                _currentRotation.eulerAngles.x
                , _currentRotation.eulerAngles.y + _mouseY / sensitivity
            );
                
            _camTransform.rotation = _currentRotation;
        }
    }
}

