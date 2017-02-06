using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static bool MadeShot = false;

    public GameObject shotText;
    public GameObject shotTextSwish;
    public GameObject Basketball;
    public static Vector3 ballStart = new Vector3(3f, 1f, 0f);
    public static Vector3 textStartPos = new Vector3(7f, 3f, 1f);

    public static bool throughBasket = false;
    private static float shotScore = 0;
    private static bool swish = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            Instantiate(Basketball, ballStart, Quaternion.identity);
        
        if (MadeShot)
        {
            //do things after user makes shot
            Debug.Log("Shot made registered in update method");
            SwishMade();
        }
        if (shotText != null)
            TextFade();
    }

    public void CreateBall()
    {
        ResetBall();
        Instantiate(Basketball, ballStart, Quaternion.identity);
    }
    private void ResetBall()
    {
        BottomNetTrigger.resetTrigger();
        TopNetTrigger.resetTrigger();
    }

    public void OnTriggerExit(Collider other)
    {
        throughBasket = true;
    }

    public static void SetMadeShot(float score, bool isSwish)
    {
        MadeShot = true;
        shotScore = score;
        swish = isSwish;
        Debug.Log("Made Shot!");
    }

    private void SwishMade()
    {
        shotText = Instantiate(shotTextSwish, textStartPos, Quaternion.Euler(-10,90,0));
        if (swish)
            shotText.GetComponent<TextMesh>().text = "Swish! - " + shotScore.ToString();
        else
            shotText.GetComponent<TextMesh>().text = "Made it! - " + shotScore.ToString();
        MadeShot = false;
    }

    public void TextFade()
    {
        shotText.transform.Translate(0, Time.deltaTime, 0);
    }
}
