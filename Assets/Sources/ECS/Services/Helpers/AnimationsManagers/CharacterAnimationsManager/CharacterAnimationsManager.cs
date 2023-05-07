using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationsManager : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null) 
        {
            return;
        }
    }
}
