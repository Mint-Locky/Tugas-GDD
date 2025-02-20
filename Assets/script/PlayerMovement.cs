using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 5f;

    public Camera mainCamera;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, moveY, 0));

        //Camera follow
        if (mainCamera != null )
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);
        }
        float playerX = transform.position.x;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //flip to mouse
        if (mousePos.x < playerX)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
