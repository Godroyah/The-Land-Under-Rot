using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Volume_Single : MonoBehaviour
{

    public bool instantKill;
    public bool healthDrain;
    public GameObject gamePlayer;
    public PlayerController playerController;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            playerController = player.GetComponent<PlayerController>();
            playerController.isDead = true;
        }
    }
}
