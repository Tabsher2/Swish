using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AppSignUp : MonoBehaviour {

    public Text email;
    public Text username;
    public Text pw;
    public Text dob;
    public Text errorText;

    public Button SignUp;
    public GameObject signUpPanel;
    public GameObject loginPanel;
    public GameObject successText;

    private void Start()
    {
        SignUp.onClick.AddListener(loginClicked);
    }

    void loginClicked()
    {
        if(username.text.Length <= 5)
        {
            errorText.text = "Username must be at least six characters";
            return;
        }
        else if(!IsEmail(email.text))
        {
            errorText.text = "Email is not valid";
            return;
        }
        else if (pw.text.Length < 6)
        {
            errorText.text = "Password must be at least 6 characters";
            return;
        }
        else
        {
            errorText.text = "";
        }
        string added = NetworkController.AddUser(username.text, email.text, pw.text, dob.text);
        if (added.Length == 0)//user not created
        {
            errorText.text = "Account not created.\nPlease check each field.";
        }
        else//user created
        {

            Debug.Log(added);

            successText.SetActive(true);
            loginPanel.SetActive(true);
            signUpPanel.SetActive(false);

        }

    }

    public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address.
    /// </summary>
    /// <param name="email">Parameter-string that contains an E-Mail address.</param>
    /// <returns>True, wenn Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public static bool IsEmail(string email)
    {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
}

