using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Pierce : Shot
{
    [SerializeField] private float maxAngularVelocity = 25f;

    private AudioManager audioManager;
    bool pierced;
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void IsPierced(GameObject col, int pierceCount)
    {
        LeanTween.moveLocal(col.gameObject, new Vector3(pierceCount - 1, 0, 0), 0.0f);
        /* col.transform.position = this.transform.position; */
        col.GetComponent<Rigidbody2D>().simulated = false;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform);
    }
    int pierceCount = 0;
    List<GameObject> piercedObject = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tile" && col)
        {
            pierceCount++;
            if (pierceCount <= 3)
            {
                audioManager.PlayAudioPierce(col.transform.position);
                piercedObject.Add(col.gameObject);
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
            for (int i = 0; i < piercedObject.Count; i++)
            {
                piercedObject[i].GetComponent<Tile>().DisableTile();
            }
            audioManager.PlayAudioWallHit(this.transform.position);
            this.GetComponent<Rigidbody2D>().simulated = false;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyShot());
        }
    }

    private IEnumerator DestroyShot()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
