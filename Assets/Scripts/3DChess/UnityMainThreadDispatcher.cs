using System.Collections;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ActionCoroutine(IEnumerator action)
    {
        yield return action;
    }

    public static void Enqueue(IEnumerator action)
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.ActionCoroutine(action));
        }
    }
}