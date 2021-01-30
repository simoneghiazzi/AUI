using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallCylinderScript : MonoBehaviour
{
    [SerializeField]
    private Transform smallcylinder_s, smallcylinder_f;
    private GameObject smalcylinderObject, leoManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        smalcylinderObject = smallcylinder_s.gameObject;

        leoManager = GameObject.Find("LeoManager");
    }

    private void onMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (leoManager.GetComponent<LeoManager>().state == TextState.SMALL)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else
        {
            leoManager.GetComponent<LeoManager>().WrongObject();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - smallcylinder_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - smallcylinder_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(smallcylinder_s.position.x, smallcylinder_s.position.y);

            gameObject.SetActive(false);
            smalcylinderObject.SetActive(false);

            sr = smallcylinder_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            leoManager.GetComponent<LeoManager>().state = TextState.SMALL_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            leoManager.GetComponent<LeoManager>().WrongPosition();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}