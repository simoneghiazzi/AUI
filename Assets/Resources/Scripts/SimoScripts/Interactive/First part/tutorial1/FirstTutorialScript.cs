using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstTutorialScript : MonoBehaviour
{
    private Text txt;

    //This is necessary to avoid a bug of Unity for which updating the text outside of the main Update() function doesn't work
    private string toUpdate;

    //Leo's different expressions. updateSprite used to avoid a bug for which the sprite is not updated outside of the Update() function
    private Sprite NORMAL, HAPPY, THINK, SAD, updateSprite;

    private Timer timer = new Timer();

    //Leo's phrases
    private string[] leoPhrases = { "Il sistema delle conche consente alle piccole imbarcazioni di percorrere lunghi corsi d’acqua caratterizzati da un dislivello del terreno",
    "In questo livello parteciperete ad una gara tra barche all’ultimo secondo! Sono necessarie due squadre, ognuna costituita da due giocatori",
    "Ora, disponetevi uno accanto all’altro in corrispondenza delle macchie colorate sul pavimento",
    "Chi rema deve muovere il piú velocemente possibile le braccia in alto e in basso come se volasse, tenendole laterali al corpo",
    "L’altro componente della squadra sarà al timone: ruotando in senso orario o antiorario il braccio, infatti, potrà direzionare la barca senza farla scontrare con i bordi della conca"};

    //Index of the phrase to be said by Leo
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("TextLeo").GetComponent<Text>();
        toUpdate = txt.text;
        THINK = Resources.Load<Sprite>("leo_explaining");
        HAPPY = Resources.Load<Sprite>("Leo_happy");
        updateSprite = HAPPY;

        timer.Interval = 1000f;
        timer.Elapsed += NextIntro;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //For the bug explained at the beginning
        txt.text = toUpdate;
        gameObject.GetComponent<Image>().sprite = updateSprite;

        if (index == leoPhrases.Length)
        {
            timer.Stop();
            StartLevel();
        }
    }

    void NextIntro(object o, System.EventArgs e)
    {
        timer.Stop();
        toUpdate = leoPhrases[index];
        updateSprite = THINK;
        index++;
        timer.Start();
    }

    void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
