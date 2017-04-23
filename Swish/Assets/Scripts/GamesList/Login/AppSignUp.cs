using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AppSignUp : MonoBehaviour {

    public InputField emailField;
    public InputField usernameField;
    public InputField pwField;
    public InputField dobField;
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

        if (!Regex.IsMatch(usernameField.text, @"^[a-z0-9]{3,18}$", RegexOptions.IgnoreCase))
        {
            errorText.text = "Usernames must be alphanumeric, contain no spaces, and be between 3 and 18 characters long.";
            return;
        }
        else if(!IsEmail(emailField.text))
        {
            errorText.text = "Email must be in the format: email@example.com.";
            return;
        }
        else if (!Regex.IsMatch(pwField.text, @"^(?=.*[0-9])(?=.*[a-z])[a-z0-9!@#$%^&*?]{6,18}$", RegexOptions.IgnoreCase))
        {
            errorText.text = "Passwords can contain a-z, A-Z, 0-9, and !@#$%^&*? Passwords must contain at least one letter and one number. Passwords must be between 6 and 18 characters long.";
            return;
        }
        else if (!Regex.IsMatch(dob.text, @"^\d{4}\-\d{2}\-\d{2}$", RegexOptions.IgnoreCase))
        {
            errorText.text = "Date of birth invalid";
            return;
        }
        else if (Regex.IsMatch(dobField.text, @"^\d{4}\-\d{2}\-\d{2}$", RegexOptions.IgnoreCase))
        {
            DateTime birthdate;
            try
            {
                birthdate = new DateTime(Int32.Parse(dobField.text.Substring(0, 4)), Int32.Parse(dobField.text.Substring(5, 2)), Int32.Parse(dobField.text.Substring(8, 2)));
            }
            catch
            {
                errorText.text = "Date of birth invalid";
                return;
            }
            
            // Save today's date.
            DateTime today = DateTime.UtcNow;
            // Calculate the age.
            int age = today.Year - birthdate.Year;
            // Do stuff with it.
            if (birthdate > today.AddYears(-age)) age--;
            if (age < 13)
            {
                errorText.text = "You must be 13 years or older to create an account.";
                return;
            }
            else
                errorText.text = "";

        }
        else
        {
            errorText.text = "";
        }
        string added = NetworkController.AddUser(usernameField.text, emailField.text, pwField.text, dobField.text);
        if (added.Length == 0)//user not created
        {
            errorText.text = "Account not created.\nPlease check each field.";
        }
        else//user created
        {

            Debug.Log(added);
            emailField.text = "";
            usernameField.text = "";
            pwField.text = "";
            dobField.text = "";
            errorText.text = "";

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

