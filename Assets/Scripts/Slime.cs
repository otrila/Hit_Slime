using UnityEngine;

public enum SlimeAnimationState { Idle, Jump, Damage }

[RequireComponent(typeof(Animator))]
public class Slime : MonoBehaviour
{
    private const string AnimationDamageEnded = "AnimationDamageEnded";
    private const string AnimationJumpEnded = "AnimationJumpEnded";
    private const string TextureName = "_MainTex";

    [SerializeField] private Face _faces;
    [SerializeField] private GameObject _smileBody;

    private Animator _animator;
    private Material _faceMaterial;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _faceMaterial = _smileBody.GetComponent<Renderer>().materials[1];

        ChangeState(SlimeAnimationState.Jump);
    }

    public void ChangeState(SlimeAnimationState newState)
    {
        switch (newState)
        {
            case SlimeAnimationState.Idle:

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                SetFace(_faces.Idleface);
                break;

            case SlimeAnimationState.Jump:

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;
                SetFace(_faces.jumpFace);
                _animator.SetTrigger("Jump");
                break;

            case SlimeAnimationState.Damage:

                SetFace(_faces.damageFace);
                _animator.SetTrigger("Damage");
                break;
        }
    }

    public void AlertObservers(string message)
    {
        if (message.Equals(AnimationDamageEnded))
            ChangeState(SlimeAnimationState.Idle);

        if (message.Equals(AnimationJumpEnded))
            ChangeState(SlimeAnimationState.Idle);
    }

    private void SetFace(Texture tex) => _faceMaterial.SetTexture(TextureName, tex);

}
