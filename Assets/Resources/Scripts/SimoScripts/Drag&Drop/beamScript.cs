﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamScript : MonoBehaviour
{
    [SerializeField]
    private Transform beam1_s, beam2_s, beam3_s, beam1_f, beam2_f, beam3_f;
    private GameObject beamObject1, beamObject2, beamObject3, leoManager;
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
        beamObject1 = beam1_s.gameObject;
        beamObject2= beam2_s.gameObject;
        beamObject3 = beam3_s.gameObject;

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
            if (hitWall && hitWall.transform.name == transform.name)
            {
                OnMouseDrag();
                foreach (Transform child in components)
                {
                    if (child.name != transform.name)
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
            if (hitWall && hitWall.transform.name == transform.name)
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

        if (leoManager.GetComponent<LeoManager>().state == TextState.BEAM)
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
            if (beamObject1.active && Mathf.Abs(transform.position.x - beam1_s.position.x) <= 90.0f
            && Mathf.Abs(transform.position.y - beam1_s.position.y) <= 50.0f)
            {
                transform.position = new Vector2(beam1_s.position.x, beam1_s.position.y);

                gameObject.SetActive(false);
                beamObject1.SetActive(false);

                sr = beam1_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
            }
            else if (beamObject2.active && Mathf.Abs(transform.position.x - beam2_s.position.x) <= 60.0f
                && Mathf.Abs(transform.position.y - beam2_s.position.y) <= 50.0f)
            {
                transform.position = new Vector2(beam2_s.position.x, beam2_s.position.y);

                gameObject.SetActive(false);
                beamObject2.SetActive(false);

                sr = beam2_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
            }
            else if (beamObject3.active && Mathf.Abs(transform.position.x - beam3_s.position.x) <= 60.0f
                && Mathf.Abs(transform.position.y - beam3_s.position.y) <= 50.0f)
            {
                transform.position = new Vector2(beam3_s.position.x, beam3_s.position.y);

                gameObject.SetActive(false);
                beamObject3.SetActive(false);

                sr = beam3_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
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
