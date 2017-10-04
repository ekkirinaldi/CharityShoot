using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ball;
    public GameObject playerCamera;

    public float ballDistance = 2f;
    public float ballThrowingForce = 5f;

    // Use this for initialization
    void Start()
    {
        ball.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q)) && GameObject.Find("HighScore").GetComponent<HighScore>().clickAccess == true)
        {
            GameObject clon = Instantiate(ball, playerCamera.transform.position, Quaternion.identity) as GameObject;
            clon.GetComponent<Rigidbody>().useGravity = true;
            clon.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * ballThrowingForce);
            Destroy(clon, 2);
        }
    }
}
