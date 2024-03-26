using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletDamage;
    public bool isPlayer;
    public float bulletRange;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, bulletRange);
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.tag == "Enemy" && isPlayer) || (collision.gameObject.tag == "Player" && !isPlayer))
        {
           
            collision.gameObject.GetComponent<EntityStats>().removeFromHp(bulletDamage);
            Destroy(this.gameObject);
        }
        
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
