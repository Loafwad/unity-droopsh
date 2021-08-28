using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Bomb_Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private float deathRange;
    void OnTriggerEnter2D(Collider2D col)
    {
        deathRange = this.GetComponent<CircleCollider2D>().radius;
        if (col.gameObject.tag == "Tile")
        {
            if (Vector3.Distance(col.gameObject.transform.position, this.transform.position) > deathRange)
            {
                col.gameObject.GetComponent<Tile>().DisableTile();
            }
            else
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Camera.main.transform.up * 25 * 100);
            }
        }
    }
}
