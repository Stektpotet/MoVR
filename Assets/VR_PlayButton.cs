using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class VR_PlayButton : MonoBehaviour
{
    public LinearAnimator previewAnimator;
    public AnimationClip animationClip;
    LinearMapping animationMapping;
    
    public UnityEvent onHandHoverBegin;
    public UnityEvent onHandHoverEnd;

    public bool playing;
    public bool loop;


    private float animationTime;
    public float playbackRate = 0;


    private void Awake()
    {
        animationMapping = previewAnimator.linearMapping;
    }

    //-------------------------------------------------
    private void OnHandHoverBegin() { onHandHoverBegin.Invoke(); }
    private void OnHandHoverEnd() { onHandHoverEnd.Invoke(); }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButton() || ((hand.controller != null) && hand.controller.GetPressDown(EVRButtonId.k_EButton_Grip)))
        {
            playing = !playing;
            previewAnimator.animator.Play(animationClip.name, 0, 0);
            animationTime = animationClip.length;
        }
    }

    private void Update()
    {
        if (playing)
        {
            animationMapping.value += playbackRate * Time.deltaTime / animationTime;
            if (animationMapping.value > 1)
            {
                if (loop)
                    animationMapping.value = 0;
                else
                    playing = false;
            }
        }
    }
}
