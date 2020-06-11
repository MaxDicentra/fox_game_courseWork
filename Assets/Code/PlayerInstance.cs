using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance
{
    private static playerScript instance;

    public static playerScript getInstance()
    {
        return instance;
    }

    public static void setInstance(playerScript inst)
    {
        instance = inst;
    }
}
