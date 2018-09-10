using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBoard : MonoBehaviour
{
    public Text activeOperationHeading;

    private ControlBoardControl activeControl;

    private void LateUpdate()
    {
        if (activeControl != ControlBoardControl.Focused)
        {
            activeControl = ControlBoardControl.Focused;
            activeOperationHeading.text = activeControl.OperationHeading;
        }
    }
}
