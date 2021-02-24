using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtsCamera : MonoBehaviour
{
    float Speed = 0.06f;
    float ZoomSpeed = 10.0f;
    float RotationSpeed = 0.1f;

    float MaxHieght = 40f;
    float MinHieght = 4f;

    Vector2 P1;
    Vector2 P2;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 0.1f;
            ZoomSpeed = 25f;
        }
        else 
        {
            Speed = 0.05f;
            ZoomSpeed = 10f;
        }




        //Basic Movement
        float hsp = transform.position.y * Speed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * Speed * Input.GetAxis("Vertical");
        float ScrollSp = Mathf.Log(transform.position.y) * - ZoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if ((transform.position.y >= MaxHieght) && (ScrollSp > 0))
        {
            ScrollSp = 0;
        }
        else if ((transform.position.y <= MinHieght) && (ScrollSp < 0)) 
        {
            ScrollSp = 0;
        }
        if ((transform.position.y + ScrollSp) > MaxHieght) 
        {
            ScrollSp = MaxHieght - transform.position.y;
        }
        else if ((transform.position.y + ScrollSp) < MinHieght)
        {
            ScrollSp = MinHieght - transform.position.y;
        }




        Vector3 VerticalMove = new Vector3(0, ScrollSp, 0);
        Vector3 LateralMove = hsp * transform.right;
        Vector3 ForwardMove = transform.forward;
        //So we don't move the same rotation into the ground
        ForwardMove.y = 0;
        ForwardMove.Normalize();
        ForwardMove *= vsp;

        Vector3 move = ForwardMove + LateralMove + VerticalMove;

        transform.position += move;

        getCameraRotation();
    }
    void getCameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            P1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            P2 = Input.mousePosition;

            float dx = (P2 - P1).x * RotationSpeed;
            float dy = (P2 - P1).y * RotationSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            //Remove this for some fun
            P1 = P2;
        }

    }
}
