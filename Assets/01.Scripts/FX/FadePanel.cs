using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class FadePanel : MonoBehaviour
{
    public float xOrigin;
    public float yOrigin;

    public Vector2 offset = Vector2.zero;
    public float pixelCount = 128.0f;
    public Shader shader;

    private Material material;

    private void Awake()
    {
        if (material == null) material = new Material(shader);

        Image img = GetComponent<Image>();
        img.material = material;
        img.enabled = true;
        material.SetFloat("_radius", 5.0f);
        material.SetFloat("_pixel", pixelCount);
        //material.SetFloat("_pixel", 128.0f);
    }

    private void Fade(float xPos, float yPos)
    {
        offset = new Vector2(xPos, yPos);

        material.SetFloat("_xOff", offset.x);
        material.SetFloat("_yOff", offset.y);
        
        material.DOFloat(0.0f, Shader.PropertyToID("_radius"), 1.0f).SetUpdate(UpdateType.Fixed).SetEase(Ease.InOutBack);
    }

    private void DeFade()
    {
        material.SetFloat("_xOff", 0);
        material.SetFloat("_yOff", 0);
        material.DOFloat(5.0f, Shader.PropertyToID("_radius"), 2.0f);
    }

    public Coroutine StartFade(Vector2 pos)
    {
        Coroutine fadeRoutine = StartCoroutine(FadeCo(pos.x, pos.y));
        return fadeRoutine;
    }

    private IEnumerator FadeCo(float xPos, float yPos)
    {
        float remapXPos = remap(xPos, -xOrigin, xOrigin, -.5f, .5f);
        float remapYPos = remap(yPos, -yOrigin, yOrigin, -.5f, .5f);

        Fade(-remapXPos, -remapYPos);
        yield return new WaitForSeconds(1.5f);
        DeFade();
    }

    private float remap(float val, float min1, float max1, float min2, float max2)
    {
        return min2 + (val - min1) * (max2 - min2) / (max1 - min1);
    }
}
