using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR 
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition = Input.mousePosition;
            if(LookFor3dObject(out RaycastHit hit, touchPosition))
            {
                GameObject touchedObject = hit.collider.gameObject;
                if (touchedObject.TryGetComponent(out TouchInteractable3dObject touchInteractable3dObject))
                {
                    touchInteractable3dObject.ResponseTouchRequest();
                }
            }
        }
#else
    
#endif
    }

    private bool LookFor3dObject(out RaycastHit hit,Vector3 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        return Physics.Raycast(ray, out hit);
    }
}
