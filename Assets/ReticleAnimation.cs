using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleAnimation : MonoBehaviour
{
    [SerializeField] private GameObject circle1;
    [SerializeField] private float c1start;
    [SerializeField] private float c1end;
    [SerializeField] private GameObject circle2;
    [SerializeField] private float c2start;
    [SerializeField] private float c2end;
    [SerializeField] private GameObject point;
    [SerializeField] private float pointStart;
    [SerializeField] private float pointEnd;

    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float selectAnimTime;
    [SerializeField] private float deselectAnimTime;


    public void Selected()
    {
        this.gameObject.SetActive(true);
        point.gameObject.SetActive(true);
        LeanTween.moveLocalX(circle2, c2start, selectAnimTime).setEase(animCurve);
        LeanTween.moveLocalX(circle1, c1start, selectAnimTime).setEase(animCurve);
        LeanTween.moveLocalX(point, pointStart, selectAnimTime).setEase(animCurve);
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.005f;
    }

    public void Deselect()
    {

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        LeanTween.moveLocalX(circle1, c1end, deselectAnimTime).setEase(animCurve);
        LeanTween.moveLocalX(circle2, c2end, deselectAnimTime).setEase(animCurve);
        LeanTween.moveLocalX(point, pointEnd, deselectAnimTime).setEase(animCurve).setOnComplete(() =>
        {
            point.GetComponent<Shot>().FireProjectile();
            this.gameObject.SetActive(false);
        }
        );
    }


}
