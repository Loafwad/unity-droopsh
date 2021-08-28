using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : Tile
{
    public Shot customShot;

    TileSpawner tileSpawner;
    void Start()
    {
        tileSpawner = GameObject.Find("Spawner").GetComponent<TileSpawner>();
    }
    public override void DisableTile()
    {
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        customShot.BurstProjectile(Random.rotation);
        this.PlayAudioShot();
        customShot.BurstProjectile(Random.rotation);
        this.PlayAudioShot();
        customShot.BurstProjectile(Random.rotation);
        this.PlayAudioShot();
        this.PlayAudioHit();
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        tileSpawner.availableTiles.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
