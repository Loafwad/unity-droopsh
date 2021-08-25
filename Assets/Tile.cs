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
    [SerializeField] private GameObject particle;
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
    void Update()
    {
        if (isClicked)
        {
            this.GetComponent<Collider2D>().isTrigger = true;
            reticle.transform.position = this.transform.position;
            Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(reticle.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            reticle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    IEnumerator DisableTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Collider2D>().isTrigger = false;
    }

    public void DestroyTile()
    {
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
    void OnMouseDown()
    {

        isClicked = true;
        reticle.GetComponent<ReticleAnimation>().Selected();
    }

    void OnMouseUp()
    {
        isClicked = false;
        reticle.GetComponent<ReticleAnimation>().Deselect();
        StartCoroutine(DisableTrigger());
    }
    // Update is called once per frame
}