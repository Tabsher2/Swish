using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameInfoForLoad;


public class GameController : MonoBehaviour
{
    private static bool MadeShot = false;

    public GameObject Basketball;
    private GameObject newBasketball;
    public GameObject obstacleMenu;
    public GameObject obstacleMenuButton;
    public GameObject PreviousToken;
    private GameObject previousTokenInstance;

    private float gameTime;

    //Multiplayer info
    public int gameID;
    private bool takingShot;
    private bool copyingShot;
    private int user;
    private int opponent;
    private int player;
    private int userScore;
    private int opponentScore;
    private string userLetters = "";
    private string opponentLetters = "";
    private float locationX;
    private float locationZ;
    private int turnCount;
    private string userName;
    private string opponentName;
    private int gameLength;
    Camera mainCam;

    //Load Game info
    public GameObject gameInfo;

    //UI text
    private GameObject shotText;
    public GameObject shotTextSwish;
    public Text remainingShotsText;
    public Text userLettersText;
    public Text opponentLettersText;

    //Notification Variables
    public GameObject notificationPanel;
    public Text notificationTitle;
    public Text notificationMessage;

    //Notification Listeners
    private static bool startReplay = false;
    private static bool transitionToShotSelection = false;
    private static bool selectingShot = false;
    public static bool slideCameraUp = false;
    public static bool spotSelected = false;
    private static bool turnCompleteMade = false;
    private static bool turnCompleteMissed = false;
    private static bool receivedLetter = false;
    private static bool understand = false;
    private static bool acknowledgeEnemy = false;
    public static bool disableThrow = false;
    private static bool gameLoss = false;
    private static bool gameWin = false;
    private static bool gameTie = false;
    private static bool sendVictory = false;
    private static bool maxRounds = false;


    //Spawn Locations
    public static Vector3 ballStart;
    public static Vector3 textStartPos = new Vector3(7f, 3f, 1f);

    //Ball Variables
    private static int shotScore = 0;
    private static bool swish = false;
    private int remainingShots = 3;
    private bool ballInPlay = false;

    //Replay variables
    private static Vector3 replayVelocity;
    public Text replayText;
    private GameObject replayBall;
    private static List<ScoreAccumulator.Obstacle> finalObstacles = new List<ScoreAccumulator.Obstacle>();
    private static List<ScoreAccumulator.Obstacle> placedObstacles = new List<ScoreAccumulator.Obstacle>();
    public static List<ScoreAccumulator.Obstacle> mapObstacles = new List<ScoreAccumulator.Obstacle>();
    public static List<ScoreAccumulator.Obstacle> usedObstacles = new List<ScoreAccumulator.Obstacle>();
    private static bool isReplaying = false;
    private static bool replayComplete = false;
    public GameObject replayShotButton;

    //Obstacle variables
    static Vector3 birdView = new Vector3(0, 20, 0);
    static Vector3 satellite = new Vector3(90, 0, 270);
    static Vector3 hoopLocation = new Vector3(8.5f, 2.5f, 0);




