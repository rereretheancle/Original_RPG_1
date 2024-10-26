using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    private PlayerActions actions;

    public bool isMoving;
    [SerializeField] private Rigidbody2D rb2D;
    public Vector2 moveDirection;
    private Animator animator;

    //�ǔ���̃��C���[
    [SerializeField] LayerMask solidObjects;

    //���E�Ɉ��������Player�͂Ȃ�
    public static PlayerController instance;

    //���[�h����Ƃ��ɂǂ���Area���痈������������Q�Ƃ���
    public string areaTransitionName;

    private void Awake()
    {
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isMoving = false;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            ReadMovement();

            if (moveDirection.x != 0)
            {
                moveDirection.y = 0;
            }

            if (moveDirection != Vector2.zero)
            {
                animator.SetFloat("moveX", moveDirection.x);
                animator.SetFloat("moveY", moveDirection.y);

                Vector2 targetPos = transform.position;
                targetPos += moveDirection;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("IsMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //targetPos�܂ŋ߂Â���
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>(); //moveDirection��Vector2�^

        moveDirection.x = Mathf.Round(moveDirection.x);
        moveDirection.y = Mathf.Round(moveDirection.y);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    bool IsWalkable(Vector2 targetPos)
    {
        bool hit = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjects);
        return !hit;
    }
}
