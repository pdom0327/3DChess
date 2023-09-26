using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 initPos;
    public Vector3 whiteBaseViewPos;
    public Vector3 blackBaseViewPos;
    public Vector3 topViewPos;

    public Quaternion whiteBaseViewRot;
    public Quaternion blackBaseViewRot;
    public Quaternion topViewRot;

    public float moveDuration = 3.0f;
    public float rotationDuration = 3.0f;
    
    private bool _isTouring;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Start()
    {
        initPos = new Vector3(0f, 1.25f, -10);
        
        whiteBaseViewPos = new Vector3(12f, 13f, 0f);
        whiteBaseViewRot = Quaternion.Euler(45f, -90f, 0f);
        
        blackBaseViewPos = new Vector3(-12f, 13f, 0f);
        blackBaseViewRot = Quaternion.Euler(45f, 90f, 0f);
        
        topViewPos = new Vector3(0f, 13f, 0f);
        topViewRot = Quaternion.Euler(90f, -90f, 0f);
        
        _startPosition =  initPos;
        _startRotation =  Quaternion.Euler(Vector3.zero);

        StartCoroutine(CameraTouring());
    }

    private IEnumerator CameraTouring()
    {
        _isTouring = true;
        
        Vector3 endPosition;
        Quaternion endRotation;
        
        int a = Random.Range(0, 2);
        if (a > 0)
        {
            endPosition = blackBaseViewPos;
            endRotation = blackBaseViewRot;    
        }
        else
        {
            endPosition = whiteBaseViewPos;
            endRotation = whiteBaseViewRot;
        }
        
        float startTime = Time.time + 1.5f;
        
        while (_isTouring)
        {
            float journeyLength = Time.time - startTime;

            float moveFraction = Mathf.Clamp01(journeyLength / moveDuration);
            float rotationFraction = Mathf.Clamp01(journeyLength / rotationDuration);

            transform.position = Vector3.Lerp(_startPosition, endPosition, moveFraction);
            transform.rotation = Quaternion.Slerp(_startRotation, endRotation, rotationFraction);

            if (moveFraction >= 1.0f && rotationFraction >= 1.0f) { _isTouring = false; }
            
            if (!_isTouring) { break; }

            yield return null;
        }
    }
}
