using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : BaseScene
{
    [SerializeField] Image Shade;
    [SerializeField] GameObject Opening;
    [SerializeField] GameObject MainTitle;

    private void Start()
    {
        StartCoroutine(FirstOpening());
    }

    private void Update()
    {
    }

    public void SingleGame()
    {
        Debug.Log("Single game");
        Manager.Scene.LoadScene("TestScene");
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    IEnumerator FirstOpening()
    {
        Shade.gameObject.SetActive(true);
        Opening.gameObject.SetActive(true);
        MainTitle.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, 1f);
        for (float t = 1f; t > 0f; t -= Time.deltaTime / 3)
        {
            float alpha = Mathf.Lerp(0f, 1f, t);
            Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, 0f);
        for (float t = 0f; t < 1f; t += Time.deltaTime / 2)
        {
            float alpha = Mathf.Lerp(0f, 1f, t);
            Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, alpha);
            yield return null;
        }
        Opening.gameObject.SetActive(false);
        MainTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        for (float t = 1f; t > 0f; t -= Time.deltaTime / 3)
        {
            float alpha = Mathf.Lerp(0f, 1f, t);
            Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, alpha);
            yield return null;
        }
        Shade.gameObject.SetActive(false);
    }
}
