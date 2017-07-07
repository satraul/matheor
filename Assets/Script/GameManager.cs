using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private RuntimePlatform platform = Application.platform;
    private GameObject numberObject1;
    private GameObject numberObject2;

    public float velocity = 1.0f;
    public GameObject attackText;

    void Update() {
        if (platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        } else if (platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0)) {
                checkTouch(Input.mousePosition);
            }
        }
    }

    void checkTouch(Vector3 pos) {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if (hit) {
            if (hit.transform.gameObject.tag == "number") {
                if (!this.numberObject1) {
                    this.numberObject1 = hit.transform.gameObject;
                    this.numberObject1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    Debug.Log("lal");
                }
                else if (this.numberObject1 == hit.transform.gameObject) {
                    //Kalo sama
                    this.numberObject1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -this.velocity);
                    this.numberObject1 = null;
                }
                else if (this.numberObject1 != hit.transform.gameObject) {
                    //Kalo beda
                    this.numberObject2 = hit.transform.gameObject;
                    this.numberObject1.SendMessage("operated", 0, SendMessageOptions.DontRequireReceiver);
                    this.numberObject2.SendMessage("operated", 0, SendMessageOptions.DontRequireReceiver);
                    this.operation(this.numberObject1, this.numberObject2);
                    int attack = int.Parse(this.numberObject1.name) + int.Parse(this.numberObject2.name); //kalau bagi, liat yang bawah 0 bukan kalau iya switch
                    Debug.Log(attack);
                    this.numberObject1 = null;
                    this.numberObject2 = null;

                    StartCoroutine(instantiateAttackText(attack, 1f));
                }
            }
        }
    }

    void operation(GameObject operand1, GameObject operand2) {

    }

    void menuClicked() {
        Debug.Log("click");
    }

    IEnumerator instantiateAttackText(int attack, float timer) {
        yield return new WaitForSeconds(timer);
        Vector2 position = new Vector2(0, 6);
        GameObject x = Instantiate(attackText, position, Quaternion.identity);
        x.name = attack.ToString();
        x.GetComponent<TextMesh>().text = attack.ToString();
        StartCoroutine(launchAtack(x, attack, 0.25f));
    }

    IEnumerator launchAtack(GameObject attackText, int attack, float timer) {
        yield return new WaitForSeconds(timer);
        int attackVelo = (attack <= 10) ? 8 : -8;
        Rigidbody2D velo = attackText.GetComponent<Rigidbody2D>();
        velo.velocity = new Vector2(0, attackVelo);
        Destroy(attackText, 2.0f);
    }
}
