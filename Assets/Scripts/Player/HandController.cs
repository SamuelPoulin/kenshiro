using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public Transform rightHand;
    public Transform leftHand;
    public Animator animator;

    public Transform RailgunRightHandPoint;
    public Transform RailgunLeftHandPoint;

    public Transform RifleRightHandPoint;
    public Transform RifleLeftHandPoint;

    Transform rightHandPoint;
    Transform leftHandPoint;

    public PlayerController playerController;
    
    Transform Railgun;
    Transform Rifle;

    public float reachDetermination;

    private void Start()
    {
        Railgun = RailgunRightHandPoint.parent;
        Rifle = RifleRightHandPoint.parent;

        reachDetermination = 100;
    }

    private void Update()
    {
        if (playerController.activeWeapon == GameConstants.RAILGUN_GUN_NAME)
        {
            rightHandPoint = RailgunRightHandPoint;
            leftHandPoint = RailgunLeftHandPoint;
        }
        if (playerController.activeWeapon == GameConstants.RIFLE_GUN_NAME)
        {
            rightHandPoint = RifleRightHandPoint;
            leftHandPoint = RifleLeftHandPoint;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPoint.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPoint.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, reachDetermination);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, reachDetermination);
    }
}
