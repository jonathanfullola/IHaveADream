using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 1.5f;

    Transform firePoint;
    GameObject blast;

    void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        blast = GameObject.Find("blast");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Debug.Log("button up");
                blast.GetComponent<Animator>().SetInteger("blastInt",1);
                Shoot();
                if (blast.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blast"))
                {
                    blast.GetComponent<Animator>().SetInteger("blastInt", 0);
                }
            }
        }else
        {
            if(Input.GetButtonDown("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

    }

 

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, mousePosition,Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
        }
    }
}
