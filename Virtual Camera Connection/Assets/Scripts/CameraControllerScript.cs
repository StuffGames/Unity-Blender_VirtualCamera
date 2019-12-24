using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class CameraControllerScript : MonoBehaviour
{
    private GameObject gObject;
    //public Camera mainCam;
    Gyroscope gyro;

    public float xFloat, yFloat, zFloat, wFloat;

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        gObject = new GameObject("Camera Conatiner");
        if (transform.parent != null)
        {
            gObject.transform.SetParent(transform.parent);
        }
        gObject.transform.position = transform.position;
        transform.SetParent(gObject.transform);
        gObject.transform.rotation = Quaternion.Euler(90f,90f,0);
    }

    void Update()
    {
        GyroModifyCamera();
        //Debug.Log(gyro.gravity);
        transform.localRotation = gyro.attitude * new Quaternion(0,0,1,0);
    }

    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}
