using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    [SerializeField] private LayerMask platformsLayerMask;
    public float speed = 6.0f;
    private bool walking = false;
    public Vector2 lastMovment = Vector2.zero;
    
    //variable constante que no cambia
    private const string AXIS_H = "Horizontal"; 

    //referencias
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private BoxCollider2D _boxCollider2d;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        walking = false;
        // S=V*t --> dar el valor exacto de cuanto se ha movido
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            Vector3 translation = new Vector3(
                Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            this.transform.Translate(translation);
            walking = true;
            lastMovment = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("me han hecho daño");
            _animator.SetTrigger("Get_Hit");
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 25f;
            _rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }


    }
    private void LateUpdate()
    {
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        
        _animator.SetBool("Walking", walking);
        _animator.SetFloat("Last_H", lastMovment.x);
    }
    private bool IsGrounded()
    {
       RaycastHit2D raycastHit2d = Physics2D.BoxCast(_boxCollider2d.bounds.center, _boxCollider2d.bounds.size,
                          0f, Vector2.down, 1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }
}
