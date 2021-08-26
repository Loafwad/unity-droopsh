using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int score;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnDelay;

    [SerializeField] public GameObject[] tiles;

    [SerializeField] public List<GameObject> availableTiles;
    // Start is called before the first frame update
    public float spawnTime;
    void Start()
    {
        spawnTime = spawnDelay;
        spawnArea = new Vector2(this.GetComponent<Collider2D>().bounds.size.x, this.GetComponent<Collider2D>().bounds.size.y);
        StartCoroutine(SpawnerController());
        for (int i = 0; i < 1000; i++)
        {
            GameObject tile = Instantiate(tiles[Random.Range(0, tiles.Length)]);
            availableTiles.Add(tile);
            SpawnTile(spawnArea, tile);
            tile.SetActive(false);
            tile.transform.position = new Vector2(tile.transform.position.x, -100);
        }
    }

    public void SpawnTile(Vector2 bounds, GameObject tile)
    {
        Vector2 randomPos = new Vector2(Random.Range(-bounds.x, bounds.x), Random.Range(this.transform.position.y, this.transform.position.y + bounds.y));
        tile.transform.position = randomPos;
    }

    private IEnumerator SpawnerController()
    {
        for (int i = 0; i < availableTiles.Count; i++)
        {
            //activeTiles[i] = tiles[Random.Range(0, tiles.Length)];
            GameObject tile = availableTiles[i];
            if (tile == null)
            {
                continue;
            }
            tile.GetComponent<Rigidbody2D>().simulated = false;

            if (Random.value < tile.GetComponent<Tile>().spawnChance)
            {
                yield return new WaitForSeconds(spawnTime);
                tile.SetActive(true);
                tile.GetComponent<Tile>().EnableTile();
                SpawnTile(spawnArea, tile);
            }
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnerController());
    }
}
