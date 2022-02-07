using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Camera mainCamera;
    public Localization playerLocation;


    private float elapsedTime;
    public float transitionDuration = 10f;
    public AnimationCurve curve;

    private void Awake()
    {
        Localization.OnRoomChanged += resetTimer;
    }

    private void resetTimer()
    {
        elapsedTime = 0;
    }

    private void Update()
    {
        if (elapsedTime<=transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / transitionDuration;
            //Debug.Log(string.Format("{0}/{1} = {2}", elapsedTime, transitionDuration, percentage));
            Vector3 destination = playerLocation.room.gameObject.transform.GetChild(0).position + new Vector3(0, 0, -80);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                destination,
                curve.Evaluate(percentage));
        }

        
    }

    private void OnDestroy()
    {
        Localization.OnRoomChanged -= resetTimer;
    }


}
