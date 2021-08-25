using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject reticle;
    [SerializeField] private float gravity;
    [SerializeField] public float spawnChance;
    [SerializeField] private string typeName;
    private Rigidbody2D rb;
    [SerializeField] public GameObject particle;
    // Start is called before the first frame update
    private TileSpawner tileSpawner;
    void Awake()
    {
        if (typeName == "single")
        {
            reticle = GameObject.Find("SingleShot");
        }
        else if (typeName == "pierce")
        {
            reticle = GameObject.Find("PierceShot");
        }
        else
        {
            reticle = GameObject.Find("Default");
        }
        tileSpawner = GameObject.Find("Spawner").GetComponent<TileSpawner>();
    }
    void Start()
    {
        this.GetComponent<Collider2D>().enabled = true;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(gravity, gravity);
        rb.gravityScale = gravity;
    }

    bool isClicked;
    bool isAvailable = true;
    void Update()
    {
        if (isClicked)
        {
            this.GetComponent<Collider2D>().isTrigger = true;
            reticle.transform.position = transform.position;
            Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(reticle.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            reticle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    IEnumerator DisableTrigger()
    {
        reticle.transform.rotation = this.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Collider2D>().isTrigger = false;
        isClicked = false;
    }

    public virtual void DisableTile()
    {
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        this.gameObject.SetActive(false);
        transform.parent = null;

        tileSpawner.availableTiles.Add(this.gameObject);
    }

    public void ReuseTile()
    {
        this.gameObject.SetActive(false);
        transform.parent = null;

        tileSpawner.availableTiles.Add(this.gameObject);
    }
    public void EnableTile()
    {
        transform.parent = null;
        this.gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Tile>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = true;

        tileSpawner.availableTiles.Remove(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "OutOfBounds")
        {
            ReuseTile();
        }
    }
    void OnMouseDown()
    {

        isClicked = true;
        reticle.GetComponent<ReticleAnimation>().Selected();
    }

    void OnMouseUp()
    {
        reticle.GetComponent<ReticleAnimation>().Deselect();
        StartCoroutine(DisableTrigger());
    }
    // Update is called once per frame
}
