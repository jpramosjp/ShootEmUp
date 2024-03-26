using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public List<PowerUp> powerUps;
    public PowerUp powerUpSelected;
    // Start is called before the first frame update
    void Start()
    {
        int indiceAleatorio = Random.Range(0, powerUps.Count);

        powerUpSelected = powerUps[indiceAleatorio];
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
            PowerUpManager.Instance.applyEffect(collision.gameObject, powerUpSelected);
            Destroy(this.gameObject);

        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    


}
