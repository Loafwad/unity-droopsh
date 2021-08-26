using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Time : Tile
{
    [SerializeField] private float slowDuration = 1f;
    private TimeManager time;
    void Start()
    {
        time = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }
    public override void DisableTile()
    {
        if (gameObject != null || this.gameObject.activeSelf == true)
        {
            StartCoroutine(DelayTime());
        }
    }

    private IEnumerator DelayTime()
    {
        time.SetTime(0.15f, 0.005f);

        yield return new WaitForSeconds(slowDuration / 0.15f);
        GameObject deathParticle = Instantiate(particle, transform.position,
            transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        gameObject.SetActive(false);
    }
}
