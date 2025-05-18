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
    
    //Mobile
    int up_Value;
    int down_Value; 
    int left_Value; 
    int right_Value;
    
    bool up_Down; 
    bool down_Down; 
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //PC
        h = Manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = Manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;
   
        //pc
        bool hDown = Manager.isAction ? false : Input.GetButtonDown ("Horizontal") ||right_Down || left_Down;
        bool vDown = Manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = Manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = Manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;
        
   
        
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
        
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
        

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
    
    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            
            case "A":
                if (scanObject != null)
                    Manager.Action(scanObject);
                break;
            case "C":
                Manager.SubMenuActive();
                break;
 
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;

        }
    }

}