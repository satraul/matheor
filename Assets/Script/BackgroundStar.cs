using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStar : MonoBehaviour {

    private Vector2 startPosNow;
    private Vector2 startPosNext;
    private Vector2 pos = new Vector2(-3.2f * 2.5f, 6.5f);
    private float startTimeNow;
    private float startTimeNext;
    private Vector2 endPos = new Vector2(3.2f * 2.5f, 6.5f);
    private float duration = 30f;
    private GameObject starNow;
    private GameObject starNext;

    public GameObject star;

    // Use this for initialization
    void Start() {
        this.starNow = Instantiate(star, new Vector2(-2.8f, 6.5f), Quaternion.identity);
        this.startPosNow = this.starNow.transform.position;
        this.startTimeNow = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (this.starNext) {
            this.starNow.transform.position = Vector2.Lerp(this.startPosNow, this.endPos, (Time.time - this.startTimeNow) / this.duration);
            this.starNext.transform.position = Vector2.Lerp(this.startPosNext, new Vector2(2.8f * 5, 6.5f), (Time.time - this.startTimeNext) / (this.duration * 2.5f));
        } else {
            this.starNow.transform.position = Vector2.Lerp(this.startPosNow, this.endPos, (Time.time - this.startTimeNow) / this.duration);
        }
        
        if (this.starNow.transform.position.x >= 2.7f && !this.starNext) {
            this.starNext = Instantiate(star, pos, Quaternion.identity);
            this.startPosNext = this.starNext.transform.position;
            this.startTimeNext = Time.time;
        } else if (this.starNow.transform.position.x >= 3.2f * 2.5f) {
            Destroy(this.starNow);
            this.starNow = this.starNext;
            this.starNext = null;
            this.startPosNow = this.starNow.transform.position;
            this.startTimeNow = Time.time;
        }
    }
}

