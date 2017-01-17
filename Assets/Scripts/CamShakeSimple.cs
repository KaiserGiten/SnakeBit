using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class CamShakeSimple : MonoBehaviour
{

    Vector3 originalCameraPosition = new Vector3(0, 0, -10);

    private float shakeAmt;

    public Camera mainCamera;

    void OnTriggerEnter2D(Collider2D coll)
    {
        shakeAmt = 0.1f;
        Assert.IsTrue(shakeAmt > 0);
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}