using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 5f;  // �ړ��X�s�[�h
    [SerializeField] private LayerMask solidObjects;

    private PlayerActions actions;
    private bool isMoving = false;
    private Vector2 moveDirection;
    private Vector2 targetPos;  // �ڕW�ʒu��ێ�
    private Animator animator;

    public static SubPlayerController instance;

    //���[�h����Ƃ��ɂǂ���Area���痈������������Q�Ƃ���
    public string areaTransitionName;

    private void Awake()
    {
        actions = new PlayerActions();
        animator = GetComponent<Animator>();

        // �����̃^�[�Q�b�g�ʒu�̓v���C���[�̌��݈ʒu
        targetPos = transform.position;

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

    // Update is called once per frame
    void Update()
    {
        // �v���C���[���ڕW�ʒu�ɓ��B���Ă��邩���m�F
        if (!isMoving)
        {
            ReadMovement();

            if (moveDirection != Vector2.zero)
            {
                animator.SetFloat("moveX", moveDirection.x);
                animator.SetFloat("moveY", moveDirection.y);

                // ���̖ڕW�ʒu������
                targetPos = (Vector2)transform.position + moveDirection;

                if (IsWalkable(targetPos))
                {
                    isMoving = true;  // �ړ���ԂɕύX
                }
            }
        }
        else
        {
            // �ړ�����
            MoveToTarget();
        }

        // �A�j���[�V�����X�V
        animator.SetBool("IsMoving", isMoving);
    }
    private void MoveToTarget()
    {
        // �v���C���[�����X�ɖڕW�ʒu�Ɉړ�
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // �ڕW�ʒu�ɓ��B���������m�F
        if (Vector2.Distance(transform.position, targetPos) < Mathf.Epsilon)
        {
            // ���B������ړ����I��
            transform.position = targetPos;
            isMoving = false;
        }
    }
    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>();

        // ���͂��l�̌ܓ�����1�}�X���̈ړ��ɂ���
        moveDirection.x = Mathf.Round(moveDirection.x);
        moveDirection.y = Mathf.Round(moveDirection.y);

        // �΂߈ړ���h��
        if (moveDirection.x != 0)
        {
            moveDirection.y = 0;
        }
    }
    private bool IsWalkable(Vector2 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.2f, solidObjects);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
