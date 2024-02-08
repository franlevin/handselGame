using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CollisionHandler.PlayerDeath += DeathAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeathAnimation(){
        animator.SetBool("isDead", true);
    }
}
