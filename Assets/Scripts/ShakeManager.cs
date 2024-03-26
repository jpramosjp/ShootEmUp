using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }

    public Animator camAnim;
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
    // Start is called before the first frame update
    

    public void startShake()
    {
        camAnim.SetBool("canShake", true);
    }

    public void stopShake()
    {
        camAnim.SetBool("canShake", false);
    }
}
