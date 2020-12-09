using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamScript : MonoBehaviour
{
    [SerializeField]
    private Transform beam1_s, beam2_s, beam3_s, beam1_f, beam2_f, beam3_f;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr; 

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
        if (Mathf.Abs(transform.position.x - beam1_s.position.x) <= 90.0f
            && Mathf.Abs(transform.position.y - beam1_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam1_s.position.x, beam1_s.position.y);

            sr = beam1_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
            //locked = true;
        }
        else if (Mathf.Abs(transform.position.x - beam2_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - beam2_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam2_s.position.x, beam2_s.position.y);

            sr = beam2_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
            //locked = true;
        }
        else if (Mathf.Abs(transform.position.x - beam3_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - beam3_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam3_s.position.x, beam3_s.position.y);

            sr = beam3_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
            //locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
