using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedSimpleBehaviour : MonoBehaviour
{
    //EnemyStats
    EntityStats enemyStats;

    public GameObject projectile;
    public float cooldown;
    public bool canShoot;
    void Start()
    {
        enemyStats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
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
        GameObject projectileClone = Instantiate(projectile, this.transform.position, Quaternion.identity);
        projectileClone.GetComponent<BulletBehaviour>().bulletDamage = enemyStats.attackDamage;
        projectileClone.GetComponent<BulletBehaviour>().isPlayer = false;
        projectileClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, - enemyStats.bulletSpeed * Time.fixedDeltaTime), ForceMode2D.Impulse);
        canShoot = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<EntityStats>().removeFromHp(enemyStats.attackDamage);
            Destroy(this.gameObject);
        }
    }
}
