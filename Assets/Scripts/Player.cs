using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //PlayerStats
    EntityStats playerStats;
    
    //Attack
    public GameObject projectile;
    public GameObject Granade;
    public float cooldown;
    public bool canShoot;
    public bool isProjectile;
    

    void Start()
    {
        playerStats = GetComponent<EntityStats>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moovePlayer();

       
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            attack();
        }

        if (!canShoot)
        {
            cooldownShoot();
        }
    }
    public void cooldownShoot()
    {
        if(cooldown > playerStats.attackSpeed ) 
        {
            canShoot = true;
            cooldown = 0;
            return;
        }
        cooldown += Time.deltaTime;
       
    }

    public void moovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * playerStats.moveSpeed * Time.fixedDeltaTime, vertical * playerStats.moveSpeed * Time.fixedDeltaTime), ForceMode2D.Force);
        if ((horizontal > 0 || horizontal < 0) && (vertical > 0 || vertical < 0))
        {
            playerStats.moveSpeed = playerStats.baseSpeed * 0.66f;
        }
        else
        {
            playerStats.moveSpeed = playerStats.baseSpeed;
        }
    }
    void attack()
    {
        playerStats.shootSound.PlayOneShot(playerStats.shootClip);
        Vector2[] spawnPositions = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2( -1f, 1),
            new Vector2(1f, 1)
        };
        if(isProjectile)
        {
            for (int i = 0; i < playerStats.quantityBullet; i++)
            {
                GameObject projectileClone = Instantiate(projectile, this.transform.position, Quaternion.identity);
                projectileClone.GetComponent<BulletBehaviour>().bulletRange = playerStats.bulletRange;
                projectileClone.GetComponent<BulletBehaviour>().bulletDamage = playerStats.attackDamage;
                projectileClone.GetComponent<BulletBehaviour>().isPlayer = true;
                projectileClone.GetComponent<Rigidbody2D>().AddForce(spawnPositions[i] * playerStats.bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                canShoot = false;

            }
            return;
        }

        GameObject GranadeClone = Instantiate(Granade, this.transform.position, Quaternion.identity);
        GranadeClone.GetComponent<GranedeBehaviour>().bulletRange = playerStats.bulletRange;
        GranadeClone.GetComponent<GranedeBehaviour>().damage = playerStats.attackDamage;
        GranadeClone.GetComponent<Rigidbody2D>().AddForce(spawnPositions[0] * playerStats.bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        canShoot = false;
    }
}
