using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;
    GameObject player;

    float timeToFire = 1.5f;

    Transform firePoint;
    GameObject blast;

    public Transform BulletTrailMuzzel;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    public bool muzzleBool = true;
    public bool laserBool = false;

    GameObject muzzle, laser;
    public Sprite muzzleImage;
    public Sprite laserImage;

    void Start()
    {
        firePoint = transform.FindChild("FirePoint");
        blast = GameObject.Find("blast");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Debug.Log("muzzel");
            muzzleBool = true;
            laserBool = false;
            this.GetComponent<SpriteRenderer>().sprite = muzzleImage;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) // backwards
        {
            Debug.Log("laser");
            laserBool = true;
            muzzleBool = false;
            this.GetComponent<SpriteRenderer>().sprite = laserImage;
        }

        if (muzzleBool && Input.GetButtonUp("Fire1") && Time.time > timeToFire && Time.time >= timeToSpawnEffect)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootMuzzel();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }        
        
    }

 

    void ShootMuzzel()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        Effect();
           
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, mousePosition,Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
        }
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(mousePosition - firePointPosition).normalized.x * 1000, -(mousePosition - firePointPosition).normalized.y * 250) );
    }

    void Effect()
    {
        Object.Instantiate(BulletTrailMuzzel, firePoint.position, firePoint.rotation,this.transform);
    }

}
