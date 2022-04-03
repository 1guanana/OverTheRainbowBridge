using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetter : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI mainTMP;
    [SerializeField] TextMeshProUGUI hscoreTMP;
    [SerializeField] TextMeshProUGUI sscoreTMP;

    public GameObject runPrompt;
    public GameObject restartPrompt;

    private BackgroundMover bm;
    private DogManager dm;
    private int textInd;
    private string[] DayText =
    {
        "I used to love playing with my human." +
            " They would throw my stick, and I would go get it and bring it back for them." +
            " We had so much fun."
        , "When we got home, I would get a treat." +
            " My human would sit on the couch and watch the big screen," +
            " and I would nap beside them."
        , "Every once in a while they would give me" +
            " a scratch, and I would curl up closer. We kept eachother warm."
    };
    private string[] NightText =
    {
        "We would play for so long." +
            " I started to get tired though, and couldn't play like I used to." +
            " I wasn't so good at zooming anymore, and my joints started aching."
        , "My human gave me special cheese that made me feel better" +
            " so I could play without hurting so much. It tasted a little weird," +
            " but I didn't mind so much since it meant I could play longer."
    };
    private string[] RainbowText =
    {
        "I remember when it didn't matter how much cheese I ate." +
            " I always hurt and I was too tired to play anymore. I slept all the time, but" +
            " my human didn't mind. They loved me just as much as when we played all day. And I loved them."
        , "I went to see the vet more often, though. They would poke" +
            " me and prod me, but my human stayed with me so I knew it would be okay." +
            " Eventually we stopped going to see the vet. I just stayed at home."
        , "My human would lay with me and pet my head." +
            " They would tell me \"All dogs go to heaven.\""

    };

    void Start()
    {
        textInd = 0;
        mainTMP = GameObject.FindGameObjectWithTag("MainText").GetComponent<TextMeshProUGUI>();
        hscoreTMP = GameObject.Find("HeartScore").GetComponent<TextMeshProUGUI>();
        sscoreTMP = GameObject.Find("SkullScore").GetComponent<TextMeshProUGUI>();

        runPrompt = GameObject.Find("PromptPanel");
        restartPrompt = GameObject.FindGameObjectWithTag("RestartPanel");
        bm = GameObject.Find("Background").GetComponent<BackgroundMover>();
        dm = GameObject.Find("Dog").GetComponent<DogManager>();

        runPrompt.SetActive(false);
        restartPrompt.SetActive(false);
        mainTMP.text = DayText[textInd];
        UpdateScores();
    }

    public void NextText()
    {
        switch (bm.time)
        {
            case BackgroundMover.dayNight.Day:
                if (textInd < DayText.Length)
                    mainTMP.text = DayText[textInd++];
                else
                {
                    ResetUI();
                    mainTMP.text = NightText[textInd];
                }
                break;
            case BackgroundMover.dayNight.Night:
                if (textInd < NightText.Length)
                    mainTMP.text = NightText[textInd++];
                else
                {
                    ResetUI();
                    mainTMP.text = RainbowText[textInd];
                }
                break;
            case BackgroundMover.dayNight.Rainbow:
                if (textInd < RainbowText.Length)
                    mainTMP.text = RainbowText[textInd++];
                else
                    ResetUI();
                break;
        }
    }

    public void UpdateScores() {
        hscoreTMP.text = dm.heartScore.ToString();
        sscoreTMP.text = dm.skullScore.ToString();
    }

    public void ShowRestartPrompt() 
    {
        //if (restartPrompt)
        restartPrompt.gameObject.SetActive(true);
    }

    private void ResetUI()
    {
        textInd = 0;
        if (bm.textPanel)
            bm.textPanel.SetActive(false);
        dm.allowMove = true;
        runPrompt.gameObject.SetActive(true);
    }
}
