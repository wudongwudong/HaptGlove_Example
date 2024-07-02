using System;
using System.Collections;
using System.Collections.Generic;
using HaptGlove;
using TMPro;
using UnityEngine;

public class HaptGloveUI : MonoBehaviour
{
    //public GameObject toggleLeft, toggleRight, toggleHandMesh;
    //public GameObject buttonBLE, buttonPump, buttonExhause, buttonDropObject;

    public GameObject handLeft, handRight, sceneObject;

    public List<string> controlledHandsList = new List<string>();

    public TMP_Text log;

    void Update()
    {
        if (log != null)
        {
            log.text = handRight.GetComponent<HaptGloveHandler>().btText;
        }
        else
        {
            Debug.Log("HaptGloveUI: No TMP object is assigned to log");
        }
        
    }

    public void UI_Actions(string name)
    {
        switch (name)
        {
            case "ToggleLeft":
                if (controlledHandsList.Contains("Left"))
                    controlledHandsList.Remove("Left");
                else
                    controlledHandsList.Add("Left");
                break;
            case "ToggleRight":
                if (controlledHandsList.Contains("Right"))
                    controlledHandsList.Remove("Right");
                else
                    controlledHandsList.Add("Right");
                break;
            case "ButtonBLE":
                BTButtonOnClick();
                break;
            case "ButtonPump":
                PSButtonOnClick();
                break;
            case "ButtonExhaust":
                ExButtonOnClick();
                break;
            case "ButtonDropObject":
                DropObjectButtonOnClick();
                break;
            case "ButtonHandMesh":
                HandMeshButtonOnClick();
                break;
            case "ButtonResetScene":
                ResetSceneButtonOnClick();
                break;
        }
    }

    private void BTButtonOnClick()
    {
        foreach (var hand in controlledHandsList)
        {
            switch (hand)
            {
                case "Left":
                    handLeft.GetComponent<HaptGloveHandler>().BTConnection();
                    break;
                case "Right":
                    handRight.GetComponent<HaptGloveHandler>().BTConnection();
                    break;
            }
        }
    }

    private void PSButtonOnClick()
    {
        foreach (var hand in controlledHandsList)
        {
            switch (hand)
            {
                case "Left":
                    handLeft.GetComponent<HaptGloveHandler>().AirPressureSourceControl();
                    break;
                case "Right":
                    handRight.GetComponent<HaptGloveHandler>().AirPressureSourceControl();
                    break;
            }
        }
    }


    private void ExButtonOnClick()
    {
        byte[][] clutchStates =
            {new byte[] {0, 2}, new byte[] {1, 2}, new byte[] {2, 2}, new byte[] {3, 2}, new byte[] {4, 2}};
        byte[] btData;

        foreach (var hand in controlledHandsList)
        {
            switch (hand)
            {
                case "Left":
                    //Haptics.ApplyHaptics(clutchStates, 60, hand, false);
                    btData = handLeft.GetComponent<HaptGloveHandler>().haptics.ApplyHaptics(clutchStates, 60, false);
                    handLeft.GetComponent<HaptGloveHandler>().BTSend(btData);
                    break;
                case "Right":
                    //Haptics.ApplyHaptics(clutchStates, 60, hand, false);
                    btData = handRight.GetComponent<HaptGloveHandler>().haptics.ApplyHaptics(clutchStates, 60, false);
                    handRight.GetComponent<HaptGloveHandler>().BTSend(btData);
                    break;
            }
        }
    }

    private void DropObjectButtonOnClick()
    {
        foreach (var hand in controlledHandsList)
        {
            switch (hand)
            {
                case "Left":
                    handLeft.GetComponent<Grasping>().DropObject();
                    break;
                case "Right":
                    handRight.GetComponent<Grasping>().DropObject();

                    //List<byte> buf = new List<byte>();
                    //for (byte i = 255; i > 0; i--)
                    //{
                    //    buf.Add(i);
                    //}

                    //handRight.GetComponent<HaptGloveHandler>().BTSend(buf.ToArray());
                    break;
            }
        }
    }

    private void HandMeshButtonOnClick()
    {
        GameObject leftHandMesh = handLeft.transform.Find("LeftHand").gameObject;
        GameObject rightHandMesh = handRight.transform.Find("RightHand").gameObject;

        if ((leftHandMesh != null)&(rightHandMesh != null))
        {
            if (leftHandMesh.activeInHierarchy)
            {
                leftHandMesh.SetActive(false);
                rightHandMesh.SetActive(false);
            }
            else
            {
                leftHandMesh.SetActive(true);
                rightHandMesh.SetActive(true);
            }
        }

    }

    private void ResetSceneButtonOnClick()
    {
        GameObject previousObject = GameObject.Find(sceneObject.name);
        if (previousObject!= null)
        {
            Destroy(previousObject);
        }
        GameObject currentObject = Instantiate(sceneObject);
        currentObject.name = "DemoProps";
        currentObject.transform.position = new Vector3(0, 0, 0.6f);
        currentObject.transform.rotation = Quaternion.identity;
    }

}
