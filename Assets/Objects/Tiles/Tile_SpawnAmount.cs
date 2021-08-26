using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_SpawnAmount : Tile
{
    [SerializeField] private int spawnAmount;
    TileSpawner tileSpawner;
    ScoreManager scoreManager;
    int defaultSpawnAmount;
    void Start()
    {
        defaultSpawnAmount = spawnAmount;
        tileSpawner = GameObject.Find("Spawner").GetComponent<TileSpawner>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        originalSpawnAmount = spawnAmount;
    }
    int originalSpawnAmount;
    int counter;
    public override void DisableTile()
    {
        spawnAmount = defaultSpawnAmount;
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < spawnAmount; i++)
        {
            //GameObject tile = Instantiate(tileSpawner.tiles[Random.Range(0, tileSpawner.tiles.Length)]);
            GameObject chosenTile = this.tileSpawner.tiles[Random.Range(0, tileSpawner.tiles.Length)];
            float spawnChance = chosenTile.GetComponent<Tile>().spawnChance;
            float RandomValue = Random.value;
            GameObject customTile = this.tileSpawner.availableTiles[Random.Range(0, tileSpawner.availableTiles.Count)];
            GameObject tile;
            if (SameType(chosenTile, customTile))
            {
                tile = customTile;
                if (spawnChance >= RandomValue)
                {
                    Debug.Log("Spwaned with: " + spawnChance + " : " + RandomValue);
                    tile.transform.GetComponent<Tile>().EnableTile();
                    tile.transform.position = this.transform.position;
                    scoreManager.IncreaseScore(1, tile.transform.position);
                }
                else
                {
                    spawnAmount++;
                }
            }
            else
            {
                spawnAmount++;
                continue;
            }
        }

        this.gameObject.SetActive(false);
        tileSpawner.availableTiles.Add(this.gameObject);
    }

    public static bool SameType(GameObject first, GameObject second)
    {
        string fr = first.GetComponent<Tile>().typeName;
        string sc = second.GetComponent<Tile>().typeName;
        if (fr != sc)
        {
            return false;
        }
        return true;
    }
}
