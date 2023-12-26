using System;
using ChessScripts3D.Managers;
using UnityEngine;

namespace ChessScripts3D.InputSystem
{
    public class InputFeedback : SingleTon<InputFeedback>
    {
        private InputActions.GameClickActions _clickActions;
        private InputActions.GameCameraActions _cameraActions;

        public bool isClick;
        public bool goHome;
        
        public Vector3 cameraMovePos;
        public float cameraZoom;
        public bool isRightButton;
        public float cameraYRotate;

        public event Action clicked;

        private void Awake()
        {
            _clickActions = new InputActions().GameClick;
            _cameraActions = new InputActions().GameCamera;
            
            _clickActions.Click.performed += context =>
            {
                clicked?.Invoke();
            };
            _cameraActions.GoHomePos.performed += context =>
            {
                goHome = _cameraActions.GoHomePos.IsPressed();
            };
        }

        private void Update()
        {
            cameraMovePos = _cameraActions.CamMove.ReadValue<Vector3>();
            cameraZoom = _cameraActions.CamZoom.ReadValue<float>();
            isRightButton = _cameraActions.ActiveCamRotate.IsPressed();
            cameraYRotate = _cameraActions.CamRotateY.ReadValue<float>();
        }

        private void OnEnable()
        {
            _cameraActions.Enable();
            _clickActions.Enable();
        }
        private void OnDisable()
        {
            _cameraActions.Disable();
            _clickActions.Disable();
        }
    }
}