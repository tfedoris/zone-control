using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public abstract class Interaction : MonoBehaviour
{
    public abstract void OnTouch(Touch touch, Vector3 touchPosition);
}
