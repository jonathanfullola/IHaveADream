using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roperatio : MonoBehaviour
{

    public GameObject Player;
    public Vector3 grabPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scaleX = Vector3.Distance(Player.transform.position,grabPos);
        GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(scaleX, 1f);
    }
}
