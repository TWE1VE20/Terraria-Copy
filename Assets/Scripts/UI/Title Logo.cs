using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleLogo : MonoBehaviour
{
    public GameObject targetObject;
    public float rotateTime;

    void Start()
    {
        StartCoroutine(RotateAndScale());
    }

    IEnumerator RotateAndScale()
    {
        while (true)
        {
            float timeElapsed = 0f;
            while (timeElapsed < rotateTime)
            {
                float angle = Mathf.Lerp(0f, 8f, timeElapsed / rotateTime);
                float scale = Mathf.Lerp(1f, 0.8f, timeElapsed / rotateTime);

                targetObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                targetObject.transform.localScale = Vector3.one * scale;

                timeElapsed += Time.deltaTime;

                yield return null;
            }
            timeElapsed = 0f;
            while (timeElapsed < rotateTime)
            {
                float angle = Mathf.Lerp(8f, 0f, timeElapsed / rotateTime);
                float scale = Mathf.Lerp(0.8f, 1f, timeElapsed / rotateTime);

                targetObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                targetObject.transform.localScale = Vector3.one * scale;

                timeElapsed += Time.deltaTime;

                yield return null;
            }
            timeElapsed = 0f;
            while (timeElapsed < rotateTime)
            {
                float angle = Mathf.Lerp(0f, -8f, timeElapsed / rotateTime);
                float scale = Mathf.Lerp(1f, 0.8f, timeElapsed / rotateTime);

                targetObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                targetObject.transform.localScale = Vector3.one * scale;

                timeElapsed += Time.deltaTime;

                yield return null;
            }
            timeElapsed = 0f;
            while (timeElapsed < rotateTime)
            {
                float angle = Mathf.Lerp(-8f, 0f, timeElapsed / rotateTime);
                float scale = Mathf.Lerp(0.8f, 1f, timeElapsed / rotateTime);

                targetObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                targetObject.transform.localScale = Vector3.one * scale;

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
