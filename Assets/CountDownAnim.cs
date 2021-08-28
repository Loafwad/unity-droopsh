using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownAnim : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float scaleTime;
    [SerializeField] private AnimationCurve scaleCurve;


    [SerializeField] private GameObject hint;
    [SerializeField] private float hintTime;
    [SerializeField] private AnimationCurve hintCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    int counter = 3;
    bool animate;

    private IEnumerator CountDown()
    {
        hint.SetActive(true);
        text.text = counter.ToString();
        LeanTween.scale(text.gameObject, new Vector3(1.5f, 1.5f, 1), scaleTime).setEase(scaleCurve);
        animate = true;
        if (animate)
        {
            LeanTween.moveLocalX(hint, 0, hintTime).setEase(hintCurve);
            animate = false;
        }
        yield return new WaitForSeconds(1);
        counter--;
        if (counter > 0 && counter < 3)
        {
            StartCoroutine(CountDown());
        }
        if (counter <= 0)
        {
            LeanTween.scale(text.gameObject, new Vector3(1.5f, 1.5f, 1), scaleTime).setEase(scaleCurve);
            text.text = "START";
            LeanTween.moveLocalX(hint, -1000, hintTime).setEase(hintCurve);
            yield return new WaitForSeconds(1);
            LeanTween.moveLocalY(this.gameObject, -1000f, 4f).setEase(hintCurve);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
