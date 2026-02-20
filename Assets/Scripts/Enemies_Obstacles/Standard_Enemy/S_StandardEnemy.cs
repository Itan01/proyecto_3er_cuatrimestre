using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_StandardEnemy : MonoBehaviour, ISoundInteractions
{

    private S_StandardEnemy_Patrol _patrol;
    private S_StandardEnemy_Watching _watching;
    private S_StandardEnemy_Chasing _chase;
    private S_StandardEnemy_HearNoise _hear;
    private S_StandardEnemy_MoveToPosition _movingToPosition;
    private S_StandardEnemy_Search _search;
    private S_StandardEnemy_Stunned _stunned;

    private NavMeshAgent _agent;
    private Animator _animator;
    private AudioSource _audioSource;

    private Vector3 _desirePosition;
    [SerializeField] private EStandardEnemyBehaviours _state;
    [SerializeField] private EStandardEnemyBehaviours _previousState;
    [SerializeField] private bool _activate=false;

    [Header("<color=red>Setter</color>")]
    [SerializeField] private EnemyVision _vision;
    [SerializeField] private MarkStateManager _markState;
    [SerializeField] private Transform _addReferencer;

    [Header("<color=Blue>DATA</color>")]
    [SerializeField] private DATA_MARKSTATE[] Marks;
    [SerializeField] private DATA_STANDARDENEMY Data;
    [SerializeField] private DATA_FEEDBACK_STATE[] DataState;



    private Fsm_StandardEnemy _fsm;
    private Dictionary<EMarkEnemyState, Sprite> _spritesMark = new();

    private void Start()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Data.Speed;
        if(_animator == null) _animator = GetComponentInChildren<Animator>();
        if(_audioSource==null) _audioSource = GetComponent<AudioSource>();
        RoomManager Room = GetComponentInParent<RoomManager>();
        Room.DestroyRoom += Destroy;
        Room.DesActRoom += DesActivation;
        Room.ActRoom += Activation;

        _fsm = new Fsm_StandardEnemy();
        _patrol = (S_StandardEnemy_Patrol)new S_StandardEnemy_Patrol(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[0]);
        _patrol = _patrol.Positions(Data.Positions);
        _watching = (S_StandardEnemy_Watching) new S_StandardEnemy_Watching(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[1]);
        _chase= (S_StandardEnemy_Chasing)new S_StandardEnemy_Chasing(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[2]);
        _hear=(S_StandardEnemy_HearNoise) new S_StandardEnemy_HearNoise(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[3]);
        _movingToPosition = (S_StandardEnemy_MoveToPosition) new S_StandardEnemy_MoveToPosition(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[4]);
        _search= (S_StandardEnemy_Search) new S_StandardEnemy_Search(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[5]);
        _stunned = (S_StandardEnemy_Stunned)new S_StandardEnemy_Stunned(_fsm).Agent(_agent).Entity(this).Animator(_animator).DATA(DataState[6]);

        _patrol.AddBehaviour(EStandardEnemyBehaviours.Watching,_watching);
        _patrol.AddBehaviour(EStandardEnemyBehaviours.MoveToPosition, _movingToPosition);
        _patrol.AddBehaviour(EStandardEnemyBehaviours.Hear, _hear);
        _patrol.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);

        _watching.AddBehaviour(EStandardEnemyBehaviours.Patrol,_patrol);
        _watching.AddBehaviour(EStandardEnemyBehaviours.MoveToPosition,_movingToPosition);
        _watching.AddBehaviour(EStandardEnemyBehaviours.Chase, _chase);
        _watching.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);

        _chase.AddBehaviour(EStandardEnemyBehaviours.Patrol,_patrol);
        _chase.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);

        _hear.AddBehaviour(EStandardEnemyBehaviours.Hear, _hear);
        _hear.AddBehaviour(EStandardEnemyBehaviours.MoveToPosition, _movingToPosition);
        _hear.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);


        _movingToPosition.AddBehaviour(EStandardEnemyBehaviours.Search, _search);
        _movingToPosition.AddBehaviour(EStandardEnemyBehaviours.Hear, _hear);
        _movingToPosition.AddBehaviour(EStandardEnemyBehaviours.Watching, _watching);
        _movingToPosition.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);

        _search.AddBehaviour(EStandardEnemyBehaviours.Patrol,_patrol);
        _search.AddBehaviour(EStandardEnemyBehaviours.Stunned, _stunned);

        _stunned.AddBehaviour(EStandardEnemyBehaviours.Search, _search);


        SetMarkDictionary();

       EventManager.Subscribe(EEvents.DetectPlayer, SetDesirePosition);
       EventManager.Subscribe(EEvents.AlertPlayer, SetNoTimer);
       EventManager.Subscribe(EEvents.DetectSound, SetDesirePosition);
       EventManager.Subscribe(EEvents.ReStart, Restart);
        DesActivation();
    }
    private void Update()
    {
        if (!_activate) return;
        _fsm.VirtualUpdate();
    }
    private void FixedUpdate()
    {
        if (!_activate) return;
        _fsm.VirtualFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerManager>())
        EventManager.Trigger(EEvents.Reset);
    }

    #region DATA_GENERAL
    public EStandardEnemyBehaviours State
    {
        set { _state = value; }
        get { return _state; }

    }
    public EStandardEnemyBehaviours PreviousState
    {
        set { _previousState = value; }
        get { return _previousState; }

    }
    public float Speed()
    {
        return Data.Speed;
    }
    public Vector3 DesirePos
    {
        set {_desirePosition = value; }
        get { return _desirePosition; }
    }

    public void PlayAudio(AudioClip Clip, float Volume = 1.0f, float Pitch = 1.0f)
    {
        if (Clip == null) return;
        _audioSource.pitch = Pitch;
        _audioSource.PlayOneShot(Clip,Volume);
    }
    public bool CheckDistanceToHear()
    {
        PlayerManager Player = GameManager.Instance.PlayerReference;
        if (Player.IsDeath() || !Player.IsMoving() || Player.IsCrouching()) return false;
        if ((Player.transform.position - transform.position).magnitude <= Data.DistanceToHear)
        {
            return true;
        }
        else
            return false;
    }
    public void AddNewPos()
    {
        _addReferencer.position = transform.position;
        _patrol.AddPosition(_addReferencer);
    }

    #endregion

    #region VISION
    public bool CheckVision()
    {
       return _vision.CheckIfHasVIsion();
    }
    public void SetColorVision(Color Color)
    {
        _vision.SetColorVision(Color);
    }

    #endregion

    #region DATA_MARKSTATE
    public void ShowMark(bool ShowMark, EMarkEnemyState State)
    {
        if (!_spritesMark.ContainsKey(State)) return;
        _markState.SetMark(ShowMark, _spritesMark[State]);
    }
    private void SetMarkDictionary()
    {
        for (int i = 0; i < Marks.Length; i++)
        {
            _spritesMark.Add(Marks[i].Name, Marks[i].Sprite);
        }

    }

    #endregion

    #region EVENTS 
    public void IIteraction(bool PlayerShootIt)
    {
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Stunned);
    }
    private void Restart(params object[] Parameters)
    {
        if (!_activate) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Patrol);
        _patrol.SetWaypoint(0);
        transform.position = Data.Positions[0].position;
        AddNewPos();
    }
    public void SetDesirePosition(params object[] Parameters)
    {
        if(!_activate) return;
        Transform Transform = (Transform)Parameters[0];
        DesirePos = Transform.position;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Hear);
    }
    public void SetNoTimer(params object[] Parameters)
    {
        if (!_activate) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Chase);
    }

    private void Destroy()
    {
        RoomManager Room = GetComponentInParent<RoomManager>();
        Room.DestroyRoom -= Destroy;
        Room.DesActRoom -= DesActivation;
        Room.ActRoom -= Activation;
        _activate = false;
        gameObject.SetActive(false);
    }
    private void DesActivation()
    {
        _animator.SetBool("isMoving", false);
        _animator.SetBool("isRunning", false);
        _vision.Deactivate();
        _activate = false;
    }

    private void Activation()
    {
        _vision.Activate();
        _fsm.SetStartBehaviour(_patrol);
        _activate = true;
    }


    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEvents.ReStart, Restart);
        EventManager.Unsubscribe(EEvents.DetectSound, SetDesirePosition);
        EventManager.Unsubscribe(EEvents.AlertPlayer, SetNoTimer);
        EventManager.Unsubscribe(EEvents.DetectPlayer, SetDesirePosition);
    }
    #endregion
}
