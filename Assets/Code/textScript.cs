using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{

    [SerializeField] Text playerInfo = default;

    GameObject fox;
    playerScript fox_;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.text = "Lives: " + 3 + '\n' + "Gems: " + 0 + '\n' + "Health: " + 100;
        fox = GameObject.Find("fox_player");
        fox_ = fox.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.text = "Lives: " + fox_.Lives + '\n' + "Gems: " + fox_.Gems + '\n' + "Health: " + fox_.Health;
    }
}
