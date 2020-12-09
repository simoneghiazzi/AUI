using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handlebarScript : MonoBehaviour
{
    [SerializeField]
    private Transform handlebar_s, handlebar_f;
    private GameObject handlebarObject;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        handlebarObject = handlebar_s.gameObject;
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
        if (Mathf.Abs(transform.position.x - handlebar_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - handlebar_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(handlebar_s.position.x, handlebar_s.position.y);

            gameObject.SetActive(false);
            handlebarObject.SetActive(false);

            sr = handlebar_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}