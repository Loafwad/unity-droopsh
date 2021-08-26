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
    int counter;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            counter++;
            if (counter >= 2)
            {
                counter = 0;
                return;
            }
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
            gameObject.GetComponent<Collider2D>().enabled = false;
            col.gameObject.GetComponent<Tile>().DisableTile();
        }
    }

}

