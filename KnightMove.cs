using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public Animator animator;
    //�̵�
    public float moveSpeed = 2f;
    public float runSpeed = 5f;
    public float justSpeed = 2f;
    //����
    public bool attacked = false;
    public bool Skill1 = false;
    public Transform pos;
    public Transform pos1; //��ų
    public Vector2 boxSize;
    public Vector2 SkillboxSize;
    Rigidbody2D rigid;
    
    private float curtime;
    public float cooltime = 0.5f;

    void AttackTrue()//��Ÿ
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void Skill1True()//��ų1
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
    IEnumerator AttackAction()// ��Ÿ
    {
        animator.SetTrigger("IsAttack");

        //yield return null; //������Ʈ �ѹ� �ϰ� �´�.
        yield return new WaitForSeconds(0.3f);//�Ű����� �ð� ��ŭ ������Ʈ�ϰ� �´�.

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }
    IEnumerator AttackStrike()//��ų 1
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

    private void OnDrawGizmos()//���� ����
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);

        Gizmos.DrawWireCube(pos1.position, SkillboxSize);

    }
    void Move()//�̵�
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //����Ʈ�� ������ movespeed�� �����س��� runspeed�� �ȴ�
            moveSpeed = runSpeed; 
        }
        else
        {
            moveSpeed = justSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //A�� ������ OnMoving animation�� Ȱ��ȭ �Ǹ� ĳ������ X��ǥ�� �̵��ȴ�
            animator.SetBool("OnMoving", true);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //D�� ������ OnMoving animation�� Ȱ��ȭ �Ǹ� ĳ������ X��ǥ�� �̵��ȴ�
            animator.SetBool("OnMoving", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            //�̵��� ������ OnMoving animation�� ����
            animator.SetBool("OnMoving", false);
        }
        

    } 
    
}
