// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
//
//
// //This script is showing setup of simple custom variable tracking
// public class AnimatorParameter : RewindAbstract           
// {
//
//     private void Start()
//     {
//         _animator = GetComponent<Animator>();
//         _parameterBuffer = new CircularBuffer<float>();        //Circular buffer must be initialized in start method, it cannot use field initialization
//     }
//
//     //In this method define what will be tracked. In our case we want only track our custom added variable scale tracking
//     protected override void Track()
//     {
//         TrackObjectScale();      
//     }
//
//
//     //In this method define, what will be restored on time rewinding. In our case we want to restore object scale
//     protected override void Rewind(float seconds)
//     {
//         RestoreObjectScale(seconds);
//     }
//
//
//     // This is an example of custom variable tracking
//     public void TrackObjectScale()
//     {
//         _parameterBuffer.WriteLastValue(_animator.GetFloat(parameter));
//     }
//
//     
//     // This is an example of custom variable restoring
//     public void RestoreObjectScale(float seconds)
//     {
//         _animator.SetFloat(parameter, _parameterBuffer.ReadFromBuffer(seconds));
//
//     }
//
//     //Just for demonstration object scaling was chosen, otherwise it would be probably better to track and restore slider value, which would then also update the object scale accordingly
// }
