using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimteHUD : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float hudTime;
    [SerializeField] private AnimationCurve hudCurve;
    void Start()
    {
        StartCoroutine(moveHUD());
    }

    private IEnumerator moveHUD()
    {
        yield return new WaitForSeconds(3f);
        LeanTween.moveLocalX(this.gameObject, 0, hudTime).setEase(hudCurve);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
