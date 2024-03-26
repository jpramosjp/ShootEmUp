using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EntityStats enemyStats;
    public float cooldown;
    public bool canWalkX;
    public float forceX;
    public float baseTime;

    void Start()
    {

        enemyStats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyStats.baseSpeed * Time.fixedDeltaTime);
        if (canWalkX)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
            canWalkX = false;
        }
        cooldownMove();
    }

    public void Update()
    {
        
    }
    public void cooldownMove()
    {
        if(cooldown > baseTime)
        {
            baseTime = Random.Range(1.5f, 3.5f); ;
            cooldown = 0;
            forceX *= -1 ;
            canWalkX = true;
            return;
        }
        cooldown += Time.deltaTime;
        
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
