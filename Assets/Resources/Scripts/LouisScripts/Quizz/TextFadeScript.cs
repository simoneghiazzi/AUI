using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFadeScript : MonoBehaviour
{

    public Text i;
    public float t = 1f;

    public void TextFadeIn()
    {
      StartCoroutine(FadeTextToFullAlpha());
    }

    public void TextFadeOut()
    {
      StartCoroutine(FadeTextToZeroAlpha());
    }

    public IEnumerator FadeTextToFullAlpha()
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
