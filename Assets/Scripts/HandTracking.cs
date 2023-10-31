using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;


public class HandTracking : MonoBehaviour
{
    public GameObject tracker;
    //public GameObject palmPrefeb;

    GameObject wristObjectLeft, wristObjectRight;

    MixedRealityPose pose;

   
    void Awake()
    {
        wristObjectLeft = Instantiate(tracker, this.transform);
        wristObjectLeft.name = "ViveTracker_Left";

        wristObjectRight = Instantiate(tracker, this.transform);
        wristObjectRight.name = "ViveTracker_Right";
    }

    // Update is called once per frame
    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out pose))
        {
            wristObjectLeft.transform.position = pose.Position;
            wristObjectLeft.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Right, out pose))
        {
            wristObjectRight.transform.position = pose.Position;
            wristObjectRight.transform.rotation = pose.Rotation;
        }
    }
}
