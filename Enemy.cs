using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pos;
    public Vector2 boxSize;
    public float speed;
    public int Hp = 3;
    public int MaxHp = 3;
    public int atk = 1;
    public static int dropxp = 1;
    public float stopDistance;
    public float stopEnemyDistance;
    private Transform target;
    Animator animator;
    GameObject GameObject;


    

    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        //Å¸°Ù Ãß°Ý
        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("OnMoving", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("OnMoving", false);
        }

    }
    public void TakeDamage(int damage)
    {
        

        Hp = Hp - damage;

        
        if (Hp > 0)
        {
            animator.SetTrigger("IsDamage");
            
        }
        else 
        {
            

            Playerxp.nowxp += dropxp;
            Destroy(gameObject, 1f);
            animator.SetTrigger("IsDying");
            if (Playerxp.nowxp > Playerxp.maxxp)
            {
                Playerxp.maxxp += 10;
                Playerxp.nowxp = 0;
            }
            
            
            
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
