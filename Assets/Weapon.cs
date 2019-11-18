using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using System.IO;

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

    public Camera cam;
    public RaycastHit2D hit;
    public LayerMask cullingMask;
    public float Maxdistance;
    public bool isFlying;
    public Vector2 loc;
    public float speed = 10f;
    public Transform hand;

    public PlatformerCharacter2D pc2d;
    public LineRenderer LR;

    Vector2 mousePosition;
    Vector2 firePointPosition;
    Vector2 targetPos;

    Vector2 mousepos;
    public Transform[] point;
    public Transform Laser;
    public GameObject prefab_HotSpot;
    private SpriteRenderer sprLaser;
    bool CoroutineFire = true;
    bool startLaser = false;

    void Start()
    {
        firePoint = transform.FindChild("FirePoint");
        blast = GameObject.Find("blast");
        player = GameObject.FindGameObjectWithTag("Player");
        sprLaser = Laser.GetComponent<SpriteRenderer>();
        sprLaser.enabled = false;
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

        if (muzzleBool && Input.GetMouseButtonUp(0) && Time.time > timeToSpawnEffect && Time.time >= timeToSpawnEffect)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootMuzzel();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        if (laserBool && Input.GetMouseButtonDown(0))
        {
            startLaser = true;
        }

        if (startLaser)
        {
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mousepos - (Vector2)transform.position).normalized;

            hit = Physics2D.Linecast(point[0].position, point[1].position, Physics2D.DefaultRaycastLayers);

            Laser.localScale = new Vector3(Laser.localScale.x, hit.distance < 0.00001 ? 25f : (hit.distance / 3f), 1f);

            sprLaser.enabled = true;
            if (hit.distance > 0.01f)
            {
                if (CoroutineFire)
                {
                    StartCoroutine(SpotFlame());
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            sprLaser.enabled = false;
            startLaser = false;
        }
        
    }

    IEnumerator SpotFlame()
    {
        CoroutineFire = false;
        GameObject g = Instantiate(prefab_HotSpot, hit.point, Quaternion.identity).gameObject;
        yield return new WaitForSeconds(0.015f);
        CoroutineFire = true;
    }

    void ShootMuzzel()
    {
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        Effect();
           
        hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
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
