using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class GPGSManager : MonoBehaviour
{
#if UNITY_ANDROID
    public static GPGSManager instance;


    private string leaderBoardID = "CgkI6tuaxMccEAIQAQ";


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
        LogIn();
    }






    //초기화 
    public void Init()
    {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       //.EnableSavedGames()
       // registers a callback to handle game invitations received while the game is not running.
       //.WithInvitationDelegate(< callback method >)
       // registers a callback for turn based match notifications received while the
       // game is not running.
       //.WithMatchDelegate(< callback method >)
       // requests the email address of the player be available.
       // Will bring up a prompt for consent.
       //.RequestEmail()
       // requests a server auth code be generated so it can be passed to an
       //  associated back end server application and exchanged for an OAuth token.
       //.RequestServerAuthCode(false)
       // requests an ID token be generated.  This OAuth token can be used to
       //  identify the player to other services such as Firebase.
       //.RequestIdToken()
       .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
}

    //로그인 
    public void LogIn()
    {

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            // handle results
        });
    }
    //리더보드로 점수 전송
    public void SendBoardScore(float score)
    {
        Social.ReportScore((long)score, leaderBoardID, (bool success) => {
            // handle success or failure
            if(success)
            {
                ShowLeaderBoard();
            }
        });
    }
    //리더보드 표시
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    
#endif
}