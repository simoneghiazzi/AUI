using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{
    enum TextState { INTRO, BASE, BIG, SMALL, BEAM, TRIANGLE, FABRIC, HANDLE };
    private TextState state;

    private Text txt;

    private Timer timer = new Timer();

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate; 
    // Start is called before the first frame update
    void Start()
    {
        state = TextState.INTRO;
        txt = GameObject.Find("TextLeo").GetComponent<Text>();
        timer.Interval = 5000f;
        timer.Elapsed += NextIntro;
        toUpdate = txt.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.Enabled)
        {
            switch (state)
            {
                case TextState.INTRO:
                    timer.Start();
                    break;
                case TextState.BASE:
                    break;
                case TextState.BIG:
                    break;
                case TextState.SMALL:
                    break;
                case TextState.BEAM:
                    break;
                case TextState.TRIANGLE:
                    break;
                case TextState.FABRIC:
                    break;
                case TextState.HANDLE:
                    break;
                default:
                    break;
            }
        }
        //For the bug explained at the beginning
        txt.text = toUpdate;
    }

    void NextIntro(object o, System.EventArgs e)
    {
        timer.Stop();
        toUpdate = "Per afferrare un oggetto chiudi la mano a pugno davanti a quello. " +
            "Tenendo il pugno chiuso trascinalo dove vuoi metterlo e riapri la mano per lasciarlo andare";
        state = TextState.BASE;
    }
}
