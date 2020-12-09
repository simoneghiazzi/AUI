using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basecylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform basecylinder_s, basecylinder_f;
    private GameObject mastObject;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        mastObject = basecylinder_s.gameObject;
    }

    private void onMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - basecylinder_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - basecylinder_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(basecylinder_s.position.x, basecylinder_s.position.y);

            gameObject.SetActive(false);
            mastObject.SetActive(false);

            sr = basecylinder_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
