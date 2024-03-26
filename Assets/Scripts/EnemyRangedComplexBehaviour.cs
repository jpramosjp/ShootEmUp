using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedComplexBehaviour : MonoBehaviour
{
    //EnemyStats
    EntityStats enemyStats;

    public GameObject projectile;
    public float cooldown;
    public bool canShoot;
    public float rotationSpeed;
    void Start()
    {
        enemyStats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotacionar o Rigidbody ao redor do eixo Y
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        if (canShoot)
        {
            attack();
        }
        cooldownShoot();
    }


    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyStats.baseSpeed * Time.fixedDeltaTime);
       
    }
    public void cooldownShoot()
    {
        if (cooldown > enemyStats.attackSpeed)
        {
            canShoot = true;
            cooldown = 0;
            return;
        }
        cooldown += Time.deltaTime;

    }

    void attack()
    {
        Vector2[] spawnPositions = new Vector2[]
        {
            new Vector2(transform.position.x + 1f, transform.position.y + 1f),
            new Vector2(transform.position.x -  1f, transform.position.y -  1f), 
            new Vector2(transform.position.x + 1f, transform.position.y -  1f), 
            new Vector2(transform.position.x  -  1f, transform.position.y +  1f)
        };

        foreach (Vector2 spawnPosition in spawnPositions)
        {
            GameObject projectileClone = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectileClone.GetComponent<BulletBehaviour>().bulletDamage = enemyStats.attackDamage;
            projectileClone.GetComponent<BulletBehaviour>().isPlayer = false;
            Rigidbody2D rb2d = projectileClone.GetComponent<Rigidbody2D>();
            rb2d.AddForce((spawnPosition - (Vector2)transform.position).normalized * enemyStats.bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        canShoot = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<EntityStats>().removeFromHp(enemyStats.attackDamage);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
