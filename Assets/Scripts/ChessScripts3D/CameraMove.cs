using UnityEngine;
using UnityEngine.InputSystem;

namespace ChessScripts3D
{
    class CameraMove : MonoBehaviour
    {
        public InputActions.GameCameraActions actions;
    
        [SerializeField] private float sensitivity = 3.0f;

        [SerializeField] private float wheelMax = 100.0f;
        [SerializeField] private float wheelMin = 20.0f;
        [SerializeField] private float wheelSpeed = 4.0f;

        private Quaternion _currentRotation;
        private float _mouseX;
        private float _mouseY;
        private float _xRot;
        private float _yRot;

        private Vector3 _movePos;
    
        private float xSpeed;
        private float ySpeed;
        private float zSpeed;
    
        private Vector3 _homePoint;
        private Quaternion _homeRotate;
    
        void Start()
        {
            actions = new InputActions().GameCamera;
    
            _homePoint = new Vector3(-8, 7, 0);
            _homeRotate = Quaternion.Euler(65, 90, 0);
        }
    
        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
        {
            camTransform.position = _homePoint;
            camTransform.rotation = _homeRotate;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 rotation = Vector3.zero;
            
            _mouseY = Input.GetAxis("Mouse X") * (sensitivity / 2f);
            _mouseX = Input.GetAxis("Mouse Y") * (sensitivity / 2f);

            rotation += new Vector3(
                camTransform.eulerAngles.x + _mouseX
                ,camTransform.eulerAngles.y + _mouseY);

            _currentRotation.eulerAngles = rotation;
            camTransform.rotation = _currentRotation;
        }*/

            /*var inputWheel = Input.mouseScrollDelta;
        if (inputWheel.y > 0 && wheelMin < _camera.fieldOfView)
            _camera.fieldOfView -= wheelSpeed;
        if (inputWheel.y < 0 && wheelMax > _camera.fieldOfView)
            _camera.fieldOfView += wheelSpeed;*/

            _movePos = actions.CamMove.ReadValue<Vector3>();
            
            transform.position += _movePos * (sensitivity * Time.deltaTime);
            
        }
        private void OnEnable()
        {
            actions.Enable();
        }
        private void OnDisable()
        {
            actions.Disable();
        }
    }
}

