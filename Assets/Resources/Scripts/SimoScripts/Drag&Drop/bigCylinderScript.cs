﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform bigcylinder_s, bigcylinder_f;
    private GameObject bigcylinderObject, gameManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        bigcylinderObject = bigcylinder_s.gameObject;
        gameManager = GameObject.Find("GameManager");
    }

    private void onMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (gameManager.GetComponent<GameManager>().state == TextState.BIG)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else
        {
            gameManager.GetComponent<GameManager>().WrongObject();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - bigcylinder_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - bigcylinder_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(bigcylinder_s.position.x, bigcylinder_s.position.y);

            gameObject.SetActive(false);
            bigcylinderObject.SetActive(false);

            sr = bigcylinder_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            gameManager.GetComponent<GameManager>().state = TextState.BIG_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            gameManager.GetComponent<GameManager>().WrongPosition();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}