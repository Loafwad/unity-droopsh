using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Single : Shot
{
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    /* int counter; */
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            col.gameObject.GetComponent<Tile>().DisableTile();
            gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Wall")
        {
            audioManager.PlayAudioWallHit(this.transform.position);
        }
    }

}

