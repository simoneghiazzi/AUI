using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMe : MonoBehaviour
{

    public GameObject tofollow;
    /// <summary>
    /// reference to the camera for the floor screen
    /// </summary>
    public Camera floorcamera;
    /// <summary>
    /// reference to the canvas on which the gameobject is placed on
    /// </summary>
    public GameObject Canvas;
    /// <summary>
    /// transform of the canvas
    /// </summary>
    RectTransform CanvasRect;
    // Use this for initialization
    void Start()
    {
        CanvasRect = Canvas.GetComponent<RectTransform>();
    }

    /// <summary>
    /// percentage of completement of the loading bar
    /// </summary>
    float amount = 0;

    // Update is called once per frame
    void Update()
    {
        Vector2 ViewportPosition = floorcamera.WorldToViewportPoint(tofollow.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
        gameObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition + Vector2.up * 50;
        if (amount >= 0.99f || amount <= 0)
        {
            gameObject.GetComponent<Image>().fillAmount = 0;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Image>().fillAmount = amount;
        }
    }
    /// <summary>
    /// set the percentage of fillup
    /// </summary>
    /// <param name="percentage"></param>
    public void insidethecard(float percentage)
    {
        amount = percentage;
    }

    /// <summary>
    /// empty the loading bar
    /// </summary>
    public void exitthecard()
    {
        amount = 0;
    }
}
