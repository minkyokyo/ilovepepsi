using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 터치로 인터랙트가 가능한 3d object
 */
public abstract class TouchInteractable3dObject : MonoBehaviour
{
    public abstract void ResponseTouchRequest();
}
