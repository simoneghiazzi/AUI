﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public Graphic UI_Element;

    RectTransform rectT;
    Vector2 centerPoint;

    public float maximumSteeringAngle = 200f;
    public float wheelReleasedSpeed = 200f;

    public float wheelAngle = 0f;
    float wheelPrevAngle = 0f;
    public bool wheelBeingHeld = false;

    //Boolean used to set rotation direction
    public bool toOpen = true;

    private Transform leftDoor, rightDoor;

    // Start is called before the first frame update
    void Start()
    {
        rectT = UI_Element.rectTransform;
        InitEventsSystem();

        leftDoor = gameObject.transform.GetChild(0);
        rightDoor = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(toOpen && wheelAngle < 0)
        {
            wheelAngle = 0;
        }
        else if (!toOpen && wheelAngle >0)
        {
            wheelAngle = 0;
        }
        // If the wheel is released, reset the rotation
        // to initial (zero) rotation by wheelReleasedSpeed degrees per second
        if (!wheelBeingHeld && !Mathf.Approximately(0f, wheelAngle))
        {
            float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= deltaAngle;
            else
                wheelAngle += deltaAngle;
        }

        // Rotate the wheel image. Vector3.back is a shorthand for writing Vector3(0, 0, -1).
        rectT.localEulerAngles = Vector3.back * wheelAngle;

        if ((((int)wheelAngle % 15) > 10 || ((int)wheelAngle % 30) < 5) && wheelAngle!=0)
        {
            if (toOpen)
            {
                leftDoor.Rotate(Vector3.forward * 0.5f);
                rightDoor.Rotate(Vector3.back * 0.5f);
            }
            else
            {
                leftDoor.Rotate(Vector3.back * 0.5f);
                rightDoor.Rotate(Vector3.forward * 0.5f);
            }
        }
    }

    public void closeDoors()
    {
        leftDoor.eulerAngles = new Vector3(
            leftDoor.eulerAngles.x,
            0,
            leftDoor.eulerAngles.z
        );
        rightDoor.eulerAngles = new Vector3(
            rightDoor.eulerAngles.x,
            0,
            rightDoor.eulerAngles.z
        );
    }

    void InitEventsSystem()
    {
        EventTrigger events = UI_Element.gameObject.GetComponent<EventTrigger>();

        if (events == null)
            events = UI_Element.gameObject.AddComponent<EventTrigger>();

        if (events.triggers == null)
            events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData>(PressEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(DragEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.Drag;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(ReleaseEvent);//
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = callback;

        events.triggers.Add(entry);
    }

    public void PressEvent(BaseEventData eventData)
    {
        // Executed when mouse/finger starts touching the steering wheel
        Vector2 pointerPos = ((PointerEventData)eventData).position;

        wheelBeingHeld = true;
        centerPoint = RectTransformUtility.WorldToScreenPoint(((PointerEventData)eventData).pressEventCamera, rectT.position);
        wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
    }

    public void DragEvent(BaseEventData eventData)
    {
        // Executed when mouse/finger is dragged over the steering wheel
        Vector2 pointerPos = ((PointerEventData)eventData).position;

        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        // Do nothing if the pointer is too close to the center of the wheel
        if (Vector2.Distance(pointerPos, centerPoint) > 20f)
        {
            if (pointerPos.x > centerPoint.x)
                wheelAngle += wheelNewAngle - wheelPrevAngle;
            else
                wheelAngle -= wheelNewAngle - wheelPrevAngle;
        }
        // Make sure wheel angle never exceeds maximumSteeringAngle
        //wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
        wheelPrevAngle = wheelNewAngle;
    }

    public void ReleaseEvent(BaseEventData eventData)
    {
        // Executed when mouse/finger stops touching the steering wheel
        // Performs one last DragEvent, just in case
        DragEvent(eventData);

        wheelBeingHeld = false;
    }

    public float GetClampedValue()
    {
        // returns a value in range [-1,1] similar to GetAxis("Horizontal")
        return wheelAngle / maximumSteeringAngle;
    }

    public float GetAngle()
    {
        // returns the wheel angle itself without clamp operation
        return wheelAngle;
    }
}
