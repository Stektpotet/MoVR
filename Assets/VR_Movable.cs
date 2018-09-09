using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
//-------------------------------------------------------------------------
[RequireComponent(typeof(Interactable))]
public class VR_Movable : MonoBehaviour
{
    [SerializeField]
    bool isReSizable = true;
    bool resizing;

    [SerializeField]
    private float resizeSpeed = 0.01f;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin(Hand hand)
    {
    }


    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
    }


    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButton() || ((hand.controller != null) && hand.controller.GetPressDown(EVRButtonId.k_EButton_Grip)))
        {
            if (hand.currentAttachedObject != gameObject)
            {
                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(GetComponent<Interactable>());

                // Attach this object to the hand
                hand.AttachObject(gameObject, attachmentFlags);
            }
            else
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(GetComponent<Interactable>());

            }
        }
    }

    private void OnAttachedToHand(Hand hand)
    {
        if (isReSizable)
        {
            resizing = true;
        }
    }

    private void OnDetachedFromHand(Hand hand)
    {
        resizing = false;
    }

    private void HandAttachedUpdate(Hand hand)
    {
        if (hand.controller != null)
        {
            Vector2 axis = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            transform.localScale += (Vector3)axis * resizeSpeed;
        }


    }
}
