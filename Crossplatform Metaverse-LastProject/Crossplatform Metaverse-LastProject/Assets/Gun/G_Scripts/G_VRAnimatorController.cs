using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_VRAnimatorController : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 0.5f;

    private Animator anim;
    private Vector3 previousPos;
    private G_VRRig vrRig;

    private void Start()
    {
        anim = GetComponent<Animator>();
        vrRig = GetComponent<G_VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    private void Update()
    {
        //Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        //local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        //Set Animator Values
        float previousDirectionX = anim.GetFloat("directionX");
        float previousDirectionY = anim.GetFloat("directionY");

        anim.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        anim.SetFloat("directionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        anim.SetFloat("directionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
