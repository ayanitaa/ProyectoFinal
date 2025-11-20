using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public float attackDuration = 0.6f;

    public bool IsAttacking { get; private set; }

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (IsAttacking) return;

        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        IsAttacking = true;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        IsAttacking = false;
    }
}
