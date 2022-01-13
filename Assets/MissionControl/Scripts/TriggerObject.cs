using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public Animator anim;
    private GameObject cam;

    public GameObject triggerTextPositive;
    public GameObject triggerTextNegative;

    public bool useAnimation = false;
    public string AnimationBool;
    public string AnimationTrigger;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        MouseOver();
        MouseDown();
    }

    private void MouseDown()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (Input.GetMouseButtonDown(0))
            {
                if (useAnimation)
                {
                    if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false)
                    {
                        anim.SetBool(AnimationBool, true);
                        anim.SetTrigger(AnimationTrigger);
                    }
                    else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == true)
                    {
                        anim.SetBool(AnimationBool, false);
                        anim.SetTrigger(AnimationTrigger);
                    }
                }
            }
        }
    }

    private void MouseOver()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (useAnimation)
            {
                if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == false)
                {
                    triggerTextNegative.SetActive(false);
                    triggerTextPositive.SetActive(true);
                }
                else if (objectHit.gameObject == gameObject && anim.GetBool(AnimationBool) == true)
                {
                    triggerTextNegative.SetActive(true);
                    triggerTextPositive.SetActive(false);
                }
            }
            else
            {
                if (objectHit.gameObject == gameObject)
                {
                    triggerTextPositive.SetActive(true);
                }
            }
        }
        else
        {
            triggerTextPositive.SetActive(false);
            triggerTextNegative.SetActive(false);
        }
    }
}
