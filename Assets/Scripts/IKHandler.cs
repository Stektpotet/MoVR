using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{

    Animator animator;

    public float IK_weight = 1;

    public Transform root;

    public Transform IK_targetLeftFoot;
    public Transform IK_targetRightFoot;

    public Transform IK_hintLeftKnee;
    public Transform IK_hintRightKnee;

    public Transform IK_targetLeftHand;
    public Transform IK_targetRightHand;

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

        animator.SetIKPosition(AvatarIKGoal.LeftHand, IK_targetLeftHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, IK_targetRightHand.position);
            //ROTATION
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,  IK_weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, IK_weight);

        animator.SetIKRotation(AvatarIKGoal.LeftHand, IK_targetLeftHand.rotation * Quaternion.Euler(0,0,90));
        animator.SetIKRotation(AvatarIKGoal.RightHand, IK_targetRightHand.rotation * Quaternion.Euler(0, 0, -90));

        //animator.SetBoneLocalRotation(HumanBodyBones.LeftHand, IK_targetLeftHand.rotation);

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

        //animator.MatchTarget()
        //animator.MatchTarget(IK_head.position, IK_head.rotation, AvatarTarget.)

        animator.SetLookAtPosition(IK_head.position + IK_head.forward);
        animator.SetLookAtWeight(1);


    }
    void OnAnimatorMove() 
    {
        transform.position = new Vector3(IK_head.transform.position.x, transform.position.y, IK_head.transform.position.z);
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

        if (IK_targetLeftHand != null && root != null)
        {
            Gizmos.DrawLine(IK_targetLeftHand.position, root.position);
        }
        if (IK_targetRightHand != null && root != null)
        {
            Gizmos.DrawLine(IK_targetRightHand.position, root.position);
        }

    }
}
