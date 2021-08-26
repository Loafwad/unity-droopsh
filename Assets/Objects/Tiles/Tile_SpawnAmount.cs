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
    int multiplierAmount;
    public override void DisableTile()
    {
        spawnAmount = defaultSpawnAmount;
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        GetComponent<Collider2D>().enabled = false;
        multiplierAmount = 0;
        for (int i = 0; i < spawnAmount; i++)
        {
            //GameObject tile = Instantiate(tileSpawner.tiles[Random.Range(0, tileSpawner.tiles.Length)]);
            if (spawnAmount > 1000)
            {
                break;
            }
            GameObject chosenTile = this.tileSpawner.tiles[Random.Range(0, tileSpawner.tiles.Length)];
            float spawnChance = chosenTile.GetComponent<Tile>().spawnChance;
            float RandomValue = Random.value;
            GameObject customTile = this.tileSpawner.availableTiles[Random.Range(0, tileSpawner.availableTiles.Count)];
            if (SameType(chosenTile, customTile))
            {
                if (customTile.GetComponent<Tile>().typeName == "3x" ||
                customTile.GetComponent<Tile>().typeName == "5x" ||
                customTile.GetComponent<Tile>().typeName == "10x")
                {
                    multiplierAmount++;
                    if (multiplierAmount >= 3)
                    {
                        spawnAmount++;
                        continue;
                    }
                }
                GameObject tile = customTile;
                if (spawnChance >= RandomValue)
                {
                    tile.transform.GetComponent<Tile>().EnableTile();
                    tile.transform.position = this.transform.position;
                    scoreManager.IncreaseScore(1, tile.transform.position);
                }
                else
                {
                    spawnAmount++;
                    continue;
                }
            }
            else
            {
                spawnAmount++;
                continue;
            }
        }

        multiplierAmount = 0;
        this.gameObject.SetActive(false);
        tileSpawner.availableTiles.Add(this.gameObject);
    }

    public static bool SameType(GameObject first, GameObject second)
    {
        if (first == null || second == null)
        {
            return false;
        }
        string fr = first.GetComponent<Tile>().typeName;
        string sc = second.GetComponent<Tile>().typeName;
        if (fr != sc)
        {
            return false;
        }
        return true;
    }
}
