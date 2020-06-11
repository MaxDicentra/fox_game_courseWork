using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{

    [SerializeField] Text playerInfo = default;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.text = "Lives: " + 3 + '\n' + "Gems: " + 0 + '\n' + "Health: " + 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.text = "Lives: " + PlayerInstance.getInstance().Lives + '\n' + "Gems: " + PlayerInstance.getInstance().Gems + '\n' + "Health: " + PlayerInstance.getInstance().Health;
    }
}
