using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public enum TextState
{
    INTRO, BASE, BASE_DONE, BIG, BIG_DONE, SMALL, SMALL_DONE, BEAM, BEAM_DONE, TRIANGLE,
    TRIANGLE_DONE, MAST, MAST_DONE, FABRIC, FABRIC_DONE, HANDLE, HANDLE_DONE, WRONG_OBJ, WRONG_POS
}

public class GameManager : MonoBehaviour
{
    //enum TextState { INTRO, BASE, BIG, SMALL, BEAM, TRIANGLE, FABRIC, HANDLE };

    //saveState needed to retrieve the current state after an error message or to swtich to the next state after an explaination.
    public TextState state, savedState;

    private Text txt;

    private GameObject objs, leoSprite;

    private Timer timer = new Timer();

    //Leo's different expressions. updateSprite used to avoid a bug for which the sprite is not updated outside of the Update() function
    private Sprite NORMAL, HAPPY, THINK, SAD, updateSprite;

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate;

    private string[] beamPhrases = { "Perfetto, siamo riusciti a fissare tutti e tre i paletti che sostengno lateralmente l'albero", "Ancora un paletto e il meccanismo di rotazione sarà completato",
        "Ben fatto! Hai posizionato il primo di tre paletti che sostengono l'albero e fanno ruotare l'elica" };

    //Counter for the beams
    private int beamCounter = 2;

    // Start is called before the first frame update
    void Start()
    {
        state = TextState.INTRO;
        txt = GameObject.Find("TextLeo").GetComponent<Text>();
        objs = GameObject.Find("Components");
        timer.Interval = 5000f;
        timer.Elapsed += NextIntro;
        toUpdate = txt.text;

        leoSprite = GameObject.Find("leo");
        NORMAL = Resources.Load<Sprite>("Leo_explaining");
        HAPPY = Resources.Load<Sprite>("Leo_happy");
        THINK = Resources.Load<Sprite>("Leo_waiting");
        SAD = Resources.Load<Sprite>("Leo_sad");
        updateSprite = NORMAL;
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
                    toUpdate = "Innazitutto c'è bisogno di qualcosa di ampio su cui poter stare in piedi";
                    savedState = state;
                    break;
                case TextState.BASE_DONE:
                    toUpdate = "Ottimo! Abbiamo sistemato un'ampia base circolare su cui i piloti corrono per far girare le pale";
                    savedState = TextState.MAST;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.MAST:
                    toUpdate = "Abbiamo bisogno di qualcosa di grosso e resistente per sostenere tutta la macchina";
                    savedState = state;
                    break;
                case TextState.MAST_DONE:
                    toUpdate = "Grazie! Questo è l'albero principale della macchina, è fondamentale per il movimento dell'elica e per la stabilità dell'elicottero";
                    savedState = TextState.BIG;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.BIG:
                    toUpdate = "Ci vuole una base a cui appoggiare il palo...qualcosa di circolare e abbastanza grande";
                    savedState = state;
                    break;
                case TextState.BIG_DONE:
                    toUpdate = "E con questo possiamo fissare l'albero alla base della macchina";
                    savedState = TextState.BEAM;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.BEAM:
                    toUpdate = "Non possiamo rischiare che la macchina si rompa...servono dei sostegni laterali!";
                    savedState = state;
                    break;
                case TextState.BEAM_DONE:
                    if (beamCounter == 0)
                    {
                        savedState = TextState.SMALL;
                    }
                    else
                    {
                        toUpdate = beamPhrases[beamCounter];
                        beamCounter--;
                        savedState = TextState.BEAM;
                    }
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.SMALL:
                    toUpdate = "Credo sia importante agganciare i sostengi a qualcosa sul palo";
                    savedState = state;
                    break;
                case TextState.SMALL_DONE:
                    toUpdate = "Questo piccolo cilindro è fondamentale per fissare i paletti all'albero e farlo girare";
                    savedState = TextState.FABRIC;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.FABRIC:
                    toUpdate = "Chiaramente non potrà muoversi con la magia...c'è bisogno di qualcosa che venga colpito dall'aria per poter volare!";
                    savedState = state;
                    break;
                case TextState.FABRIC_DONE:
                    toUpdate = "La tela tesa in cima all'albero farà sollevare la macchina: grazie alla sua forma 'a cavatappi', la rotazione spinge l'aria verso il basso facendo sollevare l'elicottero!";
                    savedState = TextState.HANDLE;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.HANDLE:
                    toUpdate = "C'è bisogno anche di qualcosa a cui aggrapparsi per non cadere giù mentre si aziona la macchina";
                    savedState = state;
                    break;
                case TextState.HANDLE_DONE:
                    toUpdate = "Grazie a questa asta orizzontale i piloti potranno tenersi saldamente a qualcosa, riuscendo a dare più forza nella rotazione e riducendo il rischio di caduta";
                    savedState = TextState.TRIANGLE;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.TRIANGLE:
                    toUpdate = "Infine blocchiamo tutto fissando l'ultimo pezzo in alto";
                    savedState = state;
                    break;
                case TextState.TRIANGLE_DONE:
                    toUpdate = "Ce l'abbiamo fatta! Siamo riusciti ad assemblare il primo elicottero della storia";
                    updateSprite = HAPPY;
                    break;
                case TextState.WRONG_POS:
                    timer.Elapsed += WrongPick;
                    toUpdate = "È un'idea carina ma non penso potrebbe funzionare. Prova a riposizionarlo!";
                    updateSprite = SAD;
                    timer.Start();
                    break;
                case TextState.WRONG_OBJ:
                    timer.Elapsed += WrongPick;
                    toUpdate = "Non sono sicuro che questo vada bene...prova a prendere un altro oggetto!";
                    updateSprite = SAD;
                    Debug.Log(savedState);
                    timer.Start();
                    break;
                default:
                    break;
            }
        }
        //For the bug explained at the beginning
        txt.text = toUpdate;

        leoSprite.GetComponent<Image>().sprite = updateSprite;
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
        updateSprite = THINK;
        //activateObjects(true);
    }

    void WrongPick(object o, System.EventArgs e)
    {
        timer.Stop();
        state = savedState;
        updateSprite = THINK;
    }

    public void WrongPosition()
    {
        state = TextState.WRONG_POS;
    }

    public void WrongObject()
    {
        state = TextState.WRONG_OBJ;
    }

    public void NextStep(object o, System.EventArgs e)
    {
        timer.Stop();
        state = savedState;
    }

    //This functions is used to set the boolean that determines whether the objects are draggable or not
    /*private void activateObjects(bool active)
    {
        if (objs != null)
        {
            Debug.Log("Non è null");
            /*PolygonCollider2D[] colliders = objs.GetComponentsInChildren<PolygonCollider2D>();
            foreach (PolygonCollider2D collider in colliders)
            {
                collider.enabled = true;
            }
            foreach(Transform child in objs.transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
        else
        {
            Debug.Log("è null");
        }
    }*/
}
