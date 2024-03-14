using UnityEngine;
public class Player : Character
{
    [SerializeField] GameObject player;
    private Transform playerTF;
    private float speed = 1.1f;
    private bool isRunning = false;
    void Start()
    {
        playerTF = player.transform;
        OnInit();
    }
    void Update()
    {
        Move();
    }
    //void Move()
    //{
    //    Vector3 direct;
    //    if(Input.GetKey(KeyCode.W))
    //    {
    //        direct = new Vector2(0, 1) * speed * Time.deltaTime;
    //        playerTF.position += direct;
    //        isRunning = true;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        direct = new Vector2(0, -1) * speed * Time.deltaTime;
    //        playerTF.position += direct;
    //        isRunning = true;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        direct = new Vector2(1, 0) * speed * Time.deltaTime;
    //        playerTF.position += direct;
    //        isRunning = true;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        direct = new Vector2(-1, 0) * speed * Time.deltaTime;
    //        playerTF.position += direct;
    //        isRunning = true;
    //    }
    //    if (isRunning)
    //    {
    //        ChangeAnim("Run");
    //    }
    //    else
    //    {
    //        ChangeAnim("Idle");
    //    }
    //    isRunning = false;
    //}
    void Move()
    {
        int moveHorizontal = 0;
        int moveVertical = 0;
        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -1;
        else if (Input.GetKey(KeyCode.D))
            moveHorizontal = 1;
        if (Input.GetKey(KeyCode.W))
            moveVertical = 1;
        else if (Input.GetKey(KeyCode.S))
            moveVertical = -1;
        if (moveHorizontal != 0 || moveVertical!=0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if(isRunning)
        {
            ChangeAnim("Run");
        }
        else
        {
            ChangeAnim("Idle");
        }
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized * speed * Time.deltaTime;
        transform.position += movement;
    }
}
