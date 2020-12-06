using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangleScript : MonoBehaviour
{
    [SerializeField]
    private Transform traingle_s;
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
        if (Mathf.Abs(transform.position.x - traingle_s.position.x) <= 40.0f
            && Mathf.Abs(transform.position.y - traingle_s.position.y) <= 40.0f)
        {
            transform.position = new Vector2(traingle_s.position.x, traingle_s.position.y);
            locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
