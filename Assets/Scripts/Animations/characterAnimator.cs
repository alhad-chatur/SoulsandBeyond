using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterAnimator : MonoBehaviour
{
    private Animator _animator;
    private RewindAbstract _rewind;

    private static readonly int StateHash = Animator.StringToHash("State");
    private static readonly int ForwardSpeedHash = Animator.StringToHash("SpeedFactor");
    private static readonly int JumpStateHash = Animator.StringToHash("IsFalling");
    ///<summary>
    /// active state have 3 modes 1.idle-run 2.push 3.jump
    ///</summary>
    public float activeState = 0;
    public float speedTarget = 0;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rewind = GetComponent<RewindAbstract>();
        
    }

    private void FixedUpdate()
    {
        if (_rewind != null && !_rewind.IsTracking)
        {
            
            return;
        }

        CheckForDeath();
        #region active State
        
        float currentValue = _animator.GetFloat(StateHash);
        float newValue = Mathf.Lerp(currentValue, (int)activeState, 0.1f);
        _animator.SetFloat(StateHash, newValue);

        #endregion
        
        #region Speed Var
        currentValue = _animator.GetFloat(ForwardSpeedHash);
        newValue = Mathf.Lerp(currentValue, speedTarget, 0.1f);
        _animator.SetFloat(ForwardSpeedHash, newValue);
        #endregion

        #region jump State

        currentValue = _animator.GetFloat(JumpStateHash);
        newValue = Mathf.Lerp(currentValue, 1, 0.01f);
        _animator.SetFloat(JumpStateHash, newValue);

        #endregion
    }

    private void CheckForDeath()
    {
        
    }

    public void Jump()
    {
        _animator.SetFloat(JumpStateHash,0);
    }
    
}
