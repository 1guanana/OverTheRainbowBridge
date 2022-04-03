using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    public bool isRunning;
    public bool allowMove;
    public int heartScore;
    public int skullScore;

    private BackgroundMover bm;
    private TextSetter ts;
    private int lanePos;
    private Animator anim;
    private GameObject heart;
    private GameObject skull;
    private bool inGame;
    private bool paused;

    void Start()
    {
        bm = GameObject.Find("Background").GetComponent<BackgroundMover>();
        ts = GameObject.Find("Canvas").GetComponent<TextSetter>();
        anim = this.GetComponent<Animator>();
        heart = GameObject.Find("Heart");
        skull = GameObject.Find("Skull");
        inGame = true;
        paused = false;

        anim.Play("DogSit");
        this.gameObject.transform.position = new Vector2(-.9f, -.43f);
        lanePos = 2;
        isRunning = false;
        allowMove = false;
        heartScore = 0;
        skullScore = 0;
        heart.SetActive(false);
        skull.SetActive(false);
    }

    void Update()
    {
        // Start running
        if (Input.GetKeyDown(KeyCode.Space) && allowMove && inGame)
        {
            isRunning = true;
            if (!bm.nowWaiting)
                StartCoroutine(bm.Waiting());
        }

        if (isRunning && inGame)
        {
            anim.Play("DogRun");
            ts.runPrompt.SetActive(false);
            SwitchLanes();
            bm.nowWaiting = false;
        }

        if (!isRunning && inGame)
            anim.Play("DogSit");

        switch (bm.time)
        {
            case BackgroundMover.dayNight.Day:
                anim.speed = 1.25f;
                break;
            case BackgroundMover.dayNight.Night:
                anim.speed = 1f;
                break;
            case BackgroundMover.dayNight.Rainbow:
                anim.speed = 0.5f;
                break;
            default:
                break;
        }
    }

    public void Ascend()
    {
        inGame = false;
        allowMove = false;
        isRunning = false;
        ts.runPrompt.SetActive(true);
        StartCoroutine(OffScreenCoroutine());
    }

    public void ShowHeart()
    {

        StartCoroutine(HeartCoroutine());
    }

    public void ShowSkull()
    {
        StartCoroutine(SkullCoroutine());
    }

    private IEnumerator OffScreenCoroutine()
    {
        anim.Play("DogSit");
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        ts.runPrompt.SetActive(false);
        Vector2 currentPos = transform.position;
        while (Vector2.Distance(currentPos, new Vector2(2f, transform.position.y)) > 1f)
        {
            anim.Play("DogRun");
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(2f, transform.position.y), Time.deltaTime * 1f);

            if (transform.position.x == 2)
            {
                ts.ShowRestartPrompt();
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator HeartCoroutine()
    {
        heart.SetActive(true);
        yield return new WaitForSeconds(1f);
        heart.SetActive(false);
    }

    private IEnumerator SkullCoroutine()
    {
        skull.SetActive(true);
        yield return new WaitForSeconds(1f);
        skull.SetActive(false);
    }

    // Allow player input to change lanes
    private void SwitchLanes()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (lanePos <= 2)
            {
                this.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
                lanePos++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (lanePos >= 2)
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
                lanePos--;
            }
        }
    }
}
