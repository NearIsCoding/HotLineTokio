using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float shootCooldown = 0.5f;
    private float shootCooldownTimer = 0f;
    bool isWalking = false;
    Rigidbody2D rigidbody;
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float weaponRange = 10f;
    [SerializeField] private Animator muzzleFlashAnimator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shootCooldownTimer -= Time.deltaTime;
        FollowMouse();
        MoveCharacter();
        Shoot();
        print(isWalking);
    }

    private void FollowMouse()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.right = (mousePos - new Vector2(transform.position.x, transform.position.y));

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MoveCharacter()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidbody.velocity = input.normalized * movementSpeed;
        if ((rigidbody.velocity.x != 0) || (rigidbody.velocity.y != 0)) 
        { 
            isWalking = true;

        } 
        else { 
            isWalking = false; 
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && shootCooldownTimer <= 0f)
        {
            shootCooldownTimer = shootCooldown;

            //muzzleFlashAnimator.SetTrigger("Shoot");
            var hit = Physics2D.Raycast(
                gunPoint.position,
                transform.right,
                weaponRange
                );

            var trail = Instantiate(
                bulletTrail,
                gunPoint.position,
                transform.rotation
                );

            var trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                //var hittable = hit.collider.GetComponent<IHittable>();
                //hittable.hit();
            }
            else
            {
                var endPosition = gunPoint.position + transform.right * weaponRange;
                trailScript.SetTargetPosition(endPosition);
            }
        }
    }


}
