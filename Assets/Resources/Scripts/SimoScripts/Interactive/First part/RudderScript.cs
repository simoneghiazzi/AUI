#define ENABLE_UPDATE_FUNCTION_CALLBACK
#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RudderScript : MonoBehaviour
{
    public Graphic UI_Element;

    RectTransform rectT;
    Vector2 centerPoint;

    public float maximumSteeringAngle = 200f;
    public float wheelReleasedSpeed = 200f;
    
    //The grater the slower the boat rotates
    public float wheelInfluenceOnBoat = 500f;

    //The greater the slower the boat translates
    public float waterResistance = 1.5f;

    public float wheelAngle = 0f;
    float wheelPrevAngle = 0f;
    public bool wheelBeingHeld = false;

    void Start()
    {
        rectT = UI_Element.rectTransform;
        InitEventsSystem();
    }

    void Update()
    {
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

        // Rotate and translate the boat according to the wheel rotation
        transform.Rotate(Vector3.back * wheelAngle / wheelInfluenceOnBoat);
        transform.Translate(-transform.rotation.z / waterResistance , 0, 0, Space.World);

        //Having the boat face downward would be irealistic. Notice that eulerAngles are between 0 and 360, but we can 
        //make a rotation with negative angles like in the unity inspector using transform.rotation
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 180)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if(transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z < 270)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    //After a collision with the piers, the CollisionScript calls this method 
    public void CollisionReset()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Awake()
    {
        UnityThread.initUnityThread();
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
        functionCall = new UnityAction<BaseEventData>(ReleaseEvent);
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
        wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
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

//DON'T CARE ABOUT THE FOLLOWING CLASS. BORING STUFF ABOUT THREADING
/// <summary>
///Class used for handle the thread for randomly picking a direction inside the thread of the Timer.Elapsed event
/// </summary>
public class UnityThread : MonoBehaviour
{
    //our (singleton) instance
    private static UnityThread instance = null;


    ////////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////////
    //Holds actions received from another Thread. Will be coped to actionCopiedQueueUpdateFunc then executed from there
    private static List<System.Action> actionQueuesUpdateFunc = new List<Action>();

    //holds Actions copied from actionQueuesUpdateFunc to be executed
    List<System.Action> actionCopiedQueueUpdateFunc = new List<System.Action>();

    // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
    private volatile static bool noActionQueueToExecuteUpdateFunc = true;


    ////////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////////
    //Holds actions received from another Thread. Will be coped to actionCopiedQueueLateUpdateFunc then executed from there
    private static List<System.Action> actionQueuesLateUpdateFunc = new List<Action>();

    //holds Actions copied from actionQueuesLateUpdateFunc to be executed
    List<System.Action> actionCopiedQueueLateUpdateFunc = new List<System.Action>();

    // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
    private volatile static bool noActionQueueToExecuteLateUpdateFunc = true;



    ////////////////////////////////////////////////FIXEDUPDATE IMPL////////////////////////////////////////////////////////
    //Holds actions received from another Thread. Will be coped to actionCopiedQueueFixedUpdateFunc then executed from there
    private static List<System.Action> actionQueuesFixedUpdateFunc = new List<Action>();

    //holds Actions copied from actionQueuesFixedUpdateFunc to be executed
    List<System.Action> actionCopiedQueueFixedUpdateFunc = new List<System.Action>();

    // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
    private volatile static bool noActionQueueToExecuteFixedUpdateFunc = true;


    //Used to initialize UnityThread. Call once before any function here
    public static void initUnityThread(bool visible = false)
    {
        if (instance != null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            // add an invisible game object to the scene
            GameObject obj = new GameObject("MainThreadExecuter");
            if (!visible)
            {
                obj.hideFlags = HideFlags.HideAndDontSave;
            }

            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<UnityThread>();
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //////////////////////////////////////////////COROUTINE IMPL//////////////////////////////////////////////////////
#if (ENABLE_UPDATE_FUNCTION_CALLBACK)
    public static void executeCoroutine(IEnumerator action)
    {
        if (instance != null)
        {
            executeInUpdate(() => instance.StartCoroutine(action));
        }
    }

    ////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////
    public static void executeInUpdate(System.Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action");
        }

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.Add(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    public void Update()
    {
        if (noActionQueueToExecuteUpdateFunc)
        {
            return;
        }

        //Clear the old actions from the actionCopiedQueueUpdateFunc queue
        actionCopiedQueueUpdateFunc.Clear();
        lock (actionQueuesUpdateFunc)
        {
            //Copy actionQueuesUpdateFunc to the actionCopiedQueueUpdateFunc variable
            actionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc);
            //Now clear the actionQueuesUpdateFunc since we've done copying it
            actionQueuesUpdateFunc.Clear();
            noActionQueueToExecuteUpdateFunc = true;
        }

        // Loop and execute the functions from the actionCopiedQueueUpdateFunc
        for (int i = 0; i < actionCopiedQueueUpdateFunc.Count; i++)
        {
            actionCopiedQueueUpdateFunc[i].Invoke();
        }
    }
#endif

    ////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////
#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)
    public static void executeInLateUpdate(System.Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action");
        }

        lock (actionQueuesLateUpdateFunc)
        {
            actionQueuesLateUpdateFunc.Add(action);
            noActionQueueToExecuteLateUpdateFunc = false;
        }
    }


    public void LateUpdate()
    {
        if (noActionQueueToExecuteLateUpdateFunc)
        {
            return;
        }

        //Clear the old actions from the actionCopiedQueueLateUpdateFunc queue
        actionCopiedQueueLateUpdateFunc.Clear();
        lock (actionQueuesLateUpdateFunc)
        {
            //Copy actionQueuesLateUpdateFunc to the actionCopiedQueueLateUpdateFunc variable
            actionCopiedQueueLateUpdateFunc.AddRange(actionQueuesLateUpdateFunc);
            //Now clear the actionQueuesLateUpdateFunc since we've done copying it
            actionQueuesLateUpdateFunc.Clear();
            noActionQueueToExecuteLateUpdateFunc = true;
        }

        // Loop and execute the functions from the actionCopiedQueueLateUpdateFunc
        for (int i = 0; i < actionCopiedQueueLateUpdateFunc.Count; i++)
        {
            actionCopiedQueueLateUpdateFunc[i].Invoke();
        }
    }
#endif

    ////////////////////////////////////////////FIXEDUPDATE IMPL//////////////////////////////////////////////////
#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)
    public static void executeInFixedUpdate(System.Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action");
        }

        lock (actionQueuesFixedUpdateFunc)
        {
            actionQueuesFixedUpdateFunc.Add(action);
            noActionQueueToExecuteFixedUpdateFunc = false;
        }
    }

    public void FixedUpdate()
    {
        if (noActionQueueToExecuteFixedUpdateFunc)
        {
            return;
        }

        //Clear the old actions from the actionCopiedQueueFixedUpdateFunc queue
        actionCopiedQueueFixedUpdateFunc.Clear();
        lock (actionQueuesFixedUpdateFunc)
        {
            //Copy actionQueuesFixedUpdateFunc to the actionCopiedQueueFixedUpdateFunc variable
            actionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc);
            //Now clear the actionQueuesFixedUpdateFunc since we've done copying it
            actionQueuesFixedUpdateFunc.Clear();
            noActionQueueToExecuteFixedUpdateFunc = true;
        }

        // Loop and execute the functions from the actionCopiedQueueFixedUpdateFunc
        for (int i = 0; i < actionCopiedQueueFixedUpdateFunc.Count; i++)
        {
            actionCopiedQueueFixedUpdateFunc[i].Invoke();
        }
    }
#endif

    public void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}