using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Pierce : Shot
{
    [SerializeField] private float maxAngularVelocity = 25f;
    bool pierced;


    void IsPierced(GameObject col, int pierceCount)
    {
        LeanTween.moveLocal(col.gameObject, new Vector3(pierceCount - 1, 0, 0), 0.0f);
        /* col.transform.position = this.transform.position; */
        col.GetComponent<Rigidbody2D>().simulated = false;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform);
    }
    int pierceCount = 0;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            pierceCount++;
            if (pierceCount <= 3)
            {
                Debug.Log("HIT BY PIERCE");
                pierced = true;
                IsPierced(col.gameObject, pierceCount);
                /* LeanTween.moveLocalX(this.transform.GetChild(0).gameObject, -2f, 0.0f); */
                //col.transform.position = this.transform.position;
                col.transform.rotation = this.transform.rotation;
                col.transform.GetComponent<Collider2D>().enabled = false;
            }
        }
        if (col.gameObject.tag == "Wall")
        {
            this.GetComponent<Rigidbody2D>().simulated = false;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
