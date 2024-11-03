using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public Animator animator;
    //이동
    public float moveSpeed = 2f;
    public float runSpeed = 5f;
    public float justSpeed = 2f;
    //공격
    public bool attacked = false;
    public bool Skill1 = false;
    public Transform pos;
    public Transform pos1; //스킬
    public Vector2 boxSize;
    public Vector2 SkillboxSize;
    Rigidbody2D rigid;
    
    private float curtime;
    public float cooltime = 0.5f;

    void AttackTrue()//평타
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void Skill1True()//스킬1
    {
        Skill1 = true;
    }
    void Skill1False()
    {
        Skill1 = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(AttackAction());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(AttackStrike());
        }
        Move();


    }
    IEnumerator AttackAction()// 평타
    {
        animator.SetTrigger("IsAttack");

        //yield return null; //업데이트 한번 하고 온다.
        yield return new WaitForSeconds(0.3f);//매개변수 시간 만큼 업데이트하고 온다.

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }
    IEnumerator AttackStrike()//스킬 1
    {
        animator.SetTrigger("Skill1");
        yield return new WaitForSeconds(0.3f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position, SkillboxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(3);

            }
        }
    }

    private void OnDrawGizmos()//공격 범위
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);

        Gizmos.DrawWireCube(pos1.position, SkillboxSize);

    }
    void Move()//이동
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //쉬프트를 누르면 movespeed가 설정해놓은 runspeed가 된다
            moveSpeed = runSpeed; 
        }
        else
        {
            moveSpeed = justSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //A를 누르면 OnMoving animation이 활성화 되며 캐릭터의 X좌표가 이동된다
            animator.SetBool("OnMoving", true);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //D를 누르면 OnMoving animation이 활성화 되며 캐릭터의 X좌표가 이동된다
            animator.SetBool("OnMoving", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            //이동이 없을때 OnMoving animation을 끈다
            animator.SetBool("OnMoving", false);
        }
        

    } 
    
}
