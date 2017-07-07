using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour {

    public int value;
    private bool isOperated = false;

    private Vector2 startPoint;
    private Vector2 endPoint = new Vector2(0,5);
    private float duration = 1.0f;
    private float startTime;
    private int rotateDirection;

    private Vector3 startScale;
    private Vector3 endScale = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
		if (transform.position.x > 0) {
            rotateDirection = -1;
        } else {
            rotateDirection = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0) {
            Destroy(gameObject, 1.0f);
        }
        if (isOperated) {
            transform.position = Vector2.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
            transform.localScale = Vector2.Lerp(startScale, endScale, (Time.time - startTime) / duration);
            transform.Rotate(Vector3.forward * 8 * rotateDirection);
            if (transform.position == new Vector3(0,5,0)) {
                Destroy(gameObject);
            }
        }
	}

    void operated() {
        startPoint = transform.position;
        startScale = transform.localScale;
        startTime = Time.time;
        this.isOperated = true;
    }
}
