using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float velocity;
    public GameObject[] numbers = new GameObject[10];

	// Use this for initialization
	void Start () {
        InvokeRepeating("GenerateNumber", 0.0f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateNumber() {
        Vector2 position = new Vector2(Random.Range(-2.0f, 2.0f), 11);

        int no = Random.Range(0, 10);
        GameObject x = Instantiate(this.numbers[no], position, Quaternion.identity);
        x.name = no.ToString();

        //Instantiate velocity
        Rigidbody2D velo = x.GetComponent<Rigidbody2D>();
        velo.velocity = new Vector2(0, -this.velocity);
    }
}
