using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{

    public Vector3 initPos;
    public Vector3 whiteBaseViewPos;
    public Vector3 blackBaseViewPos;
    public Vector3 topViewPos;
    public Vector3 sideViewZMinusPos;
    public Vector3 sideViewZPlusPos;

    public Quaternion whiteBaseViewRot;
    public Quaternion whiteTopViewRot;
    
    public Quaternion blackBaseViewRot;
    public Quaternion blackTopViewRot;

    public Quaternion sideViewZMinusRot;
    public Quaternion sideViewZPlusRot;

    public float moveDuration = 3.0f;
    public float rotationDuration = 3.0f;
    
    private bool _isTouring;
    private bool _isHorizontal;
    private bool _isVertical;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private float _timeCount;

    public Transform centralAxis;
    public float camSpeed;
    private float _mouseX;
    private float _mouseY;

    private void CamMove()
    {
        if (Input.GetMouseButton(1))
        {
            _mouseX += Input.GetAxis("Mouse X") * -1;
            _mouseY += Input.GetAxis("Mouse Y");

            
            //centralAxis.position = 
            centralAxis.rotation =
                Quaternion.Euler(new Vector3(0, centralAxis.rotation.y + _mouseY, centralAxis.rotation.x + _mouseX) * camSpeed);
        }
    }

    void Start()
    {
        initPos = new Vector3(0f, 1.25f, -10);
        
        whiteBaseViewPos = new Vector3(12f, 13f, 0f);
        whiteBaseViewRot = Quaternion.Euler(45f, -90f, 0f);
        
        blackBaseViewPos = new Vector3(-12f, 13f, 0f);
        blackBaseViewRot = Quaternion.Euler(45f, 90f, 0f);
        
        topViewPos = new Vector3(0f, 13f, 0f);
        sideViewZMinusPos = new Vector3(0, 10, -10);
        sideViewZPlusPos = new Vector3(0, 10, 10);
        
        whiteTopViewRot = Quaternion.Euler(90f, -90f, 0f);
        blackTopViewRot = Quaternion.Euler(90f, 90f, 0f);
        
        sideViewZMinusRot = Quaternion.Euler(45f, 0f, 0f);
        sideViewZPlusRot = Quaternion.Euler(45f, 180f, 0f);

        _startPosition =  initPos;
        _startRotation =  Quaternion.Euler(Vector3.zero);

        //StartCoroutine(StartTouring());
    }

    private void Update()
    {
        CamMove();
        
        if (Input.GetKey(KeyCode.A))
        {
            if (SetEndPosition(sideViewZMinusPos))
                return;

            MoveLeft();

            /*if (SetEndPosition(sideViewZPlusPos))
                return;
            
            MoveLeft();*/
        } else if (Input.GetKey(KeyCode.D))
        {
            if (SetEndPosition(sideViewZPlusPos))
                return;

            MoveRight();
            
            /*if (SetEndPosition(sideViewZMinusPos))
                return;

            MoveRight();*/
        } else if (Input.GetKey(KeyCode.W))
        {
            if (SetEndPosition(topViewPos))
                return;

            MoveVertical();
        } else if (Input.GetKey(KeyCode.S))
        {
            if (SetEndPosition(whiteBaseViewPos))
                return;

            MoveVertical();

            /*if (SetEndPosition(blackBaseViewPos))
                return;
            
            MoveVertical();*/
            
            if (transform.position == whiteBaseViewPos)
                _isVertical = false;
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = whiteBaseViewPos;
            transform.rotation = whiteBaseViewRot;
            _timeCount = 0;
            
            _isVertical = false;
            _isHorizontal = false;

            /*transform.position = blackBaseViewPos;
            transform.rotation = blackBaseViewRot;
            _timeCount = 0;*/
        }
    }
    private void MoveLeft()
    {
        if(_isVertical) return;
        
        _isHorizontal = true;
        
        if (transform.position.z > 0f) {
            transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZPlusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZPlusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return ;
        }

        transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZMinusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZMinusRot, _timeCount);
            
        _timeCount += Time.deltaTime; 
        
        
        /*if (Input.GetAxis("Horizontal") < 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZPlusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZPlusRot, _timeCount);
            
            _timeCount += Time.deltaTime;
            Debug.Log(Input.GetAxis("Horizontal"));
        } else if (Input.GetAxis("Horizontal") > 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZMinusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZMinusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;  
        } else if (Input.GetAxis("Horizontal") == 0f) {
            Vector3 movePos = new Vector3(0f, 0f, 0.01f);
            transform.position += movePos;
        }*/
    }

    private void MoveRight()
    {
        if(_isVertical) return;
        
        _isHorizontal = true;
        
        if (transform.position.z < 0f) {
            transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZMinusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZMinusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZPlusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZPlusRot, _timeCount);
            
        _timeCount += Time.deltaTime;    
        
        
        /*if (Input.GetAxis("Horizontal") > 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZMinusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZMinusRot, _timeCount);
            
            _timeCount += Time.deltaTime;
            Debug.Log(Input.GetAxis("Horizontal"));
        } else if (Input.GetAxis("Horizontal") < 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZPlusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZPlusRot, _timeCount);

            _timeCount -= Time.deltaTime;
            Debug.Log(Input.GetAxis("Horizontal"));
        } else if (Input.GetAxis("Horizontal") == 0f) {
            Vector3 movePos = new Vector3(0f, 0f, -0.01f);
            transform.position += movePos;
        }*/
    }

    private void MoveVertical()
    {
        if(_isHorizontal) return;
        
        _isVertical = true;
        
        transform.position = Vector3.Lerp(whiteBaseViewPos, topViewPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, whiteTopViewRot, _timeCount);

        if (Input.GetAxis("Vertical") > 0)
            _timeCount += Time.deltaTime;
        else if (Input.GetAxis("Vertical") < 0)
            _timeCount -= Time.deltaTime;
        
        /*transform.position = Vector3.Lerp(blackBaseViewPos, topViewPos, _timeCount);
        transform.rotation = Quaternion.Slerp(blackBaseViewRot, blackTopViewRot, _timeCount);

        if (Input.GetAxis("Vertical") > 0)
            _timeCount += Time.deltaTime;
        else if (Input.GetAxis("Vertical") < 0)
            _timeCount -= Time.deltaTime;*/
    } 

    private bool SetEndPosition(Vector3 endPos)
    {
        var endPosition = endPos;

        if (endPosition == transform.position)
            return true;

        return false;
    }

    private IEnumerator StartTouring()
    {
        _isTouring = true;
        
        Vector3 endPosition;
        Quaternion endRotation;
        
        // !request : 진영 위치에 따른 요청을 받은후 적용
        int a = Random.Range(0, 2);
        if (a > 0)
        {
            endPosition = blackBaseViewPos;
            endRotation = blackBaseViewRot;
            endPosition = whiteBaseViewPos;
            endRotation = whiteBaseViewRot;
        }
        else
        {
            endPosition = whiteBaseViewPos;
            endRotation = whiteBaseViewRot;
        }
        //
        float startTime = Time.time + 1.5f;
        
        while (_isTouring)
        {
            float touringTimer = Time.time - startTime;

            float moveFraction = Mathf.Clamp01(touringTimer / moveDuration);
            float rotationFraction = Mathf.Clamp01(touringTimer / rotationDuration);

            transform.position = Vector3.Lerp(_startPosition, endPosition, moveFraction);
            transform.rotation = Quaternion.Slerp(_startRotation, endRotation, rotationFraction);

            if (moveFraction >= 1.0f && rotationFraction >= 1.0f) { _isTouring = false; }
            
            if (!_isTouring) { break; }

            yield return null;
        }
    }
}
