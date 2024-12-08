using ActionBars;
using Survivors;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourSurvivor : ActionBar, ISurvivor
{
    private const string RunParameterName = "isRunning";
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _survivor;
    [SerializeField] private Animator _animator;

    private Transform _player;
    private Transform _target;
    private bool _isFollowing = false;
    private bool _isRunning;

    public void Initialize(Transform player, ActionSlider slider, float interaction)
    {
        _player = player;
        Initialize(slider, interaction, false);
        _target = _player;
    }

    public override Vector3 GetPosition()
    {
        return _survivor.position;
    }

    public override void OnComplete()
    {
        _isFollowing = true;
    }
    
    public void ChangeTarget(Transform target)
    {
        _target = target;
        _agent.stoppingDistance = 0.2f;
    }

    private void FixedUpdate()
    {
        Following(_target);
    }

    private void Following(Transform target)
    {
        if (_isFollowing == false)
        {
            return;
        }

        _agent.SetDestination(target.position);
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _animator.SetBool(RunParameterName, false);
            _isRunning = false;
            return;
        }

        if (_isRunning == false)
        {
            _animator.SetBool(RunParameterName, true);
            _isRunning = true;
        }
    }

}