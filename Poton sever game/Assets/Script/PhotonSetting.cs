using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab.ClientModels;
using PlayFab;

public class PhotonSetting : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        NotificationManager.NotificationWindow
        (
            "Login Failed",
                "There are currently no acount registred on the server." +
                "\n\n please enter your ID and password correctly."

        );
    }
    
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        NotificationManager.NotificationWindow
        (
            "MemberShip Successful",
            "Congratulations on becoming a member." +
            "\n\n Your email acocount has been registered on the game server."

        );
    }
    
    public void SignUpFailure(PlayFabError error)
    {
        NotificationManager.NotificationWindow
            (
             "Failed to Sign Up",
            "MemberShip registration failed due to a current server error"+
            "\n\n Please try to register as a memeber again"
            );
    }

    public void SignUp()
    {
        //plya팹 사이트에 정보를 넘겨서 유저 등록 하는 것 
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text
        
         };

        PlayFabClientAPI.RegisterPlayFabUser
            (
            request,
            SignUpSuccess,
            SignUpFailure
            );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
            (
            request,
            LoginSuccess,
            LoginFailure
            );
    }

}
