using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Cursor.visible = false;
    }
}
