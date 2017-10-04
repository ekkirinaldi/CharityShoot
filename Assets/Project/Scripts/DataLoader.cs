using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string[] allData;
    private List<HighScoreClass> highScores = new List<HighScoreClass>();
    public int topRanks;
    public GameObject scorePrefab;
    public Transform scoreParent;

    Coroutine co;

    // Use this for initialization

    public void ShowScore()
    {
        //StopCoroutine(CoReadData());
        StartCoroutine(CoReadData());
    }

    IEnumerator CoReadData()
    {
        while (true)
        {
            WWW itemsData = new WWW("http://ekkirinaldi.xyz/Scoreboard.php");
            yield return itemsData;
            string itemsDataString = itemsData.text;
            allData = itemsDataString.Split(';');          

            highScores.Clear();

            for (int i = 0; i < (allData.Length - 1); i++)
            {
                string[] data = allData[i].Split('|');

                if (data[0].StartsWith("Player"))
                {

                }
                else
                {
                    string name = data[0];
                    int score = Int32.Parse(data[1]);
                    DateTime date = Convert.ToDateTime(data[2]);

                    highScores.Add(new HighScoreClass(name, score, date));
                }
            }

            highScores.Sort();

            foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score"))
            {
                Destroy(score);
            }

            for (int i = 0; i < topRanks; i++)
            {
                if (i <= highScores.Count - 1)
                {
                    GameObject tmpObj = Instantiate(scorePrefab);

                    HighScoreClass tmpScore = highScores[i];

                    tmpObj.GetComponent<HighScoreScript>().SetScore(tmpScore.PlayerName, tmpScore.Score.ToString(), (i + 1).ToString());

                    tmpObj.transform.SetParent(scoreParent);
                }
            }
        }
    }

}
