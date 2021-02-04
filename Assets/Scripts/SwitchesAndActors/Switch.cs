using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Actor target;
    public bool on = false;

    private void Update()
    {
        if (on)
        {
            target.ActionOn();
        }
        else
        {
            target.ActionOff();
        }
    }


}
