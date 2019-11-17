using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameMaster>();
        }
    }

    public Transform spawnPrefab;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    public IEnumerator RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
        Destroy(clone, 2.5f);

    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}
