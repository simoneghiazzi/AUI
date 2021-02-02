using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform bigcylinder_s, bigcylinder_f;
    private GameObject bigcylinderObject, leoManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private Camera wallCamera;

    private Transform components;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    //Boolean to check if this is the correct object to drag and drop
    private bool isCorrect;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        bigcylinderObject = bigcylinder_s.gameObject;
        leoManager = GameObject.Find("LeoManager");
        wallCamera = GameObject.Find("WallCamera").GetComponent<Camera>();
        components = GameObject.Find("Components").transform;
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
            if (hitWall && hitWall.transform.name == "_big cylinder")
            {
                OnMouseDrag();
                foreach (Transform child in components)
                {
                    if (child.name != "_big cylinder")
                    {
                        child.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 rayPosWall = new Vector2(wallCamera.ScreenToWorldPoint(Input.mousePosition).x, wallCamera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hitWall = Physics2D.Raycast(rayPosWall, Vector2.zero, 0f);
            if (hitWall && hitWall.transform.name == "_big cylinder")
            {
                OnMouseUp();
                foreach (Transform child in components)
                {
                    child.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
        }
    }


    private void OnMouseDown()
    {
        deltaX = wallCamera.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = wallCamera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        mousePosition = wallCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);

        if (leoManager.GetComponent<LeoManager>().state == TextState.BIG)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }

    private void OnMouseUp()
    {
        if (isCorrect)
        {
            if (Mathf.Abs(transform.position.x - bigcylinder_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - bigcylinder_s.position.y) <= 80.0f)
            {
                transform.position = new Vector2(bigcylinder_s.position.x, bigcylinder_s.position.y);

                gameObject.SetActive(false);
                bigcylinderObject.SetActive(false);

                sr = bigcylinder_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.BIG_DONE;
            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
                leoManager.GetComponent<LeoManager>().WrongPosition();
                //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            leoManager.GetComponent<LeoManager>().WrongObject();
        }
    }
}
