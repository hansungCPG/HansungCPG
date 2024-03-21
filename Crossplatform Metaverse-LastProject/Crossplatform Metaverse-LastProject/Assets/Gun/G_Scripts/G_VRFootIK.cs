using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_VRFootIK : MonoBehaviour
{
    //발이 땅을 통과하지 않게 하는 스크립트
    private Animator anim;

    public Vector3 footOffset;
    [Range(0,1)]
    public float rightFootPosWeight = 1f;
    [Range(0, 1)]
    public float rightFootRotWeight = 1f;
    [Range(0, 1)]
    public float leftFootPosWeight = 1f;
    [Range(0, 1)]
    public float leftFootRotWeight = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 rightFootPos = anim.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            //Raycast에서 탐지한 땅 위치에 발을 딛게 함
            anim.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion RightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, RightFootRotation);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        Vector3 leftFootPos = anim.GetIKPosition(AvatarIKGoal.LeftFoot);

        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            //Raycast에서 탐지한 땅 위치에 발을 딛게 함
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion LeftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootRotation);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
