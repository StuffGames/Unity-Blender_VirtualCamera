using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UI;

public class AnimationControlScript : MonoBehaviour
{
    private GameObjectRecorder objRec;
    public Animator animControl;
    public AnimationClip anim;
    public Text placementText;

    private bool timeToPlay = false;

    void Start()
    {
        //anim.frameRate = 24;
        objRec = new GameObjectRecorder(gameObject);
        objRec.BindComponentsOfType<Transform>(gameObject, false);
    }
    bool startedRecording = false;
    bool savedRecording = false;
    void LateUpdate()
    {
        print(objRec.isRecording);
        placementText.text = objRec.currentTime.ToString();
        if (timeToPlay)
        {
            objRec.TakeSnapshot(Time.deltaTime);
            startedRecording = true;
        }
        else if(!timeToPlay && startedRecording)
        {
            if (!savedRecording)
            {
                objRec.SaveToClip(anim, 24.0f);
                objRec.ResetRecording();
                savedRecording = true;
            }
            else
            {
                return;
            }
        }
    }

    public void ResetAnimation()
    {
        //objRec.ResetRecording();
    }

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

    bool thing = false;

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
