using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private float projectileSpeed;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void FireProjectile()
    {
        GameObject shot = Instantiate(point, null);
        shot.transform.position = this.transform.position;
        shot.transform.LeanScale(new Vector3(1, 1, 1), 0);
        shot.transform.rotation = this.transform.rotation;
        shot.GetComponent<Rigidbody2D>().AddRelativeForce(-this.transform.right * projectileSpeed * 100);
        shot.GetComponent<Collider2D>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
