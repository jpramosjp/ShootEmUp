using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    //Speed
    public float baseSpeed;
    public float moveSpeed;

    //HP
    public float maxHp;
    public float hp;

    //Attack
    public float attackSpeed;
    public float bulletSpeed;
    public float attackDamage;
    public float quantityBullet;
    public float bulletRange;

    //Enemy
    public int scoreGive;
    public GameObject itemHp;
    public bool giveItem;
    public float chanceGiveItem;
    public float enemyHit;

    //Sound
    public AudioSource shootSound;
    public AudioClip shootClip;

    //Particle
    public GameObject deathParticle;
    void Start()
    {
        if(this.gameObject.tag == "Enemy") {
            float random = Random.Range(0, 20);

            if(random <= chanceGiveItem)
            {
                giveItem = true;
            }
        }
        hp = maxHp;
        moveSpeed = baseSpeed;
        if (this.gameObject.tag == "Player")
        {
            HudManager.Instance.playerHp();
        }
    }
    private void Update()
    {
        if (this.gameObject.tag == "Player")
        {
            HudManager.Instance.playerHp();
        }
    }
    public void removeFromHp(float hpRemoved)
    {
        hp -= hpRemoved;
        if(this.gameObject.tag == "Enemy")
        {
            if(enemyHit > 500)
            {
                enemyHit = 0;
                dropItem();
            }
            enemyHit += hpRemoved;
        }

       
        Death();
    }

    void Death()
    {
        if (hp <= 0)
        {
            if(this.gameObject.tag == "Enemy")
            {
                ScoreManager.Instance.score += scoreGive;
                dropItem();
            }
            Instantiate(deathParticle, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if(this.gameObject.tag == "Player")
            {
                HudManager.Instance.gameOver();
            }
        }
    }

    public void dropItem()
    {
        if (giveItem)
        {
            Instantiate(itemHp, this.transform.position, Quaternion.identity);
        }
    }

    public void addFromHp(float hpAdd)
    {
        if(hpAdd + hp > maxHp)
        {
            hp = maxHp;
            return;
        }

        hp += hpAdd;

    }

    public void removeFromSpeed(float speed)
    {
        attackSpeed -= speed;
    }


    public void addToSpeed(float speed)
    {
        attackSpeed += speed;
    }



}
