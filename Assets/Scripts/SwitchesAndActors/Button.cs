using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Switch
{
    private void OnTriggerEnter(Collider other)
    {
        on = true;
    }

    private void OnTriggerExit(Collider other)
    {
        on = false;
    }
}
