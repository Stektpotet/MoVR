using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBoardControl : MonoBehaviour
{

    private static ControlBoardControl focused;
    public static ControlBoardControl Focused { get { return focused; } }

    [SerializeField]
    private ControlBoardControl offloadControl;


    [SerializeField]
    private string operationHeading;
    public string OperationHeading { get { return operationHeading; } }
    
    public void RequestControlBoardFocus()
    {
        focused = this;
    }

    public void GiveAwayControlBoardFocus()
    {
        focused = offloadControl ?? this;
    }
}
