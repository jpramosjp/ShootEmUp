using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public GameObject powerUpDrop;
    int positionSelected;
    public List<Transform> spawnPoints;
    public float cooldown;
    public float baseTime;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {

            Instance = this;
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownDrop())
        {
            positionSelected = Random.Range(0, spawnPoints.Count);
            Instantiate(powerUpDrop, spawnPoints[positionSelected].position, Quaternion.identity);
        }
    }

    public void applyEffect(GameObject target, PowerUp powerUpSelected)
    {
        Player player = target.GetComponent<Player>();
        EntityStats playerStats = target.GetComponent<EntityStats>();

        player.isProjectile = powerUpSelected.isProjectile;
        playerStats.attackSpeed = powerUpSelected.attackSpeed;
        playerStats.quantityBullet = powerUpSelected.quantity;
        playerStats.bulletRange = powerUpSelected.bulletRange;
        playerStats.attackDamage = powerUpSelected.damage;
        StartCoroutine(RevertEffect(player, playerStats, powerUpSelected.duration));
    }

    IEnumerator RevertEffect(Player player, EntityStats playerStats, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Reverter os valores do jogador de volta ao estado original
        player.isProjectile = true;
        playerStats.attackSpeed = 0.4f;
        playerStats.quantityBullet = 1;
        playerStats.bulletRange = 1.5f;
        playerStats.attackDamage = 10;
    }


    bool cooldownDrop()
    {
        if(cooldown > baseTime)
        {

            cooldown = 0;
            return true;
        }
        cooldown += Time.deltaTime;
        return false;

    }
}
