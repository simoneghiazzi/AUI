using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //enum TextState { INTRO, BASE, BIG, SMALL, BEAM, TRIANGLE, FABRIC, HANDLE };
    public enum TextState { INTRO, BASE, BASE_DONE, BIG, BIG_DONE, SMALL, SMALL_DONE, BEAM, BEAM_DONE, TRIANGLE,
        TRIANGLE_DONE, FABRIC,FABRIC_DONE, HANDLE, HANDLE_DONE, WRONG_OBJ, WRONG_POS };

    //saveState needed to retrieve the current state after an error message
    public TextState state, savedState;

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
                    toUpdate = "Innazitutto avrei bisogno di ";
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
                case TextState.WRONG_POS:
                    timer.Elapsed += PositionError;
                    toUpdate = "È un'idea carina ma non penso potrebbe funzionare. Prova a riposizionarlo!";
                    timer.Start();
                    Debug.Log("funziona plis");
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
        timer.Elapsed += BeginGame;
    }

    void BeginGame(object o, System.EventArgs e)
    {
        timer.Stop();
        state = TextState.BASE;
    }

    void PositionError(object o, System.EventArgs e)
    {
        timer.Stop();
        state = savedState;
        Debug.Log(state);
    }

    public void WrongPosition()
    {
        savedState = state;
        state = TextState.WRONG_POS;
    }
}
