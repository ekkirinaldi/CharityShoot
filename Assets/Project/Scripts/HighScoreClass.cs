using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

class HighScoreClass:IComparable<HighScoreClass>
{
    public int Score { get; set; }
    public string PlayerName { get; set; }
    public DateTime Date { get; set; }

    public HighScoreClass(string name, int score, DateTime date)
    {
        this.Score = score;
        this.PlayerName = name;
        this.Date = date; 
    }

    public int CompareTo(HighScoreClass other)
    {
        if(other.Score < this.Score)
        {
            return -1;
        }

        else if(other.Score > this.Score)
        {
            return 1;
        }

        else if (other.Date > this.Date)
        {
            return -1;
        }

        else if (other.Date > this.Date)
        {
            return 1;
        }

        return 0;
    }
}
