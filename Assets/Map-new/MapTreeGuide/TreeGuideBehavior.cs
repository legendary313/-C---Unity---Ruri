using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeGuideBehavior : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] GameObject arrow_up;
    [SerializeField] GameObject arrow_down;
    [SerializeField] GameObject arrow_left;
    [SerializeField] GameObject arrow_right;
    [Header("TMPro")]
    [SerializeField] TextMeshProUGUI tmpro_up;
    [SerializeField] TextMeshProUGUI tmpro_down;
    [SerializeField] TextMeshProUGUI tmpro_left;
    [SerializeField] TextMeshProUGUI tmpro_right;
    [Header("Direction Name")]
    [SerializeField] string name_up;
    [SerializeField] string name_down;
    [SerializeField] string name_left;
    [SerializeField] string name_right;
    [Header("Active")]
    [SerializeField] bool active_arrow_up;
    [SerializeField] bool active_arrow_down;
    [SerializeField] bool active_arrow_left;
    [SerializeField] bool active_arrow_right;
    [Header("Trigger")]
    [SerializeField] CheckTriggerTreeGuide trigger1;
    [Header("Effect")]
    [SerializeField] GameObject electricEffect;
    Animator animator;
    bool onGuide = false;
    private void Awake()
    {
        trigger1.getTriggerState = getTriggerState;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        setDataTreeGuide();
        animator.SetTrigger("Hide");
        electricEffect.SetActive(false);
    }
    public void setDataTreeGuide()
    {
        arrow_up.SetActive(active_arrow_up);
        arrow_down.SetActive(active_arrow_down);
        arrow_left.SetActive(active_arrow_left);
        arrow_right.SetActive(active_arrow_right);
        if (name_up.Equals("")) tmpro_up.text = "";
        else tmpro_up.text = name_up;
        if (name_down.Equals("")) tmpro_down.text = "";
        else tmpro_down.text = name_down;
        if (name_right.Equals("")) tmpro_right.text = "";
        else tmpro_right.text = name_right;
        if (name_left.Equals("")) tmpro_left.text = "";
        else tmpro_left.text = name_left;
    }
    void getTriggerState(bool state)
    {
        if (state && !onGuide)
        {
            animator.SetTrigger("Show");
            onGuide = true;
            electricEffect.SetActive(true);
        }
        else if (!state)
        {
            animator.SetTrigger("Hide");
            onGuide = false;
            electricEffect.SetActive(false);
        }
    }
}
