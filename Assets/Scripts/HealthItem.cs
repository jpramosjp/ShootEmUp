using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    float hpToGive = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -125 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<EntityStats>().addFromHp(hpToGive);

            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
