using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private static bool MadeShot = false;
    
    public GameObject Basketball;
    private GameObject newBasketball;
    public GameObject obstacleMenu;
    public GameObject obstacleMenuButton;

    //Multiplayer info
    private bool takingShot;
    private bool copyingShot;
    private int user;
    private int opponent;
    private int player;
    private float userScore;
    private float opponentScore;
    private string userLetters = "";
    private string opponentLetters = "";
    private float locationX;
    private float locationZ;
    private int turnCount = 0;
    private string userName;
    private string opponentName;

    //UI text
    private GameObject shotText;
    public GameObject shotTextSwish;
    public Text remainingShotsText;
    public Text userLettersText;
    public Text opponentLettersText;

    //Notification Variables
    public GameObject notificationPanel;
    public Text notificationMessage;

    //Spawn Locations
    public static Vector3 ballStart;
    public static Vector3 textStartPos = new Vector3(7f, 3f, 1f);

    //Ball Variables
    private static float shotScore = 0;
    private static bool swish = false;
    private int remainingShots = 3;
    private bool ballInPlay = false;

    //Replay variables
    private static Vector3 replayVelocity;
    public Text replayText;
    private GameObject replayBall;
    private static List<string> usedObstacles = new List<string>();
    private static bool startReplay = false;
    private static bool isReplaying = false;
    private static bool replayComplete = false;
    public GameObject replayShotButton;


    private void Awake()
    {
        replayText.GetComponent<Text>().enabled = false;
    }
    // Use this for initialization
    void Start()
    {
        //newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
        LoadShotData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            Instantiate(Basketball, ballStart, Quaternion.identity);
        
        if (MadeShot)
        {
            Debug.Log(ThrowScript.shotVelocity);
            //do things after user makes shot
            Debug.Log("Shot made registered in update method");
            ShotMade();
        }
        else
        {
            if (remainingShots == 0)
            {
                ThrowScript.isThrown = true;
                remainingShots--;
                if (copyingShot)
                {
                    //Give the User a Letter
                    GiveLetter();
                    if (userLetters.Length == 5)
                    {
                        //Inform the user they lost, update the database accordingly, and end.
                        GameOver();
                    }
                    else
                    {
                        NotifyLetterReceived(userLetters);
                        NetworkController.AddLetter(player, userLetters.Length);
                        //Make panel tell them they got a letter
                        //They hit okay, their shots reset to 3 and now they're taking a shot
                        UpdateLetterText();
                        if (turnCount >= 50)
                            CheckForTieBreaker();
                    }
                }
                else
                {
                    NetworkController.SendMissedShot(opponent, turnCount);
                    NotifyOwnFailure();
                }
                    
            }
        }
        if (shotText != null)
            TextFade();

        if (newBasketball.transform.position != ballStart || isReplaying)
            ballInPlay = true;
        else
            ballInPlay = false;

        if (KillBall())
        {
            remainingShots--;
            CreateBall();
        }
        if (copyingShot)
        {
            replayShotButton.GetComponent<Image>().color = Color.white;
            replayShotButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            replayShotButton.GetComponent<Image>().color = Color.gray;
            replayShotButton.GetComponent<Button>().enabled = false;
        }
        if (replayComplete)
            EndReplay();

        if (ballInPlay)
            DisableButtons();
        else
            EnableButtons();

        if (notificationPanel.activeSelf == true)
            DisableButtons();
        else
            EnableButtons();
        if (isReplaying)
            DisableButtons();
        else
            EnableButtons();

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

    public static void SetMadeShot(float score, bool isSwish)
    {
        if (!isReplaying)
        {
            MadeShot = true;
            shotScore = score;
            swish = isSwish;
            Debug.Log("Made Shot!");
        }
        else
            replayComplete = true;
       
    }

    private void ShotMade()
    {
        shotText = Instantiate(shotTextSwish, textStartPos, Quaternion.Euler(-10,90,0));
        if (swish)
            shotText.GetComponent<TextMesh>().text = "Swish! - " + shotScore.ToString();
        else
            shotText.GetComponent<TextMesh>().text = "Made it! - " + shotScore.ToString();
        MadeShot = false;
        if (takingShot)
        {
            userScore += shotScore;
            Vector3 ballVelocity = ThrowScript.shotVelocity;
            NetworkController.SendMadeShot(player, opponent, userScore, ballStart.x, ballStart.z, turnCount, ballVelocity.x, ballVelocity.y, ballVelocity.z);
            NotifyOwnSuccess();
        }
        else
            NotifyCopySuccess();
       
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

    private void StoreShotData()
    {
        replayVelocity = ThrowScript.shotVelocity;
        usedObstacles = ScoreAccumulator.GetUsedObstacles();
    }

    private void LoadShotData()
    {
        NetworkData.ShotData shotData = NetworkController.LoadLastShot();
        //These details only change if the last shot was made or not
        if (shotData.shotMade == 0)
        {
            takingShot = true;
            copyingShot = false;
        }
        else
        {
            takingShot = false;
            copyingShot = true;
            ballStart.x = shotData.locationX;
            //The ball will always be vertically at 1
            ballStart.y = 1f;
            ballStart.z = shotData.locationZ;
            replayVelocity.x = shotData.ballX;
            replayVelocity.y = shotData.ballY;
            replayVelocity.z = shotData.ballZ;
        }
        //The following details must change everytime
        user = shotData.currentTurnOwner;
        if (user == shotData.player1)
        {
            userScore = shotData.p1score;
            opponent = shotData.player2;
            player = 1;
            PopulateLetters(shotData.p1letters, shotData.p2letters);
            turnCount++;
        }
        else
        {
            userScore = shotData.p2score;
            opponent = shotData.player1;
            player = 2;
            PopulateLetters(shotData.p1letters, shotData.p2letters);
        }
        NetworkData.UserData userData = NetworkController.FetchUserData(user, opponent);
        userName = userData.userName;
        opponentName = userData.opponentName;
        turnCount = shotData.turnNo;
        UpdateLetterText();
        if (copyingShot)
            NotifyCopyShot();
        else
            NotifyMissedShot();
    }

    #region "Replay Code"

    public void ShowInitialReplay()
    {
        ReplayShot();
        //Eventually popup list of what happened
    }

    public void ReplayShot()
    {
        isReplaying = true;
        remainingShotsText.GetComponent<Text>().enabled = false;
        newBasketball.SetActive(false);
        replayText.GetComponent<Text>().enabled = true;
        replayBall = Instantiate(Basketball, ballStart, Quaternion.identity);
        StartCoroutine(DelayReplay());

    }

    IEnumerator DelayReplay()
    {
        yield return new WaitForSeconds(1);
        replayBall.GetComponent<Rigidbody>().velocity = replayVelocity;
        replayBall.GetComponent<Rigidbody>().useGravity = true;
    }

    public void EndReplay()
    {
        isReplaying = false;
        replayComplete = false;
        Destroy(replayBall);
        remainingShotsText.GetComponent<Text>().enabled = true;
        newBasketball.SetActive(true);
        replayText.GetComponent<Text>().enabled = false;
        if (startReplay)
            NotifyShotRequirements();
    }

    #endregion

    #region "Notification Code"
    public void DismissNotificationPanel()
    {
        notificationPanel.SetActive(false);
        if (startReplay)
            ShowInitialReplay();

    }

    private void NotifyLetterReceived(string word)
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = "\t\t\t    Too bad!\n\t\t  You received: '" + word.Substring(word.Length - 1) + "'\nNow, create your own shot!";
        copyingShot = false;
        takingShot = true;
        remainingShots = 3;
        UpdateRSText();
    }

    private void NotifyShotRequirements()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = " Now, recreate their shot.\nYou must do the following:";
        startReplay = false;
    }

    private void NotifyCopyShot()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = opponentName + " made their shot!";
        startReplay = true;
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
    }

    private void NotifyMissedShot()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = opponentName + " missed their shot!\n\t\t   Create your own!";
        //PLACEHOLDER BALLSTART CODE
        //We need to add shotSelection Logic here after press okay
        ballStart.x = 3.0f;
        ballStart.y = 1.0f;
        ballStart.z = 0f;
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
    }

    private void NotifyCopySuccess()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = "\t\t\tWay to go!\nNow, create your own shot!";
        copyingShot = false;
        takingShot = true;
        remainingShots = 3;
        UpdateRSText();
    }

    private void NotifyOwnSuccess()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = "\t\t\tGood job!\n\t\tShot Score: " + shotScore.ToString();
        StartCoroutine(KillApplication());
    }

    private void NotifyOwnFailure()
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = "\t\t\t   Too bad!";
        StartCoroutine(KillApplication());
    }

    #endregion

    #region "Letter Handling"

    public void PopulateLetters(int u, int o)
    {
        switch (u)
        {
            case 0:
                userLetters = "";
                break;
            case 1:
                userLetters = "S";
                break;
            case 2:
                userLetters = "SW";
                break;
            case 3:
                userLetters = "SWI";
                break;
            case 4:
                userLetters = "SWIS";
                break;
        }
        switch (o)
        {
            case 0:
                opponentLetters = "";
                break;
            case 1:
                opponentLetters = "S";
                break;
            case 2:
                opponentLetters = "SW";
                break;
            case 3:
                opponentLetters = "SWI";
                break;
            case 4:
                opponentLetters = "SWIS";
                break;
        }
    }

    public void UpdateLetterText()
    {
        userLettersText.text = "You: \t\t\t " + userLetters;
        for (int i = userLetters.Length; i < 5; i++)
            userLettersText.text += " _";  
        opponentLettersText.text = "Opponent:\t " + opponentLetters;
        for (int i = opponentLetters.Length; i < 5; i++)
            opponentLettersText.text += " _";
    }

    public void GiveLetter()
    {
        switch (userLetters.Length)
        {
            case 0:
                userLetters = "S";
                break;
            case 1:
                userLetters = "SW";
                break;
            case 2:
                userLetters = "SWI";
                break;
            case 3:
                userLetters = "SWIS";
                break;
            case 4:
                userLetters = "SWISH";
                break;
        }
    }

    #endregion

    public void DisableButtons()
    {
        ThrowScript.isThrown = true;
        replayShotButton.GetComponent<Image>().color = Color.gray;
        replayShotButton.GetComponent<Button>().enabled = false;
        obstacleMenuButton.GetComponent<Image>().color = Color.gray;
        obstacleMenuButton.GetComponent<Button>().enabled = false;
    }
    public void EnableButtons()
    {
        if (copyingShot)
        {
            replayShotButton.GetComponent<Image>().color = Color.white;
            replayShotButton.GetComponent<Button>().enabled = true;
        }
        ThrowScript.isThrown = false;
        obstacleMenuButton.GetComponent<Image>().color = Color.white;
        obstacleMenuButton.GetComponent<Button>().enabled = true;
    }

    private void CheckForTieBreaker()
    {

    }

    private void GameOver()
    {

    }

    IEnumerator KillApplication()
    {
        yield return new WaitForSeconds(2);
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
