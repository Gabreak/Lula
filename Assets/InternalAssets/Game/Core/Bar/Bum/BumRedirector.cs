using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BumRedirector : MonoBehaviour
{
    [Required]
    public Animator BumAnimator { get; set; }
    public RandomBum BumRandom { get; set; }
    public Collider2D BumCollider { get; set; }

    private void Start()
    {
        BumAnimator = GetComponent<Animator>();
        BumRandom = GetComponent<RandomBum>();
        BumCollider = GetComponent<Collider2D>();
    }

    public void ComponentDisable()
    {
        BumRandom.enabled = false;
        BumCollider.enabled = false;
    }
}
