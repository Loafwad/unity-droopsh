using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAnimator : MonoBehaviour
{
    [Header("Button anim")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private float playButtonTime;
    [SerializeField] private AnimationCurve playButtonAnimCurve;

    [Header("Projectile Anim")]
    [SerializeField] private GameObject point;
    [SerializeField] private float pointTime;
    [SerializeField] private AnimationCurve pointAnimCurve;

    [Header("logo")]
    [SerializeField] private GameObject logo;
    [SerializeField] private float logoTime;
    [SerializeField] private AnimationCurve logoAnimCurve;

    [Header("this")]
    [SerializeField] private float thisTime;
    [SerializeField] private AnimationCurve thisAnimCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateButton());
    }

    void Update()
    {

    }
    public void StartGame()
    {
        StartCoroutine(SpawnTiles());
    }
    private IEnumerator SpawnTiles()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocalY(logo, 20, logoTime).setEase(logoAnimCurve);
        LeanTween.moveLocalY(this.gameObject, -20, thisTime).setEase(thisAnimCurve);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SpawnProjectile()
    {
        GameObject shot = Instantiate(point);
        shot.transform.SetParent(this.transform);
        shot.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        LeanTween.move(shot, playButton.transform.position, pointTime).setEase(pointAnimCurve);
        if (shot.GetComponent<UIPoint>().collided == true)
        {
            LeanTween.cancel(shot);
        }
        Vector3 dir = (playButton.transform.position - shot.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        shot.transform.rotation = Quaternion.Slerp(shot.transform.rotation, new Quaternion(0, 0, lookRot.z, lookRot.w), Time.deltaTime * 500f);
    }

    private IEnumerator AnimateButton()
    {
        LeanTween.moveLocalY(playButton, 0, playButtonTime).setEase(playButtonAnimCurve);
        yield return new WaitForSeconds(playButtonTime);
        StartCoroutine(AnimateButton());
    }

}
