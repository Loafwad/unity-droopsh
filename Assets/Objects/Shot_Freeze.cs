using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Freeze : Shot
{
    [SerializeField] private GameObject freezeBorder;

    Vector3 previousVelocity;
    float previousGravity;
    // Update is called once per frame
    void Update()
    {

    }
    bool frozen;
    GameObject collidedItem;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {

            col.gameObject.GetComponent<Tile>().SetFreeze();
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(SlowTile(col.gameObject));
        }
    }
    IEnumerator SlowTile(GameObject col)
    {

        yield return new WaitForSeconds(2f);

        col.GetComponent<Tile>().UnFreeze();

        this.gameObject.SetActive(false);

    }

}

