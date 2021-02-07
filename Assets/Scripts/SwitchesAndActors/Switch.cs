using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Actor[] targets;
    public bool on = false;

    private void Update()
    {
        if (on)
        {
            foreach (Actor target in targets)
            {
                target.ActionOn();
            }
            
        }
        else
        {
            foreach (Actor target in targets)
            {
                target.ActionOff();
            }
        }
    }


}
