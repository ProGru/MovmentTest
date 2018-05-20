using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FisrtPlayerMovment : MonoBehaviour {

    public float speed = 10.0F;
    public bool terainInBox = false;
    public float maxY,maxX,maxZ = 80;
    public float minY,minX,minZ = 0;
    public float jumpPower= 0.5f;
    public float timeBetwenJumps = 1;
    public bool groundJump = true;
    public bool lockCursor = true;
    float jumpTime = -1;
    Rigidbody _rigidbody;
    Rect position;
    public Texture2D crosshairTExture;



    void Start () {
        position = new Rect((Screen.width - crosshairTExture.width) / 2, (Screen.height - crosshairTExture.height) / 2, crosshairTExture.width, crosshairTExture.height);
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            MakeJump();
        }
        
        transform.Translate(straffe, 0, translation);
        if (terainInBox)
        {
            BoxTerrain();
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}

    void BoxTerrain()
    {
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }


        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
    }

    void MakeJump()
    {
        if (groundJump)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                _rigidbody.AddForce(Vector3.up * jumpPower * 100, ForceMode.Impulse);
            }
        }
        else
        {
            if (jumpTime < Time.time - timeBetwenJumps)
            {
                jumpTime = Time.time;
                _rigidbody.AddForce(Vector3.up * jumpPower * 100, ForceMode.Impulse);
            }
        }

    }
    private void OnGUI()
    {
        if (lockCursor)
        {
            GUI.DrawTexture(position, crosshairTExture);
        }
    }
}
