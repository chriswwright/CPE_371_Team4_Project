using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool activated = false;
    public void ActionOn() 
    {
        activated = true;
    }
    public void ActionOff() 
    {
        activated = false;
    }
}
