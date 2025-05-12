using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed = 5f; // 기본 속도 값 설정 (원한다면 Inspector에서 조절 가능)
    public GameManager Manager;
    Rigidbody2D rigid;
    private Animator anim;
    float h;
    float v;
    bool isHorizonMove;
    private Vector3 dirVec;
    private GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = Manager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        
        bool hDown = Manager.isAction ? false : Input.GetButtonDown ("Horizontal");
        bool vDown = Manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = Manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = Manager.isAction ? false : Input.GetButtonUp("Vertical");
        
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;
        
        if (anim. GetInteger ("hAxisRaw") != h) {
            anim.SetInteger ("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v) {
            anim. SetInteger("vAxisRaw", (int)v);
        }
        
        //Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3. down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;
        
        //scan
        if (Input.GetButtonDown("Jump") && scanObject != null)
            Manager.Action(scanObject);
        

    }

    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.linearVelocity = moveVec * Speed;
        //Ray
    
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color (0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        
        
        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
        
        
    }
}