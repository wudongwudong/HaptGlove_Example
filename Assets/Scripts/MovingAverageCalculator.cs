
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.XR.OpenXR.Input;
    using System.Linq;
    
    public class MovingAverageCalculator
    {
        private int windowSize;
        private List<Vector3>[] jointData; // Array of lists to store data for each joint

        public MovingAverageCalculator(int numJoints, int windowSize)
        {
            this.windowSize = windowSize;
            this.jointData = new List<Vector3>[numJoints];

            for (int i = 0; i < numJoints; i++)
            {
                jointData[i] = new List<Vector3>();
            }
        }

        public void AddDataForJoint(int jointIndex, Vector3 value)
        {
            jointData[jointIndex].Add(value);

            if (jointData[jointIndex].Count > windowSize)
            {
                jointData[jointIndex].RemoveAt(0);
            }

            //Debug.Log("joint index: " + jointIndex + ", "+jointData[jointIndex].Count);
        }

        public Vector3 CalculateMovingAverageForJoint(int jointIndex)
        {
            if (jointData[jointIndex].Count == 0)
            {
                return Vector3.zero; // No data points for the joint
            }

            Vector3 sum = Vector3.zero;

            foreach (Vector3 vector in jointData[jointIndex])
            {
                sum += vector;
            }

            Debug.Log("joint index: " + jointIndex + ", " + sum / jointData[jointIndex].Count);
            return sum / jointData[jointIndex].Count;
        }
    }
