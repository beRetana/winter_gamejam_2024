using UnityEngine;
using System.Collections;

public class BallBackHoleMovement : MonoBehaviour
{
    private const string WALL_TAG = "Wall";

    [SerializeField] private float speed;
    private float speedMultiplier = 4f;

    private float og_speed;



    private int direction = 1;
    private  void Awake()
    {
        og_speed = speed;
    }

    private void Update()
    {
        
        if (Input.GetMouseButton(1) && 
            DataMessenger.GetBool(BoolKey.IsBallInPlay) == false) 
        // if you hold down right click speed increases
        {
            speed = og_speed * speedMultiplier;
        }
        else
        {
            speed = og_speed;
        }
        transform.position += speed * Time.deltaTime * direction * Vector3.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(WALL_TAG))
        {
            direction = -direction;
        }
    }

}
