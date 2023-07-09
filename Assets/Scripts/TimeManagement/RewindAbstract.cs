using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RewindAbstract : MonoBehaviour
{
    public RewindManager rewindManager;
    public bool IsTracking { get; set; } = false;

    Rigidbody body;
    Rigidbody2D body2;
    Animator animator;
    AudioSource audioSource;
    private onpressplat _onpressplat;
    [Tooltip("Fill particle settings only if you check Track Particles")]
    [SerializeField] ParticlesSetting particleSettings;

    
    
    [SerializeField] bool trackPositionRotation;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackParticles;
    [SerializeField] bool physicsButton;
    [SerializeField] bool trackIsDying1;
    [SerializeField] bool trackIsDying2;
    [SerializeField] bool trackPlatforms;


    protected void Awake()
    {
        Time.timeScale = 1;
        if (physicsButton)
        {
            _physicsButton = GetComponent<PhysicsButton>();
        }

        if (trackPlatforms)
        {
            _onpressplat = GetComponent<onpressplat>();
        }
        
        if (rewindManager != null)
        {
            body = GetComponent<Rigidbody>();
            body2 = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();

            IsTracking = true;
        }
        else
        {
            print(this.name);
            Debug.LogError("TimeManager script cannot be found in scene. Time tracking cannot be started. Did you forget to put it into the scene?");
        }

        trackedPositionsAndRotation = new CircularBuffer<PositionAndRotationValues>();
         trackedVelocities = new CircularBuffer<Vector3>();
        trackedAnimationTimes = new List<CircularBuffer<AnimationValues>>();
        if (animator != null)
            for (int i = 0; i < animator.layerCount; i++)
                trackedAnimationTimes.Add(new CircularBuffer<AnimationValues>());
        trackedAudioTimes = new CircularBuffer<AudioTrackedData>();
        _isActiveTracker = new CircularBuffer<bool>();
        _isPressedTracker = new CircularBuffer<bool>();
        isDying2Tracker = new CircularBuffer<bool>();
        isDying1Tracker = new CircularBuffer<bool>();
        movingForwardTracker = new CircularBuffer<bool>();
        TempInitialPositionTracker = new CircularBuffer<Vector3>();
        TempFinalPositionTracker = new CircularBuffer<Vector3>();
    }
    
    private void Start()
    {
        rewinder = FindObjectOfType<Rewinder>();
        InitializeParticles(particleSettings);
    }
    
    

    protected void FixedUpdate()
    {
        if (IsTracking)
            Track();
    }

    #region PositionRotation

    CircularBuffer<PositionAndRotationValues> trackedPositionsAndRotation;
    public struct PositionAndRotationValues
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    /// <summary>
    /// Call this method in Track() if you want to track object Position and Rotation
    /// </summary>
    protected void TrackPositionAndRotation()
    {
        PositionAndRotationValues valuesToWrite;
        valuesToWrite.position = transform.position;
        valuesToWrite.rotation = transform.rotation;
        trackedPositionsAndRotation.WriteLastValue(valuesToWrite);
    }
    /// <summary>
    /// Call this method in GetSnapshotFromSavedValues() to restore Position and Rotation
    /// </summary>
    protected void RestorePositionAndRotation(float seconds)
    {
        PositionAndRotationValues valuesToRead = trackedPositionsAndRotation.ReadFromBuffer(seconds);
        transform.SetPositionAndRotation(valuesToRead.position, valuesToRead.rotation);
    }
    #endregion

    #region Velocity
    CircularBuffer<Vector3> trackedVelocities;
    /// <summary>
    /// Call this method in Track() if you want to track velocity of Rigidbody
    /// </summary>
    protected void TrackVelocity()
    {

        if (body != null)
        {
            trackedVelocities.WriteLastValue(body.velocity);            
        }
        else if (body2!=null)
        {
            trackedVelocities.WriteLastValue(body2.velocity);
        }
        else
        {
            Debug.LogError("Cannot find Rigidbody on the object, while TrackVelocity() is being called!!!");
        }
    }
    /// <summary>
    /// Call this method in GetSnapshotFromSavedValues() to velocity of Rigidbody
    /// </summary>
    protected void RestoreVelocity(float seconds)
    {   
        if(body!=null)
        {
            body.velocity = trackedVelocities.ReadFromBuffer(seconds);
        }
        else
        {
            body2.velocity = trackedVelocities.ReadFromBuffer(seconds);
        }
    }
    #endregion

    #region Animator
    List<CircularBuffer<AnimationValues>> trackedAnimationTimes;         //All animator layers are tracked
    public struct AnimationValues
    {
        public float animationStateTime;
        public int animationHash;
        public List<AnimationParameters> animationParameters;
    }
    public struct AnimationParameters
    {
        public String name;
        public AnimatorControllerParameterType Type;
        public float FloatValue;
        public int IntValue;
        public bool BoolValue;
    }
    /// <summary>
    /// Call this method in Track() if you want to track Animator states
    /// </summary>
    private void TrackAnimator()
    {
        if(animator == null)
        {
            Debug.LogError("Cannot find Animator on the object, while TrackAnimator() is being called!!!");
            return;
        }

        animator.speed = 1;
        AnimatorControllerParameter[] animationParametersArray = animator.parameters;
        List<AnimationParameters> parametersToWrite = new List<AnimationParameters>();
        foreach (var parameter in animationParametersArray)
        {
            AnimationParameters parameterToWrite = new AnimationParameters();
            parameterToWrite.name = parameter.name;
            parameterToWrite.Type = parameter.type;
            switch (parameterToWrite.Type)
            {
                case AnimatorControllerParameterType.Bool:
                    parameterToWrite.BoolValue = animator.GetBool(parameter.name);
                    break;
                case AnimatorControllerParameterType.Float:
                    parameterToWrite.FloatValue = animator.GetFloat(parameter.name);
                    break;
                case AnimatorControllerParameterType.Int:
                    parameterToWrite.IntValue = animator.GetInteger(parameter.name);
                    break;
            }
            parametersToWrite.Add(parameterToWrite);
        }
        for (int i = 0; i < animator.layerCount; i++)
        {
            AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(i);
            AnimationValues valuesToWrite;
            valuesToWrite.animationStateTime = animatorInfo.normalizedTime;
            valuesToWrite.animationHash = animatorInfo.shortNameHash;
            valuesToWrite.animationParameters = parametersToWrite;
            trackedAnimationTimes[i].WriteLastValue(valuesToWrite);
        }         
    }
    /// <summary>
    /// Call this method in GetSnapshotFromSavedValues() to restore Animator state
    /// </summary>
    protected void RestoreAnimator(float seconds)
    {
        animator.speed = 0;
        
        for(int i=0;i<animator.layerCount;i++)
        {
            AnimationValues readValues = trackedAnimationTimes[i].ReadFromBuffer(seconds);
            animator.Play(readValues.animationHash,i, readValues.animationStateTime);
            foreach (var parameter in readValues.animationParameters)
            {
                switch (parameter.Type)
                {
                    case AnimatorControllerParameterType.Bool:
                        animator.SetBool(parameter.name, parameter.BoolValue);
                        break;
                    case AnimatorControllerParameterType.Float:
                        animator.SetFloat(parameter.name, parameter.FloatValue);
                        break;
                    case AnimatorControllerParameterType.Int:
                        animator.SetInteger(parameter.name, parameter.IntValue);
                        break;
                }
            }
        }         
    }
    #endregion

    #region Audio
    CircularBuffer<AudioTrackedData> trackedAudioTimes;
    public struct AudioTrackedData
    {
        public float time;
        public bool isPlaying;
        public bool isEnabled;
    }
    /// <summary>
    /// Call this method in Track() if you want to track Audio
    /// </summary>
    protected void TrackAudio()
    {
        if(audioSource==null)
        {
            Debug.LogError("Cannot find AudioSource on the object, while TrackAudio() is being called!!!");
            return;
        }

        audioSource.volume = 1;
        AudioTrackedData dataToWrite;
        dataToWrite.time = audioSource.time;
        dataToWrite.isEnabled = audioSource.enabled;
        dataToWrite.isPlaying = audioSource.isPlaying;

        trackedAudioTimes.WriteLastValue(dataToWrite);      
    }
    /// <summary>
    /// Call this method in GetSnapshotFromSavedValues() to restore Audio
    /// </summary>
    protected void RestoreAudio(float seconds)
    {
        AudioTrackedData readValues = trackedAudioTimes.ReadFromBuffer(seconds);
        audioSource.enabled = readValues.isEnabled;
        if(readValues.isPlaying)
        {
            audioSource.time = readValues.time;
            audioSource.volume = 0;

            if (!audioSource.isPlaying)
            {  
                audioSource.Play();
            }
        }
        else if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    #endregion

    #region Particles
    private float particleTimeLimiter;
    private float particleResetTimeTo;
    List<CircularBuffer<ParticleTrackedData>> trackedParticleTimes = new List<CircularBuffer<ParticleTrackedData>>();
    public struct ParticleTrackedData
    {
        public bool isActive;
        public float particleTime;
    }


    private List<ParticleData> particleSystemsData;
    [SerializeField] private Rewinder rewinder;

    /// <summary>
    /// particle system and its enabler, which is for tracking if particle system game object is enabled or disabled
    /// particle
    /// </summary>
    [Serializable]
    public struct ParticleData
    {
        
        public ParticleSystem particleSystem;
        [Tooltip("Particle system enabler, for tracking particle system game object active state")]
        public GameObject particleSystemEnabler;
    }
    /// <summary>
    /// Particle settings to setup particles in custom variable tracking
    /// </summary>
    [Serializable]
    public struct ParticlesSetting
    {
        [Tooltip("For long lasting particle systems, set time tracking limiter to drastically improve performance ")]
        public float particleLimiter;
        [Tooltip("Variable defining to which second should tracking return to after particle tracking limit was hit. Play with this variable to get better results, so the tracking resets are not much noticeable.")]
        public float particleResetTo;
        public List<ParticleData> particlesData;
    }

    /// <summary>
    /// Use this method first when using particle rewinding implementation
    /// </summary>
    /// <param name="particleDataList">Data defining which particles will be tracked</param>
    /// <param name="particleTimeLimiter">For long lasting particle systems, set time tracking limiter to drastically improve performance </param>
    /// <param name="resetParticleTo">Variable defining to which second should tracking return to after particle tracking limit was hit. Play with this variable to get better results, so the tracking resets are not much noticeable.</param>
    protected void InitializeParticles(ParticlesSetting particleSettings)
    {
        if(particleSettings.particlesData.Any(x=>x.particleSystemEnabler==null||x.particleSystem==null))
        {
            Debug.LogError("Initialized particle system are missing data. Either Particle System or Particle System Enabler is not filled for some values");
        }
        particleSystemsData = particleSettings.particlesData;
        particleTimeLimiter = particleSettings.particleLimiter;
        particleResetTimeTo = particleSettings.particleResetTo;
        particleSystemsData.ForEach(x => trackedParticleTimes.Add(new CircularBuffer<ParticleTrackedData>()));
        foreach (CircularBuffer<ParticleTrackedData> i in trackedParticleTimes)
        {
            ParticleTrackedData trackedData;
            trackedData.particleTime = 0;
            trackedData.isActive = false;
            i.WriteLastValue(trackedData);
        }
    }
    /// <summary>
    /// Call this method in Track() if you want to track Particles (Note that InitializeParticles() must be called beforehand)
    /// </summary>
    protected void TrackParticles()
    {
        if(particleSystemsData==null)
        {
            Debug.LogError("Particles not initialized!!! Call InitializeParticles() before the tracking starts");
            return;
        }
        if(particleSystemsData.Count==0)
            Debug.LogError("Particles Data not filled!!! Fill Particles Data in the Unity Editor");

        try
        {
            for (int i = 0; i < particleSystemsData.Count; i++)
            {
                if (particleSystemsData[i].particleSystem.isPaused)
                    particleSystemsData[i].particleSystem.Play();

                ParticleTrackedData lastValue = trackedParticleTimes[i].ReadLastValue();
                float addTime = lastValue.particleTime + Time.fixedDeltaTime;

                ParticleTrackedData particleData;
                particleData.isActive = particleSystemsData[i].particleSystemEnabler.activeInHierarchy;

                if ((!lastValue.isActive) && (particleData.isActive))
                    particleData.particleTime = 0;
                else if (!particleData.isActive)
                    particleData.particleTime = 0;
                else
                    particleData.particleTime = (addTime > particleTimeLimiter) ? particleResetTimeTo : addTime;

                trackedParticleTimes[i].WriteLastValue(particleData);
            }
        }
        catch
        {
            Debug.LogError("Particles Data not filled properly!!! Fill both the Particle System and Particle System Enabler fields for each element");
        }

    }
    /// <summary>
    /// Call this method in GetSnapshotFromSavedValues() to Particles
    /// </summary>
    protected void RestoreParticles(float seconds)
    {
        for (int i = 0; i < particleSystemsData.Count; i++)
        {
            GameObject particleEnabler = particleSystemsData[i].particleSystemEnabler;


            ParticleTrackedData particleTracked = trackedParticleTimes[i].ReadFromBuffer(seconds);

            if (particleTracked.isActive)
            {

                if (!particleEnabler.activeSelf)
                    particleEnabler.SetActive(true);

                particleSystemsData[i].particleSystem.Simulate(particleTracked.particleTime, false, true, false);
            }
            else
            {
                if (particleEnabler.activeSelf)
                    particleEnabler.SetActive(false);
            }
        }
    }
    #endregion

    #region Button

    private PhysicsButton _physicsButton;
    private CircularBuffer<bool> _isActiveTracker ;
    private CircularBuffer<bool> _isPressedTracker ;

    private void TrackActiveTracker()
    {
        if (_physicsButton != null)
        {
            _physicsButton.isRewinidng = false;
            // _isActiveTracker.WriteLastValue(_physicsButton.isactive);
            _isActiveTracker.WriteLastValue(_physicsButton.isPressed);
        }
    }

    private void RestoreActiveTracker(float seconds)
    {
        
        if (_physicsButton != null)
        {
            _physicsButton.isRewinidng = true;
            // _physicsButton.isactive = _isActiveTracker.ReadFromBuffer(seconds);
            _physicsButton.isPressed = _isPressedTracker.ReadFromBuffer(seconds);
        }
    }

    #endregion

    #region death1

    private CircularBuffer<bool> isDying1Tracker;
    private CircularBuffer<bool> isDying2Tracker;

    private void TrackIsDying1Bool()
    {
        isDying1Tracker.WriteLastValue(dmanager.isdying1);
    }    
    private void TrackIsDying2Bool()
    {
        isDying2Tracker.WriteLastValue(dmanager.isdying2);
    }

    private void RestoreIsDying1Bool(float second)
    {
        dmanager.isdying1 = isDying1Tracker.ReadFromBuffer(second);
    }    
    private void RestoreIsDying2Bool(float second)
    {
        dmanager.isdying2 = isDying2Tracker.ReadFromBuffer(second);
    }

    #endregion

    #region Platform
    private CircularBuffer<bool> movingForwardTracker;
    private CircularBuffer<Vector3> TempInitialPositionTracker;
    private CircularBuffer<Vector3> TempFinalPositionTracker;

    public void TrackPlatform()
    {
        movingForwardTracker.WriteLastValue(_onpressplat.ismovingfwd);
        TempFinalPositionTracker.WriteLastValue(_onpressplat.tempfinalpos);
        TempInitialPositionTracker.WriteLastValue(_onpressplat.tempinipos);
    }

    public void RestorePlatform(float second)
    {
        _onpressplat.ismovingfwd = movingForwardTracker.ReadFromBuffer(second);
        _onpressplat.tempfinalpos = TempFinalPositionTracker.ReadFromBuffer(second);
        _onpressplat.tempinipos = TempInitialPositionTracker.ReadFromBuffer(second);
    }
    

    #endregion
    private void OnTrackingChange(bool val, RewindManager rewindMan)
    {
        if(rewindMan != rewindManager) return;
        IsTracking = val;
    }
    protected void OnEnable()
    {
        RewindManager.RewindTimeCall += Rewind;
        RewindManager.TrackingStateCall += OnTrackingChange;        
    }
    protected void OnDisable()
    {
        RewindManager.RewindTimeCall -= Rewind;
        RewindManager.TrackingStateCall -= OnTrackingChange;            
    }

    /// <summary>
    /// Main method where all tracking is filled, lets choose here what will be tracked for specific object
    /// </summary>
    private void Track()
    {
        if (trackPositionRotation)
            TrackPositionAndRotation();
        if (trackVelocity)
            TrackVelocity();
        if (trackAnimator)
            TrackAnimator();
        if (trackParticles)
            TrackParticles();
        if (trackAudio)
            TrackAudio();
        if (physicsButton)
            TrackActiveTracker();
        if (trackIsDying1)
            TrackIsDying1Bool();
        if (trackIsDying2)
            TrackIsDying2Bool();
        if(trackPlatforms)
            TrackPlatform();
    }


    /// <summary>
    /// Main method where all rewinding is filled, lets choose here what will be rewinded for specific object
    /// </summary>
    /// <param name="seconds">Parameter defining how many seconds we want to rewind back</param>
    /// <param name="rewindMan">Parameter for checking that same rewind manager is calling it</param>
    private void Rewind(float seconds, RewindManager rewindMan)
    {
        if (rewindManager != rewinder.rewindManagers[rewinder.activeRewindManagerIndex])
        {
            return;
        }
        if (trackPositionRotation)
            RestorePositionAndRotation(seconds);
        if (trackVelocity)
            RestoreVelocity(seconds);
        if (trackAnimator)
            RestoreAnimator(seconds);
        if (trackParticles)
            RestoreParticles(seconds);
        if(trackAudio)
            RestoreAudio(seconds);
        if (physicsButton)
            RestoreActiveTracker(seconds);
        if (trackIsDying1)
            RestoreIsDying1Bool(seconds);
        if (trackIsDying2)
            RestoreIsDying2Bool(seconds); 
        if (trackPlatforms)
            RestorePlatform(seconds);
    }
    
}      