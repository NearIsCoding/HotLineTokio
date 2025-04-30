using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    void Awake()
    {
        instance = this;
    }

    [HideInInspector] public bool canMove = true;

    public LifeManager vida;
    public event EventHandler Muerte;
    public Animator animator;

    public Animator Camara;

    public bool isWalking = false;
    Rigidbody2D rigidbody;
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float weaponRange = 10f;


    [SerializeField] public Transform checkPoint;
    [HideInInspector] public bool hasCheckpoint;
    private Vector3 initialPosition;


    void Start()
    {
        Camara.SetTrigger("CamShoot");
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            MoveCharacter();
        }


        if (vida.vidaAct <= 0)
        {



            Muerte?.Invoke(this, EventArgs.Empty);


            StartCoroutine(Respawn());


        }
    }





    private void MoveCharacter()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidbody.velocity = input.normalized * movementSpeed;

        if (rigidbody.velocity.x != 0 || rigidbody.velocity.y != 0)
        {
            animator.SetBool("isWalking", true);
            isWalking = true;
        }
        else
        {
            animator.SetBool("isWalking", false);
            isWalking = false;
        }
    }




    public void SetCheckpoint(Transform newCheckpoint)
    {
        checkPoint = newCheckpoint;
        hasCheckpoint = true;
    }

    private IEnumerator Respawn()
    {

        yield return new WaitForSeconds(0.5f); // Peque�o delay para reiniciar correctamente



        if (hasCheckpoint && checkPoint != null)
        {

            transform.position = checkPoint.position;

        }
        else
        {
            transform.position = initialPosition; // Respawn en la posici�n inicial si no hay checkpoint
        }
        vida.vidaAct = 3; // Asegura que el jugador reaparece con la vida llena
    }

}
