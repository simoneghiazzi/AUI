﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondTutorialScript : MonoBehaviour
{
    private Text txt;

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate;

    //Leo's different expressions. updateSprite used to avoid a bug for which the sprite is not updated outside of the Update() function
    private Sprite HAPPY, THINK, updateSprite;

    private Timer timer = new Timer();

    //Leo's phrases
    private string[] leoPhrases = { "In questo secondo livello, ogni giocatore controllerá un cancello della conca",
        "Se siete posizionati su “cancello inferiore”, vi occuperete dello sbarramento che serve ad entrare nella conca; se la vostra posizione è “cancello superiore”, vi occuperete dello sbarramento in uscita",
        "Farlo è semplicissimo: ruotate il braccio in senso orario per aprire e in senso antiorario per chiudere. La barca si muoverá automaticamente!",
        "Ma attenzione! Come avete visto nel video, affinché il meccanismo funzioni, prima di aprire lo sbarramento in uscita, quello in entrata deve essere chiuso!",
        "Pronti a iniziare!" };

    //Index of the phrase to be said by Leo
    private int index = 0;

    public GameObject video;

    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("TextLeo").GetComponent<Text>();
        toUpdate = txt.text;
        THINK = Resources.Load<Sprite>("leo_explaining");
        HAPPY = Resources.Load<Sprite>("Leo_happy");
        updateSprite = HAPPY;

        timer.Interval = 5000f;
        timer.Elapsed += NextIntro;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //For the bug explained at the beginning
        txt.text = toUpdate;
        gameObject.GetComponent<Image>().sprite = updateSprite;

        if (index == 3)
        {
            Debug.Log("CIAO");
            video.SetActive(true);
        }
        else
        {
            video.SetActive(false);
        }

        if (index == leoPhrases.Length + 1)
        {
            timer.Stop();
            StartLevel();
        }
    }

    void NextIntro(object o, System.EventArgs e)
    {
        timer.Stop();
        if (index < leoPhrases.Length)
        {
            toUpdate = leoPhrases[index];
        }
        updateSprite = THINK;
        index++;
        timer.Start();
    }

    void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
