using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{
    private const int MAX_HEALTH = 100;
    private const int START_LIVES_AMOUNT = 3;
    private const int START_GEMS_AMOUNT = 0;

    [SerializeField] Text playerInfo = default;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.text = "Lives: " + START_LIVES_AMOUNT + '\n' + "Gems: " + START_GEMS_AMOUNT + '\n' + "Health: " + MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.text = "Lives: " + PlayerInstance.getInstance().Lives + '\n' + "Gems: " + PlayerInstance.getInstance().Gems + '\n' + "Health: " + PlayerInstance.getInstance().Health;
    }
}
