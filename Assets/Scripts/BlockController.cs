using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    int Health = 1;

    int Score = 5;

    [SerializeField] bool SpecialFrameGlow = false;

    [SerializeField] List<Material> HPMaterials = new List<Material>();

    protected bool Destroyed = false;

    float Speed = 2f;
    float MoveDuration = 1f;
    private Vector3 TargetPosition;
    private bool IsMoving = false;

    [SerializeField] Renderer Renderer;
    [SerializeField] Renderer FrameRenderer;
    Rigidbody Rigidbody;

    bool firstMove = true;

    private Vector3 moveOffset = new Vector3(0, -1.5f, 0);

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(int health)
    {
        Health = health;

        if ((Health - 1) < HPMaterials.Count)
            Renderer.material = HPMaterials[Health - 1];

        if(SpecialFrameGlow)
        {
            FrameRenderer.material = EffectsManager.GetInstance().GetSpecialGlowMat();
        }
    }

    private void Update()
    {
        if(transform.position.y < -2f)
        {
            GameController.GetInstance().BlockReachedBorder();
        }
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.fixedDeltaTime);
            Rigidbody.MovePosition(newPosition);

            if (Vector3.Distance(transform.position, TargetPosition) < 0.01f)
            {
                IsMoving = false;
            }
        }
    }

    public int GetScore()
    {
        return Score;
    }

    public void Damage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Break();
        }
        else
        {
            if ((Health - 1) < HPMaterials.Count)
                Renderer.material = HPMaterials[Health - 1];
        }
    }

    public virtual void Break()
    {
        if (Destroyed)
            return;

        Destroyed = true;
        GameController.GetInstance().DestroyedBlock(this);

        Destroy(gameObject);
    }

    public void StartMove()
    {
        if (firstMove)
        {
            TargetPosition = transform.position;

            TargetPosition += new Vector3(0f, -4.5f, 0f);
            firstMove = false;
        }
        else
        {
            TargetPosition += moveOffset;
        }

        float distance = Vector3.Distance(transform.position, TargetPosition);
        Speed = distance / MoveDuration;
        IsMoving = true;
    }
}
