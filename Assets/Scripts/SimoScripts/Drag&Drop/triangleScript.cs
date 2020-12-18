using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangleScript : MonoBehaviour
{
    [SerializeField]
    private Transform triangle_s, triangle_f;
    private GameObject triangleObject, gameManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        triangleObject = triangle_s.gameObject;

        gameManager = GameObject.Find("GameManager");
    }

    private void onMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (gameManager.GetComponent<GameManager>().state == GameManager.TextState.TRIANGLE)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else
        {
            gameManager.GetComponent<GameManager>().WrongObject();
            SetCollider(false);
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - triangle_s.position.x) <= 30.0f
            && Mathf.Abs(transform.position.y - triangle_s.position.y) <= 80.0f)
        {
            transform.position = new Vector2(triangle_s.position.x, triangle_s.position.y);

            gameObject.SetActive(false);
            triangleObject.SetActive(false);

            sr = triangle_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            gameManager.GetComponent<GameManager>().WrongPosition();
            SetCollider(false);
        }
    }

    private void SetCollider(bool active)
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = active;
    }
}