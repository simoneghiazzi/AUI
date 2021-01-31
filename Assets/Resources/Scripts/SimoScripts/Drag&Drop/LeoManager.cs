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

public class LeoManager : MonoBehaviour
{
    //saveState needed to retrieve the current state after an error message or to swtich to the next state after an explaination.
    public TextState state, savedState;

    private Text txt;

    private GameObject objs, leoSprite;

    private Timer timer = new Timer();

    //Leo's different expressions. updateSprite used to avoid a bug for which the sprite is not updated outside of the Update() function
    private Sprite NORMAL, HAPPY, THINK, SAD, updateSprite;

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate;

    private string[] beamPhrases = { "Perfetto! Questi tiranti serviranno a controbilanciare il peso dell’albero, e soprattutto la forza del vento!", "Ancora un paletto e il meccanismo di rotazione sarà completato",
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
        timer.Start();

        toUpdate = txt.text;

        leoSprite = GameObject.Find("leo");
        NORMAL = Resources.Load<Sprite>("leo_explaining");
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
                case TextState.BASE:
                    toUpdate = "Per cominciare, è necessario qualcosa di ampio e circolare a cui fissare il tutto, e su cui i piloti possano stare in piedi";
                    savedState = state;
                    break;
                case TextState.BASE_DONE:
                    toUpdate = "Ottimo! Questa è la base dell’elicottero, su cui i piloti corrono per far girare le pale. La sua ampiezza conferisce piena stabilità";
                    savedState = TextState.MAST;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.MAST:
                    toUpdate = "Ora abbiamo bisogno di qualcosa di grosso e resistente che sostenga verticalmente tutta la macchina";
                    savedState = state;
                    break;
                case TextState.MAST_DONE:
                    toUpdate = "Esatto! Abbiamo appena posizionato l’albero maestro dell’elicottero, fondamentale per muovere l’elica e conferire stabilità";
                    savedState = TextState.BIG;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.BIG:
                    toUpdate = "Aggiungiamo un altro componente. Ci vuole un’altra base circolare, per ancorare maggiormente l’albero alla grande base principale";
                    savedState = state;
                    break;
                case TextState.BIG_DONE:
                    toUpdate = "Molto bene! Ora l’attacco tra la base e l’albero sarà ancora più solido e resistente.";
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
                    toUpdate = "Bisogna però ancorarli al palo stesso tramite un piccolo, ma robusto, anello circolare";
                    savedState = state;
                    break;
                case TextState.SMALL_DONE:
                    toUpdate = "Esatto! Solo così, infatti, i tiranti potranno muoversi insieme alla base principale, consentendo la rotazione dell’albero";
                    savedState = TextState.FABRIC;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.FABRIC:
                    toUpdate = "Chiaramente non potrà muoversi con la magia...c'è bisogno di qualcosa che venga colpito dall'aria per poter volare!";
                    savedState = state;
                    break;
                case TextState.FABRIC_DONE:
                    toUpdate = "La tela tesa in cima all'albero farà sollevare la macchina: grazie alla sua forma 'a cavatappi', la rotazione spingerá l'aria verso il basso facendo sollevare l'elicottero!";
                    savedState = TextState.HANDLE;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.HANDLE:
                    toUpdate = "Ma come si aziona l’elicottero? Bisogna posizionare un appoggio sull’albero principale!";
                    savedState = state;
                    break;
                case TextState.HANDLE_DONE:
                    toUpdate = "Ben fatto! Su questa asta orizzontale, infatti, i piloti imprimono la loro forza muscolare, girando intorno all’albero e azionando la macchina";
                    savedState = TextState.TRIANGLE;
                    timer.Elapsed += NextStep;
                    timer.Start();
                    break;
                case TextState.TRIANGLE:
                    toUpdate = "Infine blocchiamo tutto fissando l'ultimo pezzo in alto";
                    savedState = state;
                    break;
                case TextState.TRIANGLE_DONE:
                    toUpdate = "Ce l’abbiamo fatta! Siamo riusciti a costruire il primo elicottero della storia. Passiamo alla seconda invenzione!";
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
        toUpdate = "Per cominciare, ho bisogno di quattro giocatori che a turno, per ciascuna macchina volante, dovranno assemblarne i componenti.";
        timer.Elapsed += FinalIntro;
        timer.Start();
    }

    void FinalIntro(object o, System.EventArgs e)
    {
        timer.Stop();
        toUpdate = "Per afferrare un oggetto chiudi la mano a pugno davanti a quello. " +
        "Tenendo il pugno chiuso trascinalo dove vuoi metterlo e riapri la mano per lasciarlo andare";
        timer.Elapsed += BeginGame;
        timer.Start();
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
        savedState = state;
        state = TextState.WRONG_POS;
    }

    public void WrongObject()
    {
        savedState = state;
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
