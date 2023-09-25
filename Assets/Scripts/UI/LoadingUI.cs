using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator StartFade(bool isFadeIn)
    {
        float timer = 0;
        
        while (1 >= timer)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }
    }
}
