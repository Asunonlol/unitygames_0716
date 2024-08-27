using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContloller : MonoBehaviour
{
    Animation animation;
    //重力
    Rigidbody2D rigidbody2D;
    //ジャンプするときの力
    float jumpForce = 680.0f;
    //移動に関する力
    float walkFrce = 30.0f;
    float maxWalkSpeed = 2.0f;
    //アニメーション
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //フレームカウント
        Application.targetFrameRate = 60;
        //Rigdbodyをコンポーネント
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
        //左右移動
        int key = 0;
        if(Input.GetKey/*Down*/(KeyCode.RightArrow))
        {
            key = 1;
        }
        if (Input.GetKey/*Down*/(KeyCode.LeftArrow))
        {
            key = -1;
        }
        //プレイヤー速度
        float speedx = Mathf.Abs(this.rigidbody2D.velocity.x);


        //スピード制限
        if (speedx < this.maxWalkSpeed)
        {
            this.rigidbody2D.AddForce(transform.right * key * this.walkFrce);
        }
        //動く方向で反転される
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
    //ゴールに到達
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Goal");
        SceneManager.LoadScene("GoalScene");
    }
}
