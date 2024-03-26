using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    EntityStats bossStats;
    public Transform destino;
    public GameObject projectilePrefab;

    public float coolddown;
    public bool canAttack;
    public float baseTimeAttack;
    public float coneAngle = -15f; // Ângulo do cone de disparo
    public float oscillationDuration = 10f;
    public List<Transform> bordasX;
    public GameObject laserGameObject;
    int lastAttack = -1;

    //SFX
    public AudioSource ShakeSound;
    void Start()
    {

        bossStats = GetComponent<EntityStats>();
        laserGameObject.GetComponent<LaserBehaviour>().bulletDamage = bossStats.attackDamage;
        ShakeManager.Instance.startShake();
        ShakeSound.Play();
        StartCoroutine(Andar(destino));
        
    }

    // Update is called once per frame
    void Update()
    {

        cooldownAttack();
    }

    private void FixedUpdate()
    {
        if (canAttack)
        {
            SelectRandomAttack();

            canAttack = false;
        }
    }
    public void cooldownAttack()
    {
        if(coolddown > bossStats.attackSpeed)
        {
            coolddown = 0;
            canAttack = true;
            return;
        }

        coolddown += Time.deltaTime;

    }

    void SelectRandomAttack()
    {
        int attack;
        do
        {
            attack = Random.Range(0, 2);
        } while (attack == lastAttack); // Evita que o mesmo ataque seja selecionado duas vezes seguidas

        lastAttack = attack;

        if (attack == 0)
        {
            StartCoroutine(attackBullet());
        }
        else if (attack == 1)
        {
            StartCoroutine(laserAttack());
        }
    }
    IEnumerator laserAttack()
    {
        int xInicial = Random.Range(0, bordasX.Count);
        int xFinal = 0;
        if (xInicial == 0)
        {
            xFinal = 1;
        }
        while (Vector3.Distance(transform.position, bordasX[xInicial].position) > 0.1f)
        {
            // Move o chefe gradualmente em direção ao destino
            transform.position = Vector3.MoveTowards(transform.position, bordasX[xInicial].position, bossStats.baseSpeed * 2 * Time.deltaTime);

            // Aguarda o próximo quadro antes de continuar
            yield return null;
        }
        yield return new WaitForSeconds(2);
        laserGameObject.SetActive(true);

        while (Vector3.Distance(transform.position, bordasX[xFinal].position) > 0.1f)
        {
            // Move o chefe gradualmente em direção ao destino
            transform.position = Vector3.MoveTowards(transform.position, bordasX[xFinal].position, bossStats.baseSpeed * Time.deltaTime);

            // Aguarda o próximo quadro antes de continuar
            yield return null;
        }
        laserGameObject.SetActive(false);
        StartCoroutine(Andar(destino));
    }

    IEnumerator Andar(Transform destino)
    {
        while (Vector3.Distance(transform.position, destino.position) > 0.1f)
        {
            // Move o chefe gradualmente em direção ao destino
            transform.position = Vector3.MoveTowards(transform.position, destino.position, bossStats.baseSpeed * Time.deltaTime);

            // Aguarda o próximo quadro antes de continuar
            yield return null;
        }
        ShakeManager.Instance.stopShake();
    }

    
    IEnumerator attackBullet()
    {
       
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        for(int j = 0; j < 4; j++)
        {
            for (int i = 0; i < bossStats.quantityBullet; i++)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                float randomX = Random.Range(-coneAngle / 2f, coneAngle / 2f);
                Vector3 randomDirection = new Vector3(directionToPlayer.x + randomX, directionToPlayer.y, directionToPlayer.z);

                // Instancia o projétil
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<BulletBehaviour>().bulletRange = bossStats.bulletRange;
                // Configura a direção e velocidade do projétil
                projectile.GetComponent<Rigidbody2D>().velocity = randomDirection.normalized * bossStats.bulletSpeed * Time.fixedDeltaTime;

            }
            yield return new WaitForSeconds(2);

        }


    }

    private void OnDestroy()
    {
        HudManager.Instance.win();
    }

}



