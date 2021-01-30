using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamScript : MonoBehaviour
{
    [SerializeField]
    private Transform beam1_s, beam2_s, beam3_s, beam1_f, beam2_f, beam3_f;
    private GameObject beamObject1, beamObject2, beamObject3, leoManager;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private SpriteRenderer sr;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        beamObject1 = beam1_s.gameObject;
        beamObject2= beam2_s.gameObject;
        beamObject3 = beam3_s.gameObject;

        leoManager = GameObject.Find("LeoManager");
    }

    private void onMouseDown()
    {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (leoManager.GetComponent<LeoManager>().state == TextState.BEAM)
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
        if (beamObject1.active && Mathf.Abs(transform.position.x - beam1_s.position.x) <= 90.0f
            && Mathf.Abs(transform.position.y - beam1_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam1_s.position.x, beam1_s.position.y);

            gameObject.SetActive(false);
            beamObject1.SetActive(false);

            sr = beam1_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
        }
        else if (beamObject2.active && Mathf.Abs(transform.position.x - beam2_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - beam2_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam2_s.position.x, beam2_s.position.y);

            gameObject.SetActive(false);
            beamObject2.SetActive(false);

            sr = beam2_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
        }
        else if (beamObject3.active && Mathf.Abs(transform.position.x - beam3_s.position.x) <= 60.0f
            && Mathf.Abs(transform.position.y - beam3_s.position.y) <= 50.0f)
        {
            transform.position = new Vector2(beam3_s.position.x, beam3_s.position.y);

            gameObject.SetActive(false);
            beamObject3.SetActive(false);

            sr = beam3_f.GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, 1f);

            leoManager.GetComponent<LeoManager>().state = TextState.BEAM_DONE;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
            leoManager.GetComponent<LeoManager>().WrongPosition();
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
