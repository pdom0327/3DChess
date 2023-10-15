using System.Collections;
using HttpRequest;
using UnityEngine;


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

    private string _playerColor;
    
    private Vector3 _basePos;
    private Vector3 _endPosLeft;
    private Vector3 _endPosRight;

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

    private static CameraController _instance;

    public static CameraController Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = (CameraController)FindObjectOfType(typeof(CameraController));
            }
            return _instance;
        }
    }
    
    /*private void CamMove()
    {
        if (Input.GetMouseButton(1))
        {
            _mouseX += Input.GetAxis("Mouse X");
            _mouseY += Input.GetAxis("Mouse Y") * -1;
            
            float horizontalMovement = Input.GetAxis("Mouse X") * -0.8f;
            float verticalMovement = Input.GetAxis("Mouse Y") * -0.8f;

            Vector3 newPosition = new Vector3(horizontalMovement, verticalMovement, 0f);
            centralAxis.position += newPosition;
            
            centralAxis.rotation =
                Quaternion.Euler(new Vector3((centralAxis.rotation.y + _mouseY) * camSpeed, (centralAxis.rotation.x + _mouseX) * camSpeed, 0f));
        }
    }*/

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
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (SetEndPosition(_endPosLeft))
                return;

            MoveLeft();
        } else if (Input.GetKey(KeyCode.D))
        {
            if (SetEndPosition(_endPosRight))
                return;

            MoveRight();
        } else if (Input.GetKey(KeyCode.W))
        {
            if (SetEndPosition(topViewPos))
                return;

            MoveVertical();
        } else if (Input.GetKey(KeyCode.S))
        {
            if (SetEndPosition(_basePos))
                return;

            MoveVertical();
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            BasePos();
        }
    }
    private void MoveLeft()
    {
        if(_isVertical) return;
        
        _isHorizontal = true;

        ForWhiteLeft();
        ForBlackLeft();
    }

    private void MoveRight()
    {
        if(_isVertical) return;
        
        _isHorizontal = true;

        ForWhiteRight();
        ForBlackRight();
    }

    private void MoveVertical()
    {
        if(_isHorizontal) return;
        
        _isVertical = true;

        ForWhiteVertical();
        ForBlackVertical();
        
        if (Input.GetAxis("Vertical") > 0)
            _timeCount += Time.deltaTime;
        else if (Input.GetAxis("Vertical") < 0)
            _timeCount -= Time.deltaTime;
    }

    private void ForWhiteLeft()
    {
        if(_playerColor == "Black") return;
        
        if (transform.position.z > 0f) {
            transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZPlusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZPlusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return ;
        }

        transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZMinusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZMinusRot, _timeCount);
            
        _timeCount += Time.deltaTime;
    }
    private void ForBlackLeft()
    {
        if(_playerColor == "White") return;
        
        if (transform.position.z < 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZMinusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZMinusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZPlusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZPlusRot, _timeCount);
            
        _timeCount += Time.deltaTime;
    }
    
    private void ForWhiteRight()
    {
        if(_playerColor == "Black") return;
        
        if (transform.position.z < 0f) {
            transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZMinusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZMinusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.Lerp(whiteBaseViewPos, sideViewZPlusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, sideViewZPlusRot, _timeCount);
            
        _timeCount += Time.deltaTime;
    }
    private void ForBlackRight()
    {
        if(_playerColor == "White") return;
        
        if (transform.position.z > 0f) {
            transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZPlusPos, _timeCount);
            transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZPlusRot, _timeCount);
            
            _timeCount -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.Lerp(blackBaseViewPos, sideViewZMinusPos, _timeCount);
        transform.rotation = Quaternion.Slerp(blackBaseViewRot, sideViewZMinusRot, _timeCount);
            
        _timeCount += Time.deltaTime;
    }

    private void ForWhiteVertical()
    {
        if(_playerColor == "Black") return;
        
        transform.position = Vector3.Lerp(whiteBaseViewPos, topViewPos, _timeCount);
        transform.rotation = Quaternion.Slerp(whiteBaseViewRot, whiteTopViewRot, _timeCount);
        
        if (transform.position == whiteBaseViewPos)
            _isVertical = false;
    }
    
    private void ForBlackVertical()
    {
        if(_playerColor == "White") return;
        
        transform.position = Vector3.Lerp(blackBaseViewPos, topViewPos, _timeCount);
        transform.rotation = Quaternion.Slerp(blackBaseViewRot, blackTopViewRot, _timeCount);
        
        if (transform.position == blackBaseViewPos)
            _isVertical = false;
    }

    private void BasePos()
    {
        if (_playerColor == "White")
        {
            transform.position = whiteBaseViewPos;
            transform.rotation = whiteBaseViewRot;    
        }
        else
        {
            transform.position = blackBaseViewPos;
            transform.rotation = blackBaseViewRot;    
        }
        _timeCount = 0;
        
        _isVertical = false;
        _isHorizontal = false;
    }
    
    private bool SetEndPosition(Vector3 endPos)
    {
        var endPosition = endPos;

        if (endPosition == transform.position)
            return true;

        return false;
    }

    public IEnumerator StartTouring(string getColor)
    {
        _isTouring = true;
        _playerColor = getColor;
        
        Vector3 cinemaEndPosition = Vector3.zero;
        Quaternion cinemaEndRotation = default;
        
        if (_playerColor == "White")
        {
            cinemaEndPosition = whiteBaseViewPos;
            cinemaEndRotation = whiteBaseViewRot;

            _basePos = whiteBaseViewPos;
            _endPosLeft = sideViewZMinusPos;
            _endPosRight = sideViewZPlusPos;
        } else if (_playerColor == "Black")
        {
            cinemaEndPosition = blackBaseViewPos;
            cinemaEndRotation = blackBaseViewRot;

            _basePos = blackBaseViewPos;
            _endPosLeft = sideViewZPlusPos;
            _endPosRight = sideViewZMinusPos;
        }

        float startTime = Time.time + 1.5f;
        
        while (_isTouring)
        {
            float touringTimer = Time.time - startTime;

            float moveFraction = Mathf.Clamp01(touringTimer / moveDuration);
            float rotationFraction = Mathf.Clamp01(touringTimer / rotationDuration);

            transform.position = Vector3.Lerp(_startPosition, cinemaEndPosition, moveFraction);
            transform.rotation = Quaternion.Slerp(_startRotation, cinemaEndRotation, rotationFraction);

            if (moveFraction >= 1.0f && rotationFraction >= 1.0f) { _isTouring = false; }
            
            if (!_isTouring) { break; }

            yield return null;
        }
    }
}
