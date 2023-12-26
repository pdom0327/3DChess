using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe2 : MonoBehaviour
{
    
    void Update()
    {
        if (DeleteMe.Instance.a)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            DeleteMe.Instance.a = false;
            StartCoroutine(Quit());
        }      
    }

    private IEnumerator Quit()
    {
        yield return new WaitForSeconds(5f);
        
        QuitGame();
    }
    
    public void QuitGame()
    {
        Application.Quit(); // 빌드된 런타임에서 실행 중일 때
    }
}
