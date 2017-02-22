using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static bool MadeShot = false;

    
    public GameObject Basketball;
    private GameObject newBasketball;
    
    public Text remainingShotsText;


    //Obstacles
    public GameObject obstacleMenu;
    public GameObject wallObstacle;
    private GameObject newWall;

    public GameObject trampolineObstacle;
    private GameObject newTrampoline;

    //UI text
    private GameObject shotText;
    public GameObject shotTextSwish;

    public static Vector3 ballStart = new Vector3(3f, 1f, 0f);
    public static Vector3 wallStart = new Vector3(6.08f, 1.92f, -1.37f);
    public static Vector3 trampStart = new Vector3(6f, 0.11f, 0f);
    public static Vector3 textStartPos = new Vector3(7f, 3f, 1f);

    public static bool throughBasket = false;
    private static float shotScore = 0;
    private static bool swish = false;
    private int remainingShots = 3;


    private void Awake()
    {
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
        RemoveAllObstacles();
    }
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
            ShotMade();
            remainingShots--;
            CreateBall();
        }
        if (shotText != null)
            TextFade();

        if (remainingShots == 0)
            Debug.Log("Turn ends here!");

        if (KillBall())
        {
            remainingShots--;
            CreateBall();
        }
    }

    public void CreateBall()
    {
        ResetBall();
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
        UpdateRSText();
    }
    private void ResetBall()
    {
        Destroy(newBasketball);
        BottomNetTrigger.ResetTrigger();
        TopNetTrigger.ResetTrigger();
        ScoreAccumulator.ResetScore();
        ThrowScript.ResetThrow();
        CheckBallTimeout.ResetDeadBall();
        shotScore = 0;
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

    private void ShotMade()
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

    private void UpdateRSText()
    {
        remainingShotsText.text = "Shots Left:\n\t\t" + remainingShots.ToString() + "/3";
    }

    private bool KillBall()
    {
        bool killball = false;
        if (newBasketball.transform.position.x > 35)
            killball = true;
        else if (newBasketball.transform.position.x < -25)
            killball = true;
        else if (newBasketball.transform.position.z > 35)
            killball = true;
        else if (newBasketball.transform.position.z < -25)
            killball = true;
        else if (newBasketball.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0) && newBasketball.transform.position != ballStart)
            killball = true;
        else if (CheckBallTimeout.IsBallDead())
            killball = true;
        return killball;

    }

    public void OpenObstacleMenu()
    {
        obstacleMenu.SetActive(true);
        newBasketball.SetActive(false);
    }

    public void CloseObstacleMenu()
    {
        obstacleMenu.SetActive(false);
        newBasketball.SetActive(true);
    }

    private void RemoveAllObstacles()
    {
        obstacleMenu.SetActive(false);
        wallObstacle.SetActive(false);
        trampolineObstacle.SetActive(false);
    }

    public void CreateWall()
    {
        newWall = Instantiate(wallObstacle, wallStart, Quaternion.Euler(0, 90, 0));
        newWall.SetActive(true);
    }

    public void CreateTrampoline()
    {
        newTrampoline = Instantiate(trampolineObstacle, trampStart, Quaternion.identity);
        newTrampoline.SetActive(true);
    }

}
