using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Switch
{
    private void OnTriggerStay(Collider other)
    {
        on = true;
    }

    private void OnTriggerExit(Collider other)
    {
        on = false;
    }
}
