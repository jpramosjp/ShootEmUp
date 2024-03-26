using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranedeBehaviour : MonoBehaviour
{
    public float explosionRadius = 3f;
    public float explosionForce = 10f;
    public float damage;

    public float bulletRange;

    public LayerMask damageableLayer;

    //Particle
    public GameObject deathParticle;

    private void Start()
    {
        Destroy(gameObject, bulletRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Explode();
            
        }
    }

    void Explode()
    {

        
        // Cria um círculo de colisão para detectar os inimigos dentro do raio de explosão
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
        
        foreach (Collider2D col in colliders)
        {
            if(col.gameObject.tag == "Player")
            {
                continue;
            }
            EntityStats entityStats = col.gameObject.GetComponent<EntityStats>();
            if (entityStats != null)
            {
                entityStats.removeFromHp(damage);
            }
        }
        Instantiate(deathParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    // Desenhe o raio de explosão na cena para facilitar a visualização
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
