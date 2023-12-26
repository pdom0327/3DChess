using System.Collections;
using ChessScripts3D.InputSystem;
using ChessScripts3D.Managers;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChessScripts3D
{
    class CameraMove : MonoBehaviour
    {
        private InputFeedback _input; 
    
        [SerializeField] private float sensitivity = 5.0f;

        [SerializeField] private float wheelMax = 100.0f;
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

        private bool initEnd;
    
        void Start()
        {
            _camera = GetComponent<Camera>();
            _camTransform = transform;
            
            _input = InputFeedback.Instance;

            SetInitPos();
        }
    
        void Update()
        {
            SetInitPos();
            if (_input.goHome)
            {
                _camTransform.position = _homePoint;
                _camTransform.rotation = _homeRotate;
                _input.goHome = false;
            }
            
                
            if (_input.isRightButton)
            {
                _mouseY = _input.cameraYRotate;

                _currentRotation = _camTransform.rotation;
                
                _currentRotation.eulerAngles = new Vector3(
                    _currentRotation.eulerAngles.x
                    , _currentRotation.eulerAngles.y + _mouseY / sensitivity
                    );
                
                _camTransform.rotation = _currentRotation;
            }

            var inputWheel = _input.cameraZoom;
            if (inputWheel > 0 && wheelMin < _camera.fieldOfView)
                _camera.fieldOfView -= wheelSpeed;
            if (inputWheel < 0 && wheelMax > _camera.fieldOfView)
                _camera.fieldOfView += wheelSpeed;

            _movePos = _input.cameraMovePos;
            transform.position += _movePos * (sensitivity * Time.deltaTime);
        }
        
        private void SetInitPos()
        {
            var myColor = PieceManager3D.Instance.color;
            if (initEnd && myColor is "White" or "Black") return;

            if (myColor == "Black")
            {
                _homePoint = new Vector3(8, 17, 0);
                _homeRotate = Quaternion.Euler(65, 270, 0);
                initEnd = true;
            }
            else if (myColor == "White")
            {
                _homePoint = new Vector3(-8, 7, 0);
                _homeRotate = Quaternion.Euler(65, 90, 0);
                initEnd = true;
            } 
            
            _camTransform.position = _homePoint;
            _camTransform.rotation = _homeRotate;
        }
    }
}

