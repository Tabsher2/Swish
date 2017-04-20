using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PracticeController : MonoBehaviour
{
    private static bool MadeShot = false;

    public GameObject Basketball;
    private GameObject newBasketball;

    public GameObject PreviousToken;
    private GameObject previousTokenInstance;

    //UI Elements
    public GameObject obstacleMenu;
    public GameObject obstacleMenuButton;
    public GameObject mainMenuButton;
    public GameObject newLocationButton;
    public Text bestShotText;
    public Text lastShotText;

    private float gameTime;
    Camera mainCam;
    public int user = 4;
    private int bestShot = 0;
    private int lastShot;

    //UI text
    private GameObject shotText;
    public GameObject shotTextSwish;

    //Notification Listeners
    private static bool transitionToShotSelection = false;
    private static bool selectingShot = false;
    public static bool slideCameraUp = false;
    public static bool spotSelected = false;
    public static bool disableThrow = false;


    //Spawn Locations
    public static Vector3 ballStart;
    public static Vector3 textStartPos = new Vector3(7f, 3f, 1f);

    //Ball Variables
    private static int shotScore = 0;
    private static bool swish = false;
    private bool ballInPlay = false;
    private bool doubleMake = true;

    //Replay variables

    //Obstacle Variables
    static Vector3 birdView = new Vector3(0, 20, 0);
    static Vector3 satellite = new Vector3(90, 0, 270);
    static Vector3 hoopLocation = new Vector3(8.5f, 2.5f, 0);

    private void Awake()
    {
        mainCam = Camera.main;
    }
    // Use this for initialization
    void Start()
    {
        //Send them to shot selection
        obstacleMenuButton.SetActive(false);
        mainMenuButton.SetActive(false);
        newLocationButton.SetActive(false);
        bestShotText.enabled = false;
        lastShotText.enabled = false;
        mainCam.transform.position = new Vector3(0, 20, 0);
        mainCam.transform.eulerAngles = new Vector3(90, 0, 270);
        PracticeLocationSelector.allowSelection = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            Instantiate(Basketball, ballStart, Quaternion.identity);

        if (MadeShot && doubleMake)
        {
            Debug.Log(ThrowScript.shotVelocity);
            //do things after user makes shot
            Debug.Log("Shot made registered in update method");
            ShotMade();
        }

        if (shotText != null)
            TextFade();
        if (IsBallExisting())
        {
            if (selectingShot || !IsBallAtStart())
                ballInPlay = true;
            else
                ballInPlay = false;
        }

        if (ThrowScript.isThrown && IsBallExisting() && KillBall())
        {
            CreateBall();
        }

        DetermineButtons();

        if (slideCameraUp && mainCam.transform.position.y != 20)
            PracticeCameraController.MoveToShotSelection(Camera.main.transform.position, Camera.main.transform.eulerAngles);
        else if (slideCameraUp && mainCam.transform.position.y == 20)
        {
            mainCam.transform.eulerAngles = satellite;
            slideCameraUp = false;
        }

        if (spotSelected)
        {
            ballStart = PracticeLocationSelector.selectedLocation;
            Destroy(previousTokenInstance);
            newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
            spotSelected = false;
            selectingShot = false;
            PracticeLocationSelector.allowSelection = false;

            //Re-enable UI
            obstacleMenuButton.SetActive(true);
            mainMenuButton.SetActive(true);
            newLocationButton.SetActive(true);
            lastShotText.enabled = true;
            bestShotText.enabled = true;
        }
    }

    public void CreateBall()
    {
        ResetBall();
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
    }

    private void ResetBall()
    {
        Destroy(newBasketball);
        PracticeBNT.ResetTrigger();
        PracticeTNT.ResetTrigger();
        PracticeScoreAccumulator.ResetScore();
        ThrowScript.ResetThrow();
        CheckBallTimeout.ResetDeadBall();
        BallCollision.ResetBallCounter();
        doubleMake = true;
        shotScore = 0;
    }

    public static void SetMadeShot(int score, bool isSwish)
    {
        MadeShot = true;
        shotScore = score;
        swish = isSwish;
        Debug.Log("Made Shot!");
    }

    private void ShotMade()
    {
        shotText = Instantiate(shotTextSwish, textStartPos, Quaternion.Euler(-10, 90, 0));
        if (swish)
            shotText.GetComponent<TextMesh>().text = "Swish! - " + shotScore.ToString();
        else
            shotText.GetComponent<TextMesh>().text = "Made it! - " + shotScore.ToString();
        UpdateScoreText();
        MadeShot = false;
        doubleMake = false;
    }

    public void TextFade()
    {
        shotText.transform.Translate(0, Time.deltaTime, 0);
    }

    private bool KillBall()
    {
        BallCollision.t--;
        bool killball = false;
        if (newBasketball.transform.position.x > 35)
            killball = true;
        else if (newBasketball.transform.position.x < -25)
            killball = true;
        else if (newBasketball.transform.position.z > 35)
            killball = true;
        else if (newBasketball.transform.position.z < -25)
            killball = true;
        else if (newBasketball.transform.position.y < -1)
            killball = true;
        else if (newBasketball.transform.position.y > 32)
            killball = true;
        else if (newBasketball.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0) && newBasketball.transform.position != ballStart)
            killball = true;
        else if (CheckBallTimeout.IsBallDead())
            killball = true;
        else if (BallCollision.t == 0)
            killball = true;
        return killball;

    }

    public void OpenObstacleMenu()
    {
        obstacleMenu.SetActive(true);
        newBasketball.SetActive(false);
        bestShotText.enabled = false;
        lastShotText.enabled = false;
        newLocationButton.SetActive(false);
        obstacleMenuButton.SetActive(false);
        mainMenuButton.SetActive(false);
        mainCam.transform.position = birdView;
        mainCam.transform.eulerAngles = satellite;
        previousTokenInstance = Instantiate(PreviousToken, new Vector3(ballStart.x, 0.08f, ballStart.z), Quaternion.identity);
    }

    public void CloseObstacleMenu()
    {
        obstacleMenu.SetActive(false);
        newBasketball.SetActive(true);
        bestShotText.enabled = true;
        lastShotText.enabled = true;
        newLocationButton.SetActive(true);
        obstacleMenuButton.SetActive(true);
        mainMenuButton.SetActive(true);
        mainCam.transform.position = PracticeLocationSelector.cameraLocation;
        mainCam.transform.eulerAngles = PracticeLocationSelector.cameraAngle;
        mainCam.transform.LookAt(hoopLocation);
        Destroy(previousTokenInstance);
    }

    #region "Button Handling" 

    public void DisableButtons()
    {
        //Prevent them from interfering with the ball
        disableThrow = true;
        //obstacleMenuButton.GetComponent<Image>().color = Color.gray;
        obstacleMenuButton.GetComponent<Button>().enabled = false;
        //newLocationButton.GetComponent<Image>().color = Color.gray;
        newLocationButton.GetComponent<Button>().enabled = false;
        //mainMenuButton.GetComponent<Image>().color = Color.gray;
        mainMenuButton.GetComponent<Button>().enabled = false;
    }

    public void EnableButtons()
    {
        disableThrow = false;
        //obstacleMenuButton.GetComponent<Image>().color = Color.white;
        obstacleMenuButton.GetComponent<Button>().enabled = true;
        //newLocationButton.GetComponent<Image>().color = Color.white;
        newLocationButton.GetComponent<Button>().enabled = true;
        //mainMenuButton.GetComponent<Image>().color = Color.white;
        mainMenuButton.GetComponent<Button>().enabled = true;

    }

    private void DetermineButtons()
    {
        if (ballInPlay)
            DisableButtons();
        else if (PracticeLocationSelector.slideCameraDown)
            DisableButtons();
        else
            EnableButtons();
    }

    #endregion

    private bool IsBallAtStart()
    {
        if (newBasketball.transform.position != ballStart)
            return false;
        else
            return true;
    }

    private bool IsBallExisting()
    {
        if (newBasketball != null)
            return true;
        else
            return false;
    }

    private void UpdateScoreText()
    {
        lastShot = shotScore;
        lastShotText.text = "LAST SHOT: " + lastShot.ToString();
        if (lastShot > bestShot)
        {
            bestShot = lastShot;
            bestShotText.text = "BEST SHOT: " + bestShot.ToString();
        }

    }

    #region "Shot Selection"

    public void ActivateShotSelection()
    {
        Destroy(newBasketball);
        previousTokenInstance = Instantiate(PreviousToken, new Vector3(ballStart.x, 0.08f, ballStart.z), Quaternion.identity);
        selectingShot = true;
        obstacleMenuButton.SetActive(false);
        mainMenuButton.SetActive(false);
        newLocationButton.SetActive(false);
        mainMenuButton.SetActive(false);
        lastShotText.enabled = false;
        bestShotText.enabled = false;
        slideCameraUp = true;
        mainCam.transform.eulerAngles = new Vector3(0, 90, 0);
        AllowLocationSelection();
    }

    private void AllowLocationSelection()
    {
        PracticeLocationSelector.allowSelection = true;
    }

    public float CalculateDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.z - v2.z) * (v1.z - v2.z));
    }

    #endregion

    private void OnApplicationQuit()
    {
        gameTime = Time.realtimeSinceStartup;
        gameTime /= (60 * 60);
        NetworkController.UpdateTime(user, gameTime);
    }

    public void ReturnToMenu()
    {
        ResetBall();
        ObstacleController.placedObstacles.Clear();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
