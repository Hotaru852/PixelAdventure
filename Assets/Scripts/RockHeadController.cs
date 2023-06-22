using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHeadController : MonoBehaviour
{
    [SerializeField] private Vector2 Direction;
    [SerializeField] private LayerMask Obstacle;
    [SerializeField] private bool ClockWise;
    [SerializeField] private float Acceleration;

    private BoxCollider2D bc;
    private Animator anim;

    private float Speed;

    private enum State { HitLeft, HitRight, HitTop, HitBottom, Idle };

    // Start is called before the first frame update
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("State", 4);
    }

    // Update is called once per frame
    private void Update()
    {
        Speed += Acceleration * Time.deltaTime;

        if (Hit())
        {
            UpdateAnimation();

            Speed = 0;

            if (ClockWise)
            {
                Direction = new Vector2(Direction.y, Direction.x * -1);
            }
            else Direction *= -1;
        }

        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        if (Direction == Vector2.left)
        {
            anim.SetInteger("State", 0);
        }
        else if (Direction == Vector2.right)
        {
            anim.SetInteger("State", 1);
        }
        else if (Direction == Vector2.up)
        {
            anim.SetInteger("State", 2);
        }
        else
        {
            anim.SetInteger("State", 3);
        }
    }

    private bool Hit()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Direction, .1f,  Obstacle);
    }

    private void Idle()
    {
        anim.SetInteger("State", 4);
    }
}
