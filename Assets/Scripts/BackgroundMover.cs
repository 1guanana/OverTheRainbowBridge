using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public enum dayNight
    {
        Day,
        Night,
        Rainbow
    }
    public dayNight time;
    public float scrollSpeed;
    public GameObject textPanel;
    public bool nowWaiting;

    private DogManager dm;
    private bool textRead;
    private bool ascended;

    void Start()
    {
        time = dayNight.Day;
        this.transform.position = new Vector2(8.01f, 0);
        dm = GameObject.Find("Dog").GetComponent<DogManager>();
        textPanel = GameObject.Find("TextPanel");
    }

    void Update()
    {
        if (dm.isRunning)
            this.transform.position = new Vector2(transform.position.x - scrollSpeed, transform.position.y);

        // Change day/night cycle
        if (this.transform.position.x <= 3f && this.transform.position.x >= 2.9f && !textRead)
        {
            textRead = true;
            time = dayNight.Night;
            dm.isRunning = false;
            dm.allowMove = false;
            textPanel.SetActive(true);
        }
        if (this.transform.position.x <= -3f && this.transform.position.x >= -3.1f && !textRead)
        {
            textRead = true;
            time = dayNight.Rainbow;
            dm.isRunning = false;
            dm.allowMove = false;
            textPanel.SetActive(true);
        }
        if (this.transform.position.x <= -8f && !ascended)
        {
            dm.isRunning = false;
            ascended = true;
            dm.Ascend();
        }
    }

    // Wait before allowing textbox to return
    public IEnumerator Waiting()
    {
        nowWaiting = true;
        yield return new WaitForSeconds(1f);
        textRead = false;
    }
}
