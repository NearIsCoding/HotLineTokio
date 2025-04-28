using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public static GunShoot instance;
    void Awake()
    {
        instance = this;
    }


    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float weaponRange = 10f;
    public Animator Camara;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        Shoot();







    }

    private void FollowMouse()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.right = (mousePos - new Vector2(transform.position.x, transform.position.y));

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
            

        {
            Camara.SetTrigger("CamHit");
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
