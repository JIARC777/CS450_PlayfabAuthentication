using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Events;


public class LoginRegister : MonoBehaviour
{
    public UnityEvent onLoggedIn;

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public TextMeshProUGUI displayText;

    [HideInInspector]
    public string playFabId;
    public static LoginRegister instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void OnRegister()
    {
        RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest { 
            Username = usernameInput.text, 
            DisplayName = usernameInput.text, 
            Password = passwordInput.text, 
            RequireBothUsernameAndEmail = false 
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, 
            result =>
            {
                Debug.Log(result.PlayFabId);
                SetDisplayText("Account Created", Color.green);
            },
            error => {
                Debug.Log(error.ErrorMessage);
                SetDisplayText("Something went wrong: try a longer password", Color.red);
            }
        );
        
    }

    public void OnLoginButton() 
    {
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest 
        { 
            Username = usernameInput.text, 
            Password = passwordInput.text 
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest,
            result =>
            {
                SetDisplayText("Logged in as: " + result.PlayFabId, Color.green);

                if (onLoggedIn != null)
                    onLoggedIn.Invoke();
                playFabId = result.PlayFabId;
            },
            error => SetDisplayText("Incorrect Login", Color.red)
        );

    }

    void SetDisplayText(string text, Color color)
    {
        displayText.text = text;
        displayText.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
