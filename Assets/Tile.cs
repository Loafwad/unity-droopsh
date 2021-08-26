using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject freezeBorder;
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
        else if (typeName == "freeze")
        {
            reticle = GameObject.Find("FreezeShot");
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
        rb.simulated = true;
        rb.velocity = new Vector2(gravity, gravity);
        rb.gravityScale = gravity;
    }

    Vector2 previousVelocity;
    float previousGravity;
    bool isFrozen;

    public void SetFreeze()
    {
        isFrozen = true;
        scoreManager.IncreaseScore(1, this.transform.position);
        previousVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        previousGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().mass = 1000;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        freezeBorder.SetActive(true);
        freezeBorder.transform.position = transform.position;
        freezeBorder.transform.rotation = transform.rotation;
    }

    public void UnFreeze()
    {
        if (previousGravity != 0)
        {
            isFrozen = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, previousVelocity.y);
            GetComponent<Rigidbody2D>().gravityScale = this.GetComponent<Tile>().gravity;
            GetComponent<Rigidbody2D>().mass = 1;
            freezeBorder.SetActive(false);
        }
    }
    bool isClicked;
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
        else if (setPos)
        {
            reticle.transform.position = this.transform.position;
        }
    }

    bool setPos;
    IEnumerator AfterShot()
    {
        setPos = true;
        isClicked = false;
        yield return new WaitForSeconds(reticle.GetComponent<ReticleAnimation>().deselectAnimTime);
        this.GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("Finished anim");
        GetComponent<Collider2D>().enabled = false;
        setPos = false;
        GetComponent<Rigidbody2D>().AddRelativeForce(reticle.transform.right * 2 * 100);
        UnFreeze();
    }
    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = true;
    }

    public virtual void DisableTile()
    {
        isClicked = false;
        scoreManager.IncreaseScore(1, this.transform.position);
        GameObject deathParticle = Instantiate(particle, transform.position,
             transform.rotation);
        ParticleSystem.MainModule main = deathParticle.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.parent = null;

        tileSpawner.availableTiles.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void ReuseTile()
    {
        transform.parent = null;
        isClicked = false;

        tileSpawner.availableTiles.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
    public void EnableTile()
    {
        transform.parent = null;
        this.gameObject.SetActive(true);
        GetComponent<Tile>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;

        int index = tileSpawner.availableTiles.IndexOf(this.gameObject);
        if (index < tileSpawner.availableTiles.Count - 1 && index > 0)
        {
            tileSpawner.availableTiles.RemoveAt(index);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "OutOfBounds")
        {
            ReuseTile();
        }
    }
    Vector3 reticlePrevPos;
    void OnMouseDown()
    {
        isClicked = true;
        reticlePrevPos = reticle.transform.position;
        reticle.GetComponent<ReticleAnimation>().Selected();
    }

    Quaternion reticleRotation;
    void OnMouseUp()
    {
        StartCoroutine(AfterShot());
        reticle.GetComponent<ReticleAnimation>().Deselect();
    }
    // Update is called once per frame
}
