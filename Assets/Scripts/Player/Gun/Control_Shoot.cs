using UnityEngine;
public class Control_Shoot : Abstract_Control, IButton
{
    private bool _noSound=true;
    public Control_Shoot(PL_Control Controller)
    {;
        Controller.AddAction(Execute);
    }
    public override void Execute()
    {

    }
    //private void AvailableSound()
    //{
    //    AudioManager.Instance.PlaySFX(_clip, 1.0f);
    //    _hasASound = false;
    //    _model.forward = _direction;
    //    var NewSound = UnityEngine.Object.Instantiate(_soundReference, _spawn.position, _spawn.rotation);
    //    AbstractSound script = NewSound.GetComponent<AbstractSound>();
    //    script.SetTarget(null, 0.0f);
    //    script.SetDirection(_direction, _speed, _size);
    //    script.FreezeObject(false);
    //    script.PlayerCanCatchIt(false);
    //    script.SetIfPlayerSummoned(true);
    //    script.SetPlayerShootIt(true);
    //    _aux.y = 0.0f;
    //    _model.transform.forward = _aux;
    //}
}

