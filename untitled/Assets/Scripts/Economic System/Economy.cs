using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    private float playerCoins;
    public float playerAccount{get{return playerCoins;}}

    private int playerHearts;
    public int playerHealth{get{return playerHearts;}}

    public void AddPlayerCoins(float coins){//adjust player's coins
        playerCoins += coins;
    }

    public void MinusPlayerHearts (int hearts){//adjust player health
        playerHearts -= hearts;
    }

}
