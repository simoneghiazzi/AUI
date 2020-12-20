﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fabricScript : MonoBehaviour
{
    [SerializeField]
    private Transform fabric_s, fabric_f;
    private GameObject fabricObject, gameManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        fabricObject = fabric_s.gameObject;

        gameManager = GameObject.Find("GameManager");
    }

    private void onMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (gameManager.GetComponent<GameManager>().state == TextState.FABRIC)
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
        if (Mathf.Abs(transform.position.x - fabric_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - fabric_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(fabric_s.position.x, fabric_s.position.y);

            gameObject.SetActive(false);
            fabricObject.SetActive(false);

            sr = fabric_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            gameManager.GetComponent<GameManager>().state = TextState.FABRIC_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            gameManager.GetComponent<GameManager>().WrongPosition();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}