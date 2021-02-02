using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;

public class basecylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform basecylinder_s, basecylinder_f;
    private GameObject basecylinderObject, leoManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    //THE LINES OF CODE COMMENTED IN THIS SCRIPT ARE KEPT AS REFERENCE IN CASE, FOR FUTURE DEVELOPMENT, IT WILL BE MODIFIED TO BE USED WITH GESTURES

    //public GameObject rightHand;

    private Camera wallCamera;

    private Transform components;

    private SpriteRenderer sr;

    private float deltaX, deltaY;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        basecylinderObject = basecylinder_s.gameObject;
        leoManager = GameObject.Find("LeoManager");
        wallCamera = GameObject.Find("WallCamera").GetComponent<Camera>();
        components = GameObject.Find("Components").transform;
        //rightHand.GetComponent<TrackerPlayerPosition>().HandState += CheckHandState;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPosWall = new Vector2(wallCamera.ScreenToWorldPoint(Input.mousePosition).x, wallCamera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hitWall = Physics2D.Raycast(rayPosWall, Vector2.zero, 0f);
            if (hitWall)
            {
                OnMouseDown();
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 rayPosWall = new Vector2(wallCamera.ScreenToWorldPoint(Input.mousePosition).x, wallCamera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hitWall = Physics2D.Raycast(rayPosWall, Vector2.zero, 0f);
            if (hitWall && hitWall.transform.name == "_basecylinder") 
            {
                OnMouseDrag();
                foreach (Transform child in components)
                {
                    if(child.name != "_basecylinder")
                    {
                        child.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    }
                }
            }

        }
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 rayPosWall = new Vector2(wallCamera.ScreenToWorldPoint(Input.mousePosition).x, wallCamera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hitWall = Physics2D.Raycast(rayPosWall, Vector2.zero, 0f);
            if (hitWall && hitWall.transform.name == "_basecylinder")
            {
                OnMouseUp();
                foreach (Transform child in components)
                {
                    child.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
        }
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

    private void OnMouseDown()
    {
        //deltaX = rightHand.transform.position.x - transform.position.x;
        //deltaY = rightHand.transform.position.y - transform.position.y;
        deltaX = wallCamera.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = wallCamera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (leoManager.GetComponent<LeoManager>().state == TextState.BASE)
        {
            //mousePosition = rightHand.transform.position;
            mousePosition = wallCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else
        {
            leoManager.GetComponent<LeoManager>().WrongObject();
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

            leoManager.GetComponent<LeoManager>().state = TextState.BASE_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            leoManager.GetComponent<LeoManager>().WrongPosition();
        }
    }
}
