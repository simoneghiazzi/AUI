using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmastScript : MonoBehaviour
{
    [SerializeField]
    private Transform mainmast_s, mainmast_f;
    private GameObject mastObject, leoManager;
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
        mastObject = mainmast_s.gameObject;

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
            if (hitWall && hitWall.transform.name == "_mainmast")
            {
                OnMouseDrag();
                foreach (Transform child in components)
                {
                    if (child.name != "_mainmast")
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
            if (hitWall && hitWall.transform.name == "_mainmast")
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

        if (leoManager.GetComponent<LeoManager>().state == TextState.MAST)
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
            if (Mathf.Abs(transform.position.x - mainmast_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - mainmast_s.position.y) <= 80.0f)
            {
                transform.position = new Vector2(mainmast_s.position.x, mainmast_s.position.y);

                gameObject.SetActive(false);
                mastObject.SetActive(false);

                sr = mainmast_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.MAST_DONE;
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