    private void Awake()
    {
        mainCam = Camera.main;
        replayText.GetComponent<Text>().enabled = false;
        //gameID = GameObject.Find("GameInfo").GetComponent<GameInfo>().gameID;
        gameID = 5;
    }
    // Use this for initialization
    void Start()
    {
        LoadShotData(gameID);
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
                UpdateRSText();
                disableThrow = true;
                remainingShots--;
                if (copyingShot)
                    MissedCopyShot();
                else
                    NotifyOwnFailure();
            }
        }

        if (acknowledgeEnemy)
        {
            acknowledgeEnemy = false;
            UpdateLetterText();
            if (copyingShot)
                NotifyCopyShot();
            else
                NotifyMissedShot();
        }

        if (shotText != null)
            TextFade();
        if (IsBallExisting())
        {
            if (selectingShot || !IsBallAtStart() || isReplaying)
                ballInPlay = true;
            else
                ballInPlay = false;
        }

        if (ThrowScript.isThrown && IsBallExisting() && KillBall() && !isReplaying)
        {
            if (remainingShots == 3)
                NetworkController.UpdateShotStreak(user, 0);
            remainingShots--;
            CreateBall();
        }

        DetermineButtons();

        if (replayComplete)
            EndReplay();

        if (slideCameraUp)
            CameraController.MoveToShotSelection(Camera.main.transform.position, Camera.main.transform.eulerAngles);

        if (spotSelected)
        {
            ballStart = LocationSelector.selectedLocation;
            remainingShots = 3;
            UpdateRSText();
            newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
            spotSelected = false;
            selectingShot = false;
            LocationSelector.allowSelection = false;

            //Re-enable UI
            obstacleMenuButton.SetActive(true);
            remainingShotsText.enabled = true;
            userLettersText.enabled = true;
            opponentLettersText.enabled = true;
        }
    }

    public void CreateBall()
    {
        if (remainingShots > 0)
        {
            ResetBall();
            newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
            UpdateRSText();
        }
    }

    private void ResetBall()
    {
        Destroy(newBasketball);
        BottomNetTrigger.ResetTrigger();
        TopNetTrigger.ResetTrigger();
        ScoreAccumulator.ResetScore();
        ThrowScript.ResetThrow();
        CheckBallTimeout.ResetDeadBall();
        BallCollision.ResetBallCounter();
        shotScore = 0;
    }

    public static void SetMadeShot(int score, bool isSwish)
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
        if (usedObstacles.Count > 0 && copyingShot)
        {
            if (ScoreAccumulator.hitObstacles.Count == usedObstacles.Count)
                return;
            for (int i = 0; i < ScoreAccumulator.hitObstacles.Count; i++)
            {
                if (usedObstacles[i].locationX == ScoreAccumulator.hitObstacles[i].locationX && usedObstacles[i].locationZ == ScoreAccumulator.hitObstacles[i].locationZ)
                    continue;
                else
                {
                    if (remainingShots == 0)
                    {
                        UpdateRSText();
                        disableThrow = true;
                        remainingShots--;
                        MissedCopyShot();
                    }
                    return;
                }
            }
        }
        if (remainingShots == 3)
            NetworkController.UpdateShotStreak(user, 1);

        remainingShots = -1;
        shotText = Instantiate(shotTextSwish, textStartPos, Quaternion.Euler(-10, 90, 0));
        if (swish)
        {
            if (!copyingShot)
                shotText.GetComponent<TextMesh>().text = "Swish! - " + shotScore.ToString();
            else
                shotText.GetComponent<TextMesh>().text = "Swish!";

            NetworkController.AddSwish(user);
        }
        else
        {
            if (!copyingShot)
                shotText.GetComponent<TextMesh>().text = "Made it! - " + shotScore.ToString();
            else
                shotText.GetComponent<TextMesh>().text = "Made it!";
        }
        MadeShot = false;
        if (takingShot)
        {
            StoreObstacleData();
            userScore += shotScore;
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
        remainingShotsText.text = "SHOTS LEFT: " + remainingShots.ToString() + "/3";
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
        obstacleMenuButton.SetActive(false);
        mainCam.transform.position = birdView;
        mainCam.transform.eulerAngles = satellite;
        previousTokenInstance = Instantiate(PreviousToken, new Vector3(ballStart.x, 0.08f, ballStart.z), Quaternion.identity);
    }

    public void CloseObstacleMenu()
    {
        obstacleMenu.SetActive(false);
        newBasketball.SetActive(true);
        obstacleMenuButton.SetActive(true);
        mainCam.transform.position = LocationSelector.cameraLocation;
        mainCam.transform.eulerAngles = LocationSelector.cameraAngle;
        mainCam.transform.LookAt(hoopLocation);
        Destroy(previousTokenInstance);
    }

    private void StoreObstacleData()
    {
        finalObstacles = ScoreAccumulator.GetUsedObstacles();
        placedObstacles = ObstacleController.GetPlacedObstacles();
        if (placedObstacles.Count > 0)
            NetworkController.SendObstacles(finalObstacles, placedObstacles, gameID);
    }

    private void LoadShotData(int gameID)
    {
        NetworkData.ShotData shotData = NetworkController.LoadLastShot(gameID);
        //These details only change if the last shot was made or not
        if (shotData.shotMade == 0)
        {
            takingShot = true;
            copyingShot = false;
            obstacleMenuButton.SetActive(true);
            replayShotButton.SetActive(false);
        }
        else
        {
            takingShot = false;
            copyingShot = true;
            NetworkController.LoadObstacles(gameID);
            ObstacleController.loadObstacles = true;
            obstacleMenuButton.SetActive(false);
            replayShotButton.SetActive(true);
            ballStart.x = shotData.locationX;
            //The ball will always be vertically at 0.7
            ballStart.y = 0.7f;
            ballStart.z = shotData.locationZ;
            Vector3 hoopLocation = new Vector3(8.5f, 2.5f, 0);
            float distance = CalculateDistance(hoopLocation, ballStart);

            float yAngle = Mathf.Atan(1.5f / (distance + 1));

            float angle = Mathf.Acos((hoopLocation.x - ballStart.x) / distance);
            float cameraX = Mathf.Cos(angle) * (distance + 1);
            float cameraZ = Mathf.Sin(angle) * (distance + 1);
            float cameraY = 1 - Mathf.Tan(yAngle);
            cameraX = hoopLocation.x - cameraX;
            if (ballStart.z < 0)
            {
                cameraZ *= -1;
                angle *= -1;
            }
            mainCam.transform.position = new Vector3(cameraX, cameraY, cameraZ);
            angle *= Mathf.Rad2Deg;
            yAngle *= (-1 * Mathf.Rad2Deg);
            mainCam.transform.LookAt(hoopLocation);

            replayVelocity.x = shotData.ballX;
            replayVelocity.y = shotData.ballY;
            replayVelocity.z = shotData.ballZ;
        }
        //The following details must change everytime
        user = shotData.currentTurnOwner;
        turnCount = shotData.turnNo;
        gameLength = shotData.gameLength;
        if (user == shotData.player1)
        {
            userScore = shotData.p1score;
            opponentScore = shotData.p2score;
            opponent = shotData.player2;
            player = 1;
            switch (gameLength)
            {
                case 3:
                    Populate3Letters(shotData.p1letters, shotData.p2letters);
                    break;
                case 5:
                    Populate5Letters(shotData.p1letters, shotData.p2letters);
                    break;
                case 7:
                    Populate7Letters(shotData.p1letters, shotData.p2letters);
                    break;
            }
            turnCount++;
        }
        else
        {
            userScore = shotData.p2score;
            opponentScore = shotData.p1score;
            opponent = shotData.player1;
            player = 2;
            switch (gameLength)
            {
                case 3:
                    Populate3Letters(shotData.p2letters, shotData.p1letters);
                    break;
                case 5:
                    Populate5Letters(shotData.p2letters, shotData.p1letters);
                    break;
                case 7:
                    Populate7Letters(shotData.p2letters, shotData.p1letters);
                    break;
            }
        }
        UpdateLetterText();
        NetworkData.UserData userData = NetworkController.FetchUserData(user, opponent);
        userName = userData.userName;
        opponentName = userData.opponentName;
        switch (shotData.shotStatus)
        {
            case 0:
                acknowledgeEnemy = true;
                break;
            case 1:
                NotifyEnemySuccess();
                break;
            case 2:
                NotifyEnemyFailure();
                break;
        }

        if (player == 1 && !copyingShot && turnCount >= 50)
            CheckForTieBreaker();

        if (player == 2 && turnCount >= 50)
            CheckForTieBreaker();
    }

    #region "Replay Code"

    IEnumerator ShowInitialReplay()
    {
        yield return new WaitForSeconds(1);
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
            startReplay = false;
    }

    #endregion

    #region "Notification Code"
    public void DismissNotificationPanel()
    {
        if (receivedLetter)
        {
            receivedLetter = false;
            DestroyObstacle.DeleteAllObstacles();
            StartCoroutine(WaitToSend());
        }
        notificationPanel.SetActive(false);
        if (startReplay)
            StartCoroutine(ShowInitialReplay());
        else if (transitionToShotSelection)
            ActivateShotSelection();
        else if (turnCompleteMade)
        {
            turnCompleteMade = false;
            ReturnToMenu();
        }
        else if (turnCompleteMissed)
        {
            turnCompleteMissed = false;
            NetworkController.SendMissedShot(opponent, turnCount, gameID);
            ReturnToMenu();
        }
        else if (understand)
        {
            understand = false;
            acknowledgeEnemy = true;
        }
        else if (gameLoss)
        {
            gameLoss = false;
            GameOver(0);
        }
        else if (gameWin)
        {
            gameWin = false;
            NotifyVictory();
        }
        else if (sendVictory)
        {
            sendVictory = false;
            GameOver(1);
        }
        else if (gameTie)
        {
            gameTie = false;
            GameOver(2);
        }
    }

    private void NotifyLetterReceived(string word)
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "OH NO!!";
        notificationMessage.text = "You Missed!\nYou received the letter: '" + word.Substring(word.Length - 1) + "'\nNow create your own shot!";
        copyingShot = false;
        takingShot = true;
        ResetBall();
        receivedLetter = true;
        transitionToShotSelection = true;
    }

    private void NotifyCopyShot()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "UH-OH!";
        notificationMessage.text = opponentName + "\nmade their shot! Copy it\nto avoid getting a letter!";
        startReplay = true;
        newBasketball = Instantiate(Basketball, ballStart, Quaternion.identity);
    }

    private void NotifyMissedShot()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "NOW'S YOUR CHANCE!";
        notificationMessage.text = opponentName + "\nmissed their shot!\nNow create your own shot!";
        //Update Copy Result to a 0 for a don't care value, since you won't be copying a shot
        NetworkController.UpdateCopyResult(0, gameID);
        transitionToShotSelection = true;
    }

    private void NotifyCopySuccess()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "YOU DID IT!";
        notificationMessage.text = "You copied their shot!\nYou avoided gaining another letter!\nNow create your own shot!";
        //Update Copy result to a 1 since you 'won' their shot
        NetworkController.UpdateCopyResult(1, gameID);
        if (mapObstacles.Count > 0)
        {
            NetworkController.ClearObstacles(gameID);
            mapObstacles.Clear();
            usedObstacles.Clear();
            DestroyObstacle.DeleteAllObstacles();
        }
        copyingShot = false;
        takingShot = true;
        ResetBall();
        transitionToShotSelection = true;
    }

    private void NotifyOwnSuccess()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "NICE SHOT!";
        notificationMessage.text = "You made it!\nShot Score: " + shotScore.ToString();
        NetworkController.SendMadeShot(player, opponent, userScore, ballStart.x, ballStart.z, turnCount, ThrowScript.shotVelocity.x, ThrowScript.shotVelocity.y, ThrowScript.shotVelocity.z, gameID);
        ResetBall();
        turnCompleteMade = true;
    }

    private void NotifyOwnFailure()
    {
        notificationPanel.SetActive(true);
        ResetBall();
        notificationTitle.text = "TOO BAD!";
        notificationMessage.text = "You missed your shot!\nBetter luck next time!";
        turnCompleteMissed = true;
    }

    private void NotifyEnemySuccess()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "TOUGH LUCK!";
        notificationMessage.text = opponentName + "\nmade your shot!\nTry a harder one next time!";
        understand = true;
    }

    private void NotifyEnemyFailure()
    {
        notificationPanel.SetActive(true);
        notificationTitle.text = "GOOD JOB!";
        notificationMessage.text = opponentName + "\nmissed your shot!\nThey received a letter!";
        if (opponentLetters.Length == gameLength)
            gameWin = true;
        else
            understand = true;
    }

    private void NotifyLoss()
    {
        notificationPanel.SetActive(true);
        int currency = (userScore / 500) + 1;
        if (maxRounds)
        {
            notificationTitle.text = "YOU LOST!";
            notificationMessage.text = "Max Turn Limit Reached!\nYour Score: " + userScore + "\nOpponent Score: " + opponentScore + "\n\n You earned " + currency + " coins!";
            maxRounds = false;
        }
        else
        {
            notificationTitle.text = "YOU LOST!";
            notificationMessage.text = "Your Score: " + userScore + "\nOpponent Score: " + opponentScore + "\n\n You earned " + currency + " coins!";
        }
        gameLoss = true;
    }

    private void NotifyVictory()
    {
        notificationPanel.SetActive(true);
        int currency = (userScore / 500) + 5;
        if (maxRounds)
        {
            notificationTitle.text = "YOU WON!";
            notificationMessage.text = "Max Turn Limit Reached!\nYour Score: " + userScore + "\nOpponent Score: " + opponentScore + "\n\n You earned " + currency + " coins!";
            maxRounds = false;
        }
        else
        {
            notificationTitle.text = "YOU WON!";
            notificationMessage.text = "\nYour Score: " + userScore + "\nOpponent Score: " + opponentScore + "\n\n You earned " + currency + " coins!";
        }
        sendVictory = true;
    }

    private void NotifyTie()
    {
        notificationPanel.SetActive(true);
        int currency = (userScore / 500) + 5;
        notificationTitle.text = "YOU TIED!";
        notificationMessage.text = "Max Turn Limit Reached!\nYour Score: " + userScore + "\nOpponent Score: " + opponentScore + "\n\n You earned " + currency + " coins!";
        gameTie = true;
    }

    #endregion

    #region "Letter Handling"

    public void Populate5Letters(int u, int o)
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
            case 5:
                userLetters = "SWISH";
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
            case 5:
                opponentLetters = "SWISH";
                break;
        }
    }

    public void Populate3Letters(int u, int o)
    {
        switch (u)
        {
            case 0:
                userLetters = "";
                break;
            case 1:
                userLetters = "N";
                break;
            case 2:
                userLetters = "NE";
                break;
            case 3:
                userLetters = "NET";
                break;
        }
        switch (o)
        {
            case 0:
                opponentLetters = "";
                break;
            case 1:
                opponentLetters = "N";
                break;
            case 2:
                opponentLetters = "NE";
                break;
            case 3:
                opponentLetters = "NET";
                break;
        }
    }

    public void Populate7Letters(int u, int o)
    {
        switch (u)
        {
            case 0:
                userLetters = "";
                break;
            case 1:
                userLetters = "A";
                break;
            case 2:
                userLetters = "AI";
                break;
            case 3:
                userLetters = "AIR";
                break;
            case 4:
                userLetters = "AIRB";
                break;
            case 5:
                userLetters = "AIRBA";
                break;
            case 6:
                userLetters = "AIRBAL";
                break;
            case 7:
                userLetters = "AIRBALL";
                break;
        }
        switch (o)
        {
            case 0:
                opponentLetters = "";
                break;
            case 1:
                opponentLetters = "A";
                break;
            case 2:
                opponentLetters = "AI";
                break;
            case 3:
                opponentLetters = "AIR";
                break;
            case 4:
                opponentLetters = "AIRB";
                break;
            case 5:
                opponentLetters = "AIRBA";
                break;
            case 6:
                opponentLetters = "AIRBAL";
                break;
            case 7:
                opponentLetters = "AIRBALL";
                break;
        }
    }

    public void UpdateLetterText()
    {
        userLettersText.text = "YOU: " + userLetters;
        for (int i = userLetters.Length; i < gameLength; i++)
            userLettersText.text += "_ ";
        opponentLettersText.text = "OPPONENT: " + opponentLetters;
        for (int i = opponentLetters.Length; i < gameLength; i++)
            opponentLettersText.text += "_ ";
    }

    public void Give5Letter()
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

    public void Give3Letter()
    {
        switch (userLetters.Length)
        {
            case 0:
                userLetters = "N";
                break;
            case 1:
                userLetters = "NE";
                break;
            case 2:
                userLetters = "NET";
                break;
        }
    }

    public void Give7Letter()
    {
        switch (userLetters.Length)
        {
            case 0:
                userLetters = "A";
                break;
            case 1:
                userLetters = "AI";
                break;
            case 2:
                userLetters = "AIR";
                break;
            case 3:
                userLetters = "AIRB";
                break;
            case 4:
                userLetters = "AIRBA";
                break;
            case 5:
                userLetters = "AIRBAL";
                break;
            case 6:
                userLetters = "AIRBALL";
                break;
        }
    }

    #endregion

    #region "Button Handling" 

    public void DisableButtons()
    {
        //Prevent them from interfering with the ball
        disableThrow = true;
        //replayShotButton.GetComponent<Image>().color = Color.gray;
        replayShotButton.GetComponent<Button>().enabled = false;
        //obstacleMenuButton.GetComponent<Image>().color = Color.gray;
        obstacleMenuButton.GetComponent<Button>().enabled = false;
    }

    public void EnableButtons()
    {
        if (copyingShot)
        {
            //replayShotButton.GetComponent<Image>().color = Color.white;
            replayShotButton.GetComponent<Button>().enabled = true;
            //obstacleMenuButton.GetComponent<Image>().color = Color.gray;
            obstacleMenuButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            //obstacleMenuButton.GetComponent<Image>().color = Color.white;
            obstacleMenuButton.GetComponent<Button>().enabled = true;
        }

        disableThrow = false;
    }

    private void DetermineButtons()
    {

        if (copyingShot)
        {
            //replayShotButton.GetComponent<Image>().color = Color.white;
            replayShotButton.GetComponent<Button>().enabled = true;
            //obstacleMenuButton.GetComponent<Image>().color = Color.gray;
            obstacleMenuButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            //replayShotButton.GetComponent<Image>().color = Color.gray;
            replayShotButton.GetComponent<Button>().enabled = false;
        }

        if (isReplaying)
            DisableButtons();
        else if (notificationPanel.activeSelf)
            DisableButtons();
        else if (ballInPlay)
            DisableButtons();
        else if (LocationSelector.slideCameraDown)
            DisableButtons();
        else
            EnableButtons();
    }

    #endregion

    private void CheckForTieBreaker()
    {
        maxRounds = true;
        if (userScore > opponentScore)
            NotifyVictory();
        else if (userScore < opponentScore)
            NotifyLoss();
        else if (userScore == opponentScore)
            NotifyTie();
    }

    private void GameOver(int result)
    {
        NetworkController.UpdateGameEnd(user, userScore, result, userLetters.Length);
        NetworkController.SendMissedShot(opponent, turnCount, gameID);
        ReturnToMenu();
    }

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

    #region "Shot Selection"

    private void ActivateShotSelection()
    {
        transitionToShotSelection = false;
        selectingShot = true;
        replayShotButton.SetActive(false);
        obstacleMenuButton.SetActive(false);
        remainingShotsText.enabled = false;
        replayText.enabled = false;
        userLettersText.enabled = false;
        opponentLettersText.enabled = false;
        slideCameraUp = true;
        mainCam.transform.eulerAngles = new Vector3(0, 90, 0);
        AllowLocationSelection();
    }

    private void AllowLocationSelection()
    {
        LocationSelector.allowSelection = true;
    }

    public float CalculateDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.z - v2.z) * (v1.z - v2.z));
    }

    #endregion

    IEnumerator WaitToSend()
    {
        yield return new WaitForSeconds(2);
        NetworkController.AddLetter(player, userLetters.Length, 2, gameID);
        if (mapObstacles.Count > 0)
        {
            NetworkController.ClearObstacles(gameID);
            mapObstacles.Clear();
            usedObstacles.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        //If the user attempts the copy then quits, we count it as a failure, but it is still their turn to copy.
        if ((copyingShot && (remainingShots != -1) && (remainingShots != 3)) || (ThrowScript.isThrown && copyingShot))
        {
            NetworkController.AddLetter(player, userLetters.Length + 1, 2, gameID);
        }
        //If the user attempts their own shot and quits, we count it as a miss
        else if ((takingShot && (remainingShots != -1) && (remainingShots != 3)) || (ThrowScript.isThrown && takingShot))
        {
            NetworkController.SendMissedShot(opponent, turnCount, gameID);
        }
        //If they exit without hitting okay on their own shot
        else if (remainingShots == -1 && turnCompleteMissed)
        {
            NetworkController.SendMissedShot(opponent, turnCount, gameID);
        }
        //If they exit without hitting okay on copying a shot
        else if (remainingShots == -1 && receivedLetter)
        {
            NetworkController.AddLetter(player, userLetters.Length + 1, 2, gameID);
        }
        gameTime = Time.realtimeSinceStartup;
        gameTime /= (60 * 60);
        NetworkController.UpdateTime(user, gameTime);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void MissedCopyShot()
    {
        //Give the User a Letter
        switch (gameLength)
        {
            case 3:
                Give3Letter();
                break;
            case 5:
                Give5Letter();
                break;
            case 7:
                Give7Letter();
                break;
        }

        if (userLetters.Length == gameLength)
        {
            NetworkController.AddLetter(player, userLetters.Length, 2, gameID);
            //Inform the user they lost, update the database accordingly, and end.
            UpdateLetterText();
            NotifyLoss();
        }
        else
        {
            NotifyLetterReceived(userLetters);
            UpdateLetterText();
            if (turnCount >= 50)
                CheckForTieBreaker();
        }
    }

}
