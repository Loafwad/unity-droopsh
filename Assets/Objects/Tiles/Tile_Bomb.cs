using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Bomb : Tile
{
    [SerializeField] private GameObject explosionCollider;
    public override void DisableTile()
    {
        explosionCollider.gameObject.SetActive(true);
        if (this.gameObject != null)
        {
            StartCoroutine(DestroyTile());
        }
    }

    public IEnumerator DestroyTile()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject deathParticle = Instantiate(particle, transform.position,
            transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        this.PlayAudioHit();
        this.gameObject.SetActive(false);
    }
}
