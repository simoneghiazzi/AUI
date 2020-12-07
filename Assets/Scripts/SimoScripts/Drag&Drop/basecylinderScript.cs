﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basecylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform basecylinder_s;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private float deltaX, deltaY;

    public bool locked;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onMouseDown()
    {
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - basecylinder_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - basecylinder_s.position.y) <= 30.0f)
        {
            transform.position = new Vector2(basecylinder_s.position.x, basecylinder_s.position.y);
            locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}