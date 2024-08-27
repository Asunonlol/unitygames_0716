using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContloller : MonoBehaviour
{
    Animation animation;
    //�d��
    Rigidbody2D rigidbody2D;
    //�W�����v����Ƃ��̗�
    float jumpForce = 680.0f;
    //�ړ��Ɋւ����
    float walkFrce = 30.0f;
    float maxWalkSpeed = 2.0f;
    //�A�j���[�V����
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //�t���[���J�E���g
        Application.targetFrameRate = 60;
        //Rigdbody���R���|�[�l���g
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rigidbody2D.velocity.y==0&&Input.GetKeyDown(KeyCode.Space))
        {
            this.rigidbody2D.AddForce(transform.up * this.jumpForce);
            animator.SetBool("isJumping", true);
        }
        else if(this.rigidbody2D.velocity.y == 0) 
        { animator.SetBool("isJumping", false); }
        //���E�ړ�
        int key = 0;
        if(Input.GetKey/*Down*/(KeyCode.RightArrow))
        {
            key = 1;
        }
        if (Input.GetKey/*Down*/(KeyCode.LeftArrow))
        {
            key = -1;
        }
        //�v���C���[���x
        float speedx = Mathf.Abs(this.rigidbody2D.velocity.x);


        //�X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigidbody2D.AddForce(transform.right * key * this.walkFrce);
        }
        //���������Ŕ��]�����
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        this.animator.speed = speedx/1.0f;
        if(transform.position.y<-10||transform.position.x<-6||transform.position.x>6)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if(transform.position.y>17)
        {
            this.transform.position=new Vector3(transform.position.x,17,transform.position.z);
        }
    }
    //�S�[���ɓ��B
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Goal");
        SceneManager.LoadScene("GoalScene");
    }
}
