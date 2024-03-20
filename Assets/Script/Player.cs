using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField] GameObject player;
    private Transform playerTF;
    private float speed = 1.1f;
    private bool isRunning = false;
    private void OnDisable()
    {
        ResetCharacter();
        ChangeAnim("Idle");
    }
    void Start()
    {
        playerTF = player.transform;
        OnInit(100);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
        if (isDead)
        {
            return;
        }
        if (isHurting)
        {
            return;
        }
        Move();
    }
    void Show(int t)
    {
        Debug.Log(t);
    }
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
    protected override void OnDeath()
    {
        base.OnDeath();
        BulletController.Instance.isDead = isDead;
    }
    protected override void OnHurting(float damage)
    {
        base.OnHurting(damage);
    }
}
