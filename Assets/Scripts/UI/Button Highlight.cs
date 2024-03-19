using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    private void Start()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Highlight", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Highlight", false);
    }
}
