using UnityEngine;

namespace DefaultNamespace
{
    public class ClickEvent : MonoBehaviour
    {

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    Debug.Log("Clicked on object: " + clickedObject.name);
                    // Perform any additional actions with the clicked object here
                }
            }
        }
        
    }
}