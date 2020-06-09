using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataManager : MonoBehaviour
{
    public static Text userData;

    public static int lives;
    public static int health;
    public static int gems;

    void Start()
    {
        lives = 3;
        health = 100;
        gems = 0;

        userData = GetComponent<Text>();
    }

    void Update()
    {
        userData.text = "Lives: " + lives + '\n' + "Gems: " + gems + '\n' + "Health: " + health;
    }
}
