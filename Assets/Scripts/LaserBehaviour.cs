using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public float bulletDamage;
    public bool isPlayer;
    public float damageCooldown = 1f; // Tempo de cooldown entre os danos
    private float lastDamageTime; // Momento do último dano causado



    public float defDistanceRay = 100;
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    Transform m_transform;
    // Start is called before the first frame update
    void Awake()
    {
        m_transform = GetComponent<Transform>();
        lastDamageTime = -damageCooldown; 
    }

    private void Update()
    {
        //if(KeyCode.Space)


            ShootLaser();
        
        //ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.right, defDistanceRay);

        if (hit.collider != null)
        {
            Draw2DRay(laserFirePoint.position, hit.point);

            
            if(hit.collider.gameObject.tag == "Player" && Time.time - lastDamageTime > damageCooldown)
            {
                EntityStats entityStats = hit.collider.gameObject.GetComponent<EntityStats>();
                entityStats.removeFromHp(bulletDamage);
                lastDamageTime = Time.time;
            }

            if(hit.collider.gameObject.tag == "Bullet")
            {
                Destroy(hit.collider.gameObject);
            }
            
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }


    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);

    }

}
