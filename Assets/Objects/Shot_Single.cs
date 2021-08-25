using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Single : Shot
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponent<Tile>().DestroyTile();
            col.gameObject.SetActive(false);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

}

