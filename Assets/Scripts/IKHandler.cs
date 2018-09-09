using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class IKHandler : MonoBehaviour
{

    Animator animator;

    public float IK_weight = 1;

    public Vector3 handOffset;
    public float rootPositionForwardOffset;

    public Transform root;

    public Transform IK_targetLeftFoot;
    public Transform IK_targetRightFoot;

    public Transform IK_hintLeftKnee;
    public Transform IK_hintRightKnee;

    

    public Hand IK_LeftHand;
    public Hand IK_RightHand;

    public Transform IK_head;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK( int layerIndex )
    {

        //HANDS
            //POSITION
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IK_weight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, IK_weight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, IK_LeftHand.transform.position + IK_LeftHand.transform.TransformVector(handOffset));
        animator.SetIKPosition(AvatarIKGoal.RightHand, IK_RightHand.transform.position + IK_RightHand.transform.TransformVector(handOffset));
            //ROTATION
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,  IK_weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, IK_weight);

        animator.SetIKRotation(AvatarIKGoal.LeftHand, IK_LeftHand.transform.rotation * Quaternion.Euler(30,10,90));
        animator.SetIKRotation(AvatarIKGoal.RightHand, IK_RightHand.transform.rotation * Quaternion.Euler(30, 10, -90));
        
        //TODO: Add Elbows

        //FEET
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, IK_weight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, IK_weight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, IK_targetLeftFoot.position);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, IK_targetRightFoot.position);

        //KNEES
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, IK_weight);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, IK_weight);

        animator.SetIKHintPosition(AvatarIKHint.LeftKnee, IK_hintLeftKnee.position);
        animator.SetIKHintPosition(AvatarIKHint.RightKnee, IK_hintRightKnee.position);

        //MORE?
        
        //animator.MatchTarget(IK_head.position, IK_head.rotation, AvatarTarget.)

        animator.SetLookAtPosition(IK_head.position + IK_head.forward * 100);
        animator.SetLookAtWeight(1);
        if (Input.GetAxis("PrimaryHandTrigger") >= 0.5f) {
            animator.SetBoneLocalRotation(HumanBodyBones.LeftIndexIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftLittleIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftMiddleIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftRingIntermediate, Quaternion.Euler(0, 0, 90));
        }
        else {
            animator.SetBoneLocalRotation(HumanBodyBones.LeftIndexIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftLittleIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftMiddleIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.LeftRingIntermediate, Quaternion.Euler(0, 0, 0));
        }
        if (Input.GetAxis("SecondaryHandTrigger") >= 0.5f) {
            animator.SetBoneLocalRotation(HumanBodyBones.RightIndexIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.RightLittleIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.RightMiddleIntermediate, Quaternion.Euler(0, 0, 90));
            animator.SetBoneLocalRotation(HumanBodyBones.RightRingIntermediate, Quaternion.Euler(0, 0, 90));
        }
        else {
            animator.SetBoneLocalRotation(HumanBodyBones.RightIndexIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.RightLittleIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.RightMiddleIntermediate, Quaternion.Euler(0, 0, 0));
            animator.SetBoneLocalRotation(HumanBodyBones.RightRingIntermediate, Quaternion.Euler(0, 0, 0));
        }

    }
    void OnAnimatorMove() 
    {
        Vector3 projectedHeadForward = Vector3.ProjectOnPlane(IK_head.forward, Vector3.up).normalized;

        Vector3 combinedForward = (IK_head.forward*2 + IK_LeftHand.transform.forward + IK_RightHand.transform.forward) / 4;
        Vector3 projectedCombinedForward = Vector3.ProjectOnPlane(combinedForward, Vector3.up).normalized;

        transform.rotation = Quaternion.LookRotation(projectedCombinedForward, Vector3.up);
        transform.position = new Vector3(IK_head.position.x, transform.position.y, IK_head.position.z) - projectedHeadForward * rootPositionForwardOffset;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (IK_targetLeftFoot != null && IK_hintLeftKnee != null && root != null)
        {
            Gizmos.DrawLine(IK_hintLeftKnee.position, IK_targetLeftFoot.position);
            Gizmos.DrawLine(IK_hintLeftKnee.position, root.position);
        }
        if (IK_targetRightFoot != null && IK_hintRightKnee != null && root != null)
        {
            Gizmos.DrawLine(IK_hintRightKnee.position, IK_targetRightFoot.position);
            Gizmos.DrawLine(IK_hintRightKnee.position, root.position);
        }
        if (IK_LeftHand != null && root != null)
        {
            Gizmos.DrawLine(IK_LeftHand.transform.position, root.position);
        }
        if (IK_RightHand != null && root != null)
        {
            Gizmos.DrawLine(IK_RightHand.transform.position, root.position);
        }

    }
}
