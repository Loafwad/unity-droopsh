using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPoint : MonoBehaviour
{
    public bool collided;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Button")
        {
            Debug.Log("Hello");
            collided = true;
            LeanTween.cancel(this.gameObject);
            this.transform.SetParent(col.gameObject.transform, true);
        }
    }
}
