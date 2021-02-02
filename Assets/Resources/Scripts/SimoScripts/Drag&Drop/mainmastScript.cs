using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmastScript : MonoBehaviour
{
    [SerializeField]
    private Transform mainmast_s, mainmast_f;
    private GameObject mastObject, leoManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    //Boolean to check if this is the correct object to drag and drop
    private bool isCorrect;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        mastObject = mainmast_s.gameObject;

        leoManager = GameObject.Find("LeoManager");
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

        if (leoManager.GetComponent<LeoManager>().state == TextState.MAST)
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
            if (Mathf.Abs(transform.position.x - mainmast_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - mainmast_s.position.y) <= 80.0f)
            {
                transform.position = new Vector2(mainmast_s.position.x, mainmast_s.position.y);

                gameObject.SetActive(false);
                mastObject.SetActive(false);

                sr = mainmast_f.GetComponent<SpriteRenderer>();
                sr.color = new Color(1f, 1f, 1f, 1f);

                leoManager.GetComponent<LeoManager>().state = TextState.MAST_DONE;
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

