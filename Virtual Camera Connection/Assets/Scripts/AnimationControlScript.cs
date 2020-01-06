using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;
using UnityEngine.UI;

public class AnimationControlScript : MonoBehaviour
{
    private GameObjectRecorder objRec; //ObjectRecorder to do the auto keyframing
    public Animator animControl; //Animation controller of separate animation
    public AnimationClip anim; //The animation file to record on
    public Text placementText; //Text used to display time
    public AnimationCurve animCurve;

    public int frameStop; //Frame to stop the recording
    private bool timeToPlay = false; //Used to control when to start and stop recording
    private string path = @"C:\Users\juans\AppData\Roaming\Blender Foundation\Blender\2.81\scripts\addons\VC_Connection Blender\QuaternionKeyFrameValues.txt";//"C:\Program Files (x86)\Steam\steamapps\common\Blender\QuaternionKeyFrameValues.txt"; //Directory to text file
    public List<Quaternion> keyFrames = new List<Quaternion>(); //Holds overall Quaternion rotation values for each frame
    public List<AnimationCurve> curves = new List<AnimationCurve>(); //Holds the animation curves for each value of the Quaternion rotation values
    public List<Vector2> xKeys = new List<Vector2>(); //Holds the frame and value of the x value of the Quaternion rotation
    public List<Vector2> yKeys = new List<Vector2>(); //" " y value of Quaternion
    public List<Vector2> zKeys = new List<Vector2>(); //" " z value of Quaternion
    public List<Vector2> wKeys = new List<Vector2>(); //" " w value of Quaternion

    //Initiates some references and methods at the start of being played
    void Start()
    {
        objRec = new GameObjectRecorder(gameObject);
        objRec.BindComponentsOfType<Transform>(gameObject, false);
        print(transform.rotation);
        print(transform.localRotation);
    }

    bool startedRecording = false; //To check if the recording started
    bool savedRecording = false; //To check if the recording saved
    
    //LateUpdate used to run code at the end of a frame update so less desync occurs
    void LateUpdate()
    {
        int currentFrame = Mathf.FloorToInt(objRec.currentTime * 24.0f);
        //print(objRec.isRecording);
        placementText.text = "Frame: " + Mathf.Floor(objRec.currentTime * 24.0f).ToString();
        if (timeToPlay)
        {
            objRec.TakeSnapshot(Time.deltaTime);
            startedRecording = true;
        }

        else if(!timeToPlay && startedRecording)
        {
            if (!savedRecording)
            {
                EditorCurveBinding[] someList;
                someList = objRec.GetBindings();
                objRec.SaveToClip(anim, 24.0f);
                objRec.ResetRecording();
                savedRecording = true;
                print(transform.rotation);
                print(transform.localRotation);
                for (int i = 0; i < someList.Length; i++)
                {
                    curves.Add(AnimationUtility.GetEditorCurve(anim, someList[i]));
                }

                for (int i = 0; i < 4; i++)
                {
                    for(int k = 0; k < curves[i].keys.Length; k++)
                    {
                        float t = curves[i].keys[k].time;
                        float v = curves[i].keys[k].value;
                        if (i == 0)
                        {
                            xKeys.Add(new Vector2(t, v));
                        }
                        else if(i == 1)
                        {
                            yKeys.Add(new Vector2(t, v));
                        }
                        else if(i == 2)
                        {
                            zKeys.Add(new Vector2(t, v));
                        }
                        else if (i == 3)
                        {
                            wKeys.Add(new Vector2(t, v));
                        }
                    }
                }
                //Write on Text File Stuff
                if (File.Exists(path))
                {
                    for (int i = 0; i < xKeys.Count; i++)
                    {
                        string frameInfo;
                        Quaternion frameQuaternion = new Quaternion(xKeys[i].y, yKeys[i].y, zKeys[i].y, wKeys[i].y);
                        keyFrames.Add(frameQuaternion);
                        frameInfo = Mathf.FloorToInt(xKeys[i].x * 24.0f) + "," + wKeys[i].y + "," + xKeys[i].y + "," + yKeys[i].y + "," + zKeys[i].y + "\n";
                        //print(frameInfo);
                        File.AppendAllText(path, frameInfo);
                    }
                    Debug.Log("Done");
                }
            }
            else
            {
                return;
            }
        }

        if (frameStop > 0)
        {
            if (frameStop == Mathf.Floor(objRec.currentTime * 24.0f))
            {
                timeToPlay = false;
            }
        }
    }

    //Empty Method to be considered or use later
    public void ResetAnimation()
    {
        //objRec.ResetRecording();
    }

    //Public Method used to get input from the Record UI button
    public void Record()
    {
        if (timeToPlay)
        {
            timeToPlay = false;
            //startedRecording = false;
        }
        else
        {
            timeToPlay = true;
        }
    }

    bool thing = false; //Just used as a switch from on and off for below method

    //Public Method used by the UI Play button to play and stop the separate animation
    public void PlayAnimation()
    {

        //print(animControl.GetBool("buttonPlay"));

        if (thing == true)
        {
            thing = false;
        }
        else
        {
            thing = true;
        }

        animControl.SetBool("buttonPlay", thing);
    }
}
