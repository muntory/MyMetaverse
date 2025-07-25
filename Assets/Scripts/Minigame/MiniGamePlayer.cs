using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGamePlayer : MonoBehaviour
{
    [SerializeField]
    private float _flapForce = 6f;
    [SerializeField]
    private float _forwardSpeed = 4f;

    private bool _isDead = false;
    static readonly int IS_DEAD = Animator.StringToHash("IsDead");
    public event Action OnPlayerDead;

    public event Action<int> OnScoreChanged;

    private int _currentScore = 0;

    Animator _animator;
    Rigidbody2D _rigid;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        _rigid.velocity = new Vector2(_forwardSpeed, _rigid.velocity.y) ;

    }

    private void LateUpdate()
    {
        float angle = Mathf.Clamp(_rigid.velocity.y * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void OnFlap(InputValue value)
    {
        if (_isDead) return;
        Debug.Log("flap");
        _rigid.velocity += new Vector2(0, _flapForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;

        _isDead = true;
        _animator.SetBool(IS_DEAD, true);

        StartCoroutine("LoadGameOverUI");
    }


    public void AddScore(int score)
    {
        if (_isDead) return;
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);
    }

    IEnumerator LoadGameOverUI()
    {
        GameManager.Instance.SetCurrentScore(_currentScore);
        yield return new WaitForSeconds(2f);
        OnPlayerDead?.Invoke();

    }

    
}
