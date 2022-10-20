using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField][Range(0f, 1000f)] private float JumpForce;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private bool Grounded = false;
    public Rigidbody2D rb;

    private float DirectX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // movement
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        transform.Translate(DirectX, 0f, 0f);



        //move camera with player on y axis
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }

        //move camer if player goes under -4 on y axis
        if (transform.position.y < Camera.main.transform.position.y - 4f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }





        //screen edge teleprort
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + (Camera.main.scaledPixelWidth / 2)) 
        {
            transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.scaledPixelWidth / 2), transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.x <= Camera.main.transform.position.x - (Camera.main.scaledPixelWidth / 2))
        {
            transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.scaledPixelWidth / 2), transform.position.y, transform.position.z);
        }

        //if grounded jump
        if (Grounded == true)
        {            
            Grounded = false;
            rb.AddForce(transform.up * JumpForce  );
            //rb.velocity = new Vector2(rb.velocity.x, JumpForce);

            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag=="Ground")
        {
            Grounded = true;
            Debug.Log("Grounded");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Grounded = false;
            Debug.Log("exit");
        }
    }


}
