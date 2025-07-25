using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    float _speed;

    Vector2 _input;

    Rigidbody2D _rigid;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    Tilemap _interactableTilemap;
    PlayerInput _playerInput;

    InputAction _interactAction;

    static readonly int _isMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _interactableTilemap = GameObject.FindGameObjectWithTag("Interactable").GetComponent<Tilemap>();
        _playerInput = GetComponent<PlayerInput>();
        _speed = 5;
        _interactAction = _playerInput.currentActionMap.FindAction("Interact");
    }


    private void FixedUpdate()
    {
        Vector2 movement = _input * _speed * Time.fixedDeltaTime;

        if (movement ==  Vector2.zero)
        {
            _animator.SetBool(_isMoving, false);
            return;

        }


        _rigid.MovePosition(_rigid.position + movement);
        _animator.SetBool(_isMoving, true);

        

    }

    private void LateUpdate()
    {
        if (_input.x != 0)
        {
            _spriteRenderer.flipX = _input.x > 0;
        }
    }


    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
        
    }

    private void OnInteract(InputValue value)
    {

        Vector3Int cellPos = _interactableTilemap.WorldToCell(transform.position);
        InteractableTile tile = _interactableTilemap.GetTile(cellPos) as InteractableTile;
        Debug.Log(cellPos);

        if (tile == null)
        {
            return;
        }

        tile.Interact();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            Vector3Int cellPos = _interactableTilemap.WorldToCell(collision.bounds.center);
            InteractableTile tile = _interactableTilemap.GetTile(cellPos) as InteractableTile;

            if (tile == null)
            {
                return;
            }

            UIManager.Instance.SetDescString(tile.InteractionType.ToString());

            Vector3 screenPos = Camera.main.WorldToScreenPoint(collision.bounds.center + new Vector3(2f, 0, 0));

            UIManager.Instance.PopUpInteractionUI(screenPos);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            UIManager.Instance.CloseInteractionUI();

        }
    }
}
