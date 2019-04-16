using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndofMatchAnimations : MonoBehaviour
{

    Animator animator;
    public AnimationClip[] winAnims;
    public AnimationClip[] loseAnims;
    public bool isWinner = true;
    public int animNo;
    [SerializeField]
    bool setRando = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        animNo = Random.Range(0, winAnims.Length);
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BaseState"))
        {
            if (isWinner == true)
            {
                if (setRando == true)
                {
                   //  print("set random int");
                    animNo = Random.Range(0, winAnims.Length);
                    setRando = false;
                }
                animator.Play(winAnims[animNo].name);
            }
            else
            {
                
                if (setRando == true)
                {
                   // print("set random int");
                    animNo = Random.Range(0, loseAnims.Length);
                    setRando = false;
                }
                animator.Play(loseAnims[animNo].name);
                
            }
        }
        else
        {
            //We reset the random counter whenever we leave basestate
            if (setRando == false)
            {
                setRando = true;
            }
        }
    }
}
