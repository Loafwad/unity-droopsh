using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownAnim : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float scaleTime;
    [SerializeField] private AnimationCurve scaleCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    int counter = 3;
    private IEnumerator CountDown()
    {
        text.text = counter.ToString();
        LeanTween.scale(text.gameObject, new Vector3(1.5f, 1.5f, 1), scaleTime).setEase(scaleCurve);
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
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
