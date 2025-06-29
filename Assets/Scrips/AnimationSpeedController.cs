using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator animator;
    [Range(0f, 2f)] public float animationSpeed = 1f;

    void Start()
    {
        if (animator != null)
        {
            animator.speed = animationSpeed;
        }
    }

    void Update()
    {
        // Для динамического изменения (например, при нажатии клавиш)
        //animator.speed = animationSpeed;
    }
}
