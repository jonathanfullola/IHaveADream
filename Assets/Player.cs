using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public class PlayerStats
    {
        public int health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();
    public void DamagePlayer(int damage)
    {
        playerStats.health -= damage;
        if(playerStats.health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }
}
