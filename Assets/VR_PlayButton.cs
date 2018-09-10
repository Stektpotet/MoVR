using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(ControlBoardControl))]
public class VR_PlayButton : MonoBehaviour
{

    public LinearDrive playbackDrive;
    public AnimationClip animationClip;
    LinearMapping animationMapping;
    ControlBoardControl control;


    public UnityEvent onHandHoverBegin;
    public UnityEvent onHandHoverEnd;
    public UnityEvent onStop;
    public UnityEvent onPause;


    public bool playing;
    public bool loop;


    private float animationTime;
    public LinearMappingRemapper playbackRate;


    private void Awake()
    {
        animationMapping = playbackDrive.linearMapping;
        control = GetComponent<ControlBoardControl>();
    }

    //-------------------------------------------------
    private void OnHandHoverBegin() { onHandHoverBegin.Invoke(); }
    private void OnHandHoverEnd() { onHandHoverEnd.Invoke(); }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            hand.HoverLock(GetComponent<Interactable>());

            playing = !playing;
            if(playing)
            {
                animationTime = animationClip.length;
                playbackDrive.UpdateMapping(playbackRate.Value * Time.deltaTime / animationTime);
                control.RequestControlBoardFocus();
            }
        }

        if (hand.GetStandardInteractionButtonUp())
        {
            hand.HoverUnlock(GetComponent<Interactable>());
        }
    }

        private void Update()
    {
        if (playing)
        {
            animationMapping.Value += playbackRate.Value * Time.deltaTime / animationTime;
            if (animationMapping.Value > 1)
            {
                if (loop)
                    animationMapping.Value = 0;
                else
                {
                    playing = false;
                    onStop.Invoke();
                    control.GiveAwayControlBoardFocus();
                    animationMapping.Value = 0;

                }
            }
        }
    }
}
