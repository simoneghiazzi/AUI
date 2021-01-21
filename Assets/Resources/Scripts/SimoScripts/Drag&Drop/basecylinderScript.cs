using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;

public class basecylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform basecylinder_s, basecylinder_f;
    private GameObject basecylinderObject, gameManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    //THE LINES OF CODE COMMENTED IN THIS SCRIPT ARE KEPT AS REFERENCE IN CASE, FOR FUTURE DEVELOPMENT, IT WILL BE MODIFIED TO BE USED WITH GESTURES

    //public GameObject rightHand;

    private Camera mainCamera;

    private SpriteRenderer sr;

    private float deltaX, deltaY;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        basecylinderObject = basecylinder_s.gameObject;
        gameManager = GameObject.Find("GameManager");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //rightHand.GetComponent<TrackerPlayerPosition>().HandState += CheckHandState;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "RightHand")
        {
            Debug.Log("Ciao");
        }
    }
    private void CheckHandState(Dictionary<PartToTrack, bool> handstate)
    {

    }*/

    private void onMouseDown()
    {
        //deltaX = rightHand.transform.position.x - transform.position.x;
        //deltaY = rightHand.transform.position.y - transform.position.y;
        deltaX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = mainCamera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (gameManager.GetComponent<GameManager>().state == TextState.BASE)
        {
            //mousePosition = rightHand.transform.position;
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else
        {
            gameManager.GetComponent<GameManager>().WrongObject();
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - basecylinder_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - basecylinder_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(basecylinder_s.position.x, basecylinder_s.position.y);

            gameObject.SetActive(false);
            basecylinderObject.SetActive(false);

            sr = basecylinder_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            gameManager.GetComponent<GameManager>().state = TextState.BASE_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            gameManager.GetComponent<GameManager>().WrongPosition();
        }
    }
}
