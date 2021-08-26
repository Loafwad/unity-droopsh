using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject reticle;
    [SerializeField] private float gravity;
    [SerializeField] public float spawnChance;
    [SerializeField] public string typeName;
    private Rigidbody2D rb;
    [SerializeField] public GameObject particle;
    // Start is called before the first frame update
    private TileSpawner tileSpawner;
    private ScoreManager scoreManager;
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
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
            reticle.transform.position = transform.position;
            this.GetComponent<Collider2D>().isTrigger = true;
            Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(reticle.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            reticle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (setPos)
        {
            reticle.transform.position = this.transform.position;
        }
    }

    bool setPos;
    IEnumerator AfterShot()
    {
        isClicked = false;
        setPos = true;
        yield return new WaitForSeconds(reticle.GetComponent<ReticleAnimation>().deselectAnimTime);
        this.GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("Finished anim");
        GetComponent<Collider2D>().enabled = false;
        setPos = false;
        StartCoroutine(EnableCollider());
        GetComponent<Rigidbody2D>().AddRelativeForce(reticle.transform.right * 2 * 100);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = true;
    }

    public virtual void DisableTile()
    {
        scoreManager.IncreaseScore(1, this.transform.position);
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        this.gameObject.SetActive(false);
        transform.parent = null;
        isClicked = false;

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

    Quaternion reticleRotation;
    void OnMouseUp()
    {
        reticle.GetComponent<ReticleAnimation>().Deselect();
        StartCoroutine(AfterShot());
    }
    // Update is called once per frame
}
