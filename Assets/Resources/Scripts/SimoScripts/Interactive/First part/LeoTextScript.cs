using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeoTextScript : MonoBehaviour
{
    private Text txt;

    //To pick a random phrase
    private int rnd;

    private string[] phrases = { "Vediamo chi arriva prima!", "Attenzione alla corrente, può essere davvero forte!",
    "Cercate di non sbattere contro i bordi, altrimenti perderete velocità!", "È proprio una sfida all’ultimo secondo!",
    "Forza! Manca davvero poco al traguardo!"};

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate;

    //Leo's different expressions. updateSprite used to avoid a bug for which the sprite is not updated outside of the Update() function
    private Sprite NORMAL, THINK, updateSprite;

    private Timer timer = new Timer();

    //Boolean used to deal with the conflict between the Update thread and the Timer thread
    private bool toFinish = true;

    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("TextLeo").GetComponent<Text>();
        toUpdate = txt.text;
        NORMAL = Resources.Load<Sprite>("Leo_waiting");
        THINK = Resources.Load<Sprite>("leo_explaining");
        updateSprite = THINK;

        timer.Interval = 3000f;
        timer.Elapsed += UpdateText;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //For the bug explained at the beginning
        txt.text = toUpdate;
        gameObject.GetComponent<Image>().sprite = updateSprite;

        if(GameManager.instance.firstDone && GameManager.instance.secondDone && toFinish)
        {
            toFinish = false;
            timer.Stop();
            finishLevel();
        }
    }

    void UpdateText(object o, System.EventArgs e)
    {
        timer.Stop();

        UnityThread.executeInUpdate(() =>
        {
            rnd = UnityEngine.Random.Range(0, phrases.Length);
        });

        toUpdate = phrases[rnd];

        if(rnd%2 == 0)
        {
            updateSprite = THINK;
        }
        else
        {
            updateSprite = NORMAL;
        }

        timer.Start();
    }

    void finishLevel()
    {
        timer.Elapsed += StartVideo;
        if(GameManager.instance.score1 < GameManager.instance.score2)
        {
            toUpdate = "Wow che Sfida! Per ora è in vantaggio la squadra di sinistra che ci ha messo "+ GameManager.instance.score1+" secondi. "
                + GameManager.instance.score2+" per la squadra di destra!";
        }
        else
        {
            toUpdate = "Wow che Sfida! Per ora è in vantaggio la squadra di destra che ci ha messo " + GameManager.instance.score2 + " secondi. "
                + GameManager.instance.score1 + " per la squadra di sinistra!";
        }
        timer.Start();
    }

    void StartVideo(object o, System.EventArgs e)
    {
        timer.Stop();
        GameManager.instance.firstComplete = true;
    }
}
