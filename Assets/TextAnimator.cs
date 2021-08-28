using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextAnimator : MonoBehaviour
{
    [Header("second text")]
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private float moveSecondTime;
    [SerializeField] private float scaleSecondTime;

    [SerializeField] private AnimationCurve moveSecondCurve;
    [SerializeField] private AnimationCurve scaleSecondCurve;

    [Header("comboText")]
    [SerializeField] private GameObject comboText;
    [SerializeField] private float scaleComboTime;
    [SerializeField] private AnimationCurve scaleComboCurve;


    public void CreateSecond(Vector3 pos)
    {
        GameObject text = Instantiate(textObject);
        text.transform.position = pos;
        text.transform.SetParent(this.transform, true);
        text.transform.localScale = new Vector3(1, 1, 1);
        float scaleTo = 1.5f;
        LeanTween.scale(text, new Vector3(scaleTo, scaleTo, scaleTo), scaleSecondTime).setEase(scaleSecondCurve);
        LeanTween.move(text, scoreTextObject.transform.position, moveSecondTime).setEase(moveSecondCurve).setOnComplete(() =>
        {
            Destroy(text);
        });
    }

    public void CreateCombo(Vector3 pos, int currentCombo)
    {
        LeanTween.cancel(comboText);
        GameObject childComboText = comboText.transform.GetChild(0).gameObject;
        comboText.transform.position = pos;
        comboText.SetActive(true);
        childComboText.GetComponent<TextMeshProUGUI>().text = currentCombo.ToString();
        float scaleTo = 1.2f;
        LeanTween.scale(comboText, new Vector3(scaleTo, scaleTo, scaleTo), scaleComboTime).setEase(scaleSecondCurve).setOnComplete(() =>
        {
            comboText.SetActive(false);
        });

    }

}
