using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : Tile
{
    public Shot customShot;
    public override void DisableTile()
    {
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        this.gameObject.SetActive(false);
        customShot.FireProjectile();
        customShot.FireProjectile();
        customShot.FireProjectile();
        customShot.FireProjectile();
        customShot.FireProjectile();
        Debug.Log("FIRE PROJECTiLES");
    }
}
