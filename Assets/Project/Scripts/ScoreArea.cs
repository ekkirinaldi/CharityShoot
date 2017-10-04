using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreArea : MonoBehaviour {

    public int count;

    public Text countText;

    public void Start()
    {
        count = 0;
        SetCountText();
    }

    public void OnTriggerEnter (Collider otherCollider)
    {
        if (otherCollider.GetComponent<Ball>() != null)
        {
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }

}
