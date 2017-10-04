using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    private string connectionString;

    public GameObject scoreUI;
    public GameObject nameDialog;
    public GameObject okButton;
    public GameObject gameOverScreen;

    public Button testConnectionButton;

    public Text canvasPlayerName;
    public Text timerLabel;

    public InputField enterName;
    public Text gameOverText;

    public bool clickAccess;

    public float timeLimit;
    public bool timeStart = false;

    public string[] items;
    string InsertData = "http://ekkirinaldi.xyz/Insertscore.php";
    string UpdateData = "http://ekkirinaldi.xyz/Updatescore.php";

    // Use this for initialization
    void Start () {
        scoreUI.SetActive(false);
        clickAccess = false;
        gameOverScreen.SetActive(false);
        testConnectionButton.onClick.AddListener(TestConnection);
    }
	
	// Update is called once per frame
	void Update () {
        if (timeStart == true)
        {
            timeLimit -= Time.deltaTime;
            double minutes = Math.Floor(timeLimit / 60); //Divide the guiTime by sixty to get the minutes.
            double seconds = Math.Floor(timeLimit % 60);//Use the euclidean division for the seconds.
            double fraction = (timeLimit * 100) % 100;

            timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);

            if (timeLimit < 0)
            {
                gameOverScreen.SetActive(true);
                gameOverText.text = "Sesi permainan selesai\n \nTerimakasih atas donasi yang telah Anda berikan";
                //gameOverText.text = "Permainan selesai\n \nTerimakasih atas donasi " + GameObject.Find("ScoreArea").GetComponent<ScoreArea>().count + " coin yang telah Anda berikan";
                clickAccess = false;
                timerLabel.text = string.Format("00 : 00 : 000", minutes, seconds, fraction);
                //Invoke("CancelsInvoke",0);
                //Invoke("UpdateScore", 2);
                //Invoke("ShowScores", 3);
                Invoke("CloseApp", 15);
            }
        }
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void UpdateLastScore()
    {
        UpdateScore();
    }

    public void CancelsInvoke()
    {
        CancelInvoke();
    }

    public void TestConnection()
    {
        int randomName = UnityEngine.Random.Range(0, 100000);
        InsertScore("Player"+randomName);
    }

    public void EnterName()
    {
        if (enterName.text != string.Empty)
        {
            InsertScore(enterName.text);
            canvasPlayerName.text = enterName.text;

            clickAccess = true;
            scoreUI.SetActive(true);
            Destroy(nameDialog);
            timeStart = true;

            InvokeRepeating("UpdateScore", 0, 1);
            ShowScores();
        }
    }

    public void InsertScore(string name )
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", 0);

        WWW www = new WWW(InsertData, form);
    }

    public void UpdateScore()
    {
        UpdateScores(GameObject.Find("ScoreArea").GetComponent<ScoreArea>().count);
    }

    public void UpdateScores(int score)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("name", canvasPlayerName.text);
        form1.AddField("score", score);

        WWW www1 = new WWW(UpdateData, form1);
        //ShowScores();
    }

    public void ShowScores()
    {
        GameObject.Find("HighScore").GetComponent<DataLoader>().ShowScore();
    }
}
