using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    public float startAlpha = 1f;
    public float targetAlpha = 0f;
    public float delay = 0f;
    public float timeToFate = 1f;

    float inc;
    float currentAlpha;
    MaskableGraphic graphic;
    Color originColor;

    void Start()
    {
        graphic = GetComponent<MaskableGraphic>();
        originColor = graphic.color;
        currentAlpha = startAlpha;

        Color tempColor = new Color(originColor.r, originColor.g, originColor.b, currentAlpha);
        graphic.color = tempColor;
        inc = (targetAlpha - startAlpha) / timeToFate * Time.deltaTime;
        StartCoroutine(FateRoutine());
    }
    IEnumerator FateRoutine()
    {
        yield return new WaitForSeconds(delay);
        while(Mathf.Abs(targetAlpha-currentAlpha) > 0.01f)
        {
            yield return new WaitForEndOfFrame();
            currentAlpha += inc;
            Color tempColor = new Color(originColor.r, originColor.g, originColor.b, currentAlpha);
            graphic.color = tempColor;
        }
    }
}
