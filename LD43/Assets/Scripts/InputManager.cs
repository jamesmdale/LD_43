using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

abstract public class InputManager : NetworkBehaviour
{
    public abstract void ProcessSpace(bool isDown);
    public abstract void ProcessShift(bool isDown);

    public abstract void ProcessHorizontalAxis(float axis);
    public abstract void ProcessVerticalAxis(float axis);
}
