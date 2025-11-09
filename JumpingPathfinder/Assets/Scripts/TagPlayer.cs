using System.Collections.Generic;
using UnityEngine;

public class TagPlayer : MonoBehaviour
{
    [SerializeField] private Material taggedIndicatorMaterial;
    private bool tagged;

    private const float tagBackCooldown = 1;
    private float tagBackTimer;
    private bool canBeTagged = true;

    void OnEnable()
    {
        taggedIndicatorMaterial.SetFloat("_Outline_Thickness", 0);
        TagGameController.AddTagPlayer(this);
    }

    void Update()
    {
        TagBackTimerTick();
    }

    private void TagBackTimerTick()
    {
        if(!canBeTagged)
        {
            tagBackTimer -= Time.deltaTime;
            if(tagBackTimer <= 0)
            {
                canBeTagged = true;
            }
        }
    }



    public bool Tag()
    {
        if(canBeTagged)
        {
            tagged = true;
            taggedIndicatorMaterial.SetFloat("_Outline_Thickness", 0.1f);
        }
        return tagged;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tag Player" && tagged)
        {
            if(collision.gameObject.GetComponent<TagPlayer>().Tag())
            {
                tagged = false;
                taggedIndicatorMaterial.SetFloat("_Outline_Thickness", 0);
                canBeTagged = false;
                tagBackTimer = tagBackCooldown;
            }
        }
    }
}
