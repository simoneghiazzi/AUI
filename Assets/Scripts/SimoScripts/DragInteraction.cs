using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform silhouette;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private float deltaX, deltaY;

    public static bool locked;
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
        if(Mathf.Abs(transform.position.x - silhouette.position.x) <= 1.0f 
            && Mathf.Abs(transform.position.y - silhouette.position.y) <= 1.0f)
        {
            transform.position = new Vector2(silhouette.position.x, silhouette.position.y);
            locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
