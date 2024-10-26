using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 5f;  // 移動スピード
    [SerializeField] private LayerMask solidObjects;

    private PlayerActions actions;
    private bool isMoving = false;
    private Vector2 moveDirection;
    private Vector2 targetPos;  // 目標位置を保持
    private Animator animator;

    public static SubPlayerController instance;

    //ロードするときにどこのAreaから来たか文字列を参照する
    public string areaTransitionName;

    private void Awake()
    {
        actions = new PlayerActions();
        animator = GetComponent<Animator>();

        // 初期のターゲット位置はプレイヤーの現在位置
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
        // プレイヤーが目標位置に到達しているかを確認
        if (!isMoving)
        {
            ReadMovement();

            if (moveDirection != Vector2.zero)
            {
                animator.SetFloat("moveX", moveDirection.x);
                animator.SetFloat("moveY", moveDirection.y);

                // 次の目標位置を決定
                targetPos = (Vector2)transform.position + moveDirection;

                if (IsWalkable(targetPos))
                {
                    isMoving = true;  // 移動状態に変更
                }
            }
        }
        else
        {
            // 移動処理
            MoveToTarget();
        }

        // アニメーション更新
        animator.SetBool("IsMoving", isMoving);
    }
    private void MoveToTarget()
    {
        // プレイヤーを徐々に目標位置に移動
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // 目標位置に到達したかを確認
        if (Vector2.Distance(transform.position, targetPos) < Mathf.Epsilon)
        {
            // 到達したら移動を終了
            transform.position = targetPos;
            isMoving = false;
        }
    }
    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>();

        // 入力を四捨五入して1マスずつの移動にする
        moveDirection.x = Mathf.Round(moveDirection.x);
        moveDirection.y = Mathf.Round(moveDirection.y);

        // 斜め移動を防ぐ
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
