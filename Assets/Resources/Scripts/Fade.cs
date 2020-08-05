using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    public void fadeinfadout()
    {
        StartCoroutine(FadeInOut(GetComponent<Text>(), 1f, 2f));
    }

    private IEnumerator FadeInOut(Text i, float fadeIn, float fadeOut)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / fadeIn));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / fadeOut));
            yield return null;
        }
        //Destroy(gameObject);
    }

    private void FadeTextToFullAlpha(float t, Text i)
    {
        
    }

    private void FadeTextToZeroAlpha(float t, Text i)
    {
        
    }
}
