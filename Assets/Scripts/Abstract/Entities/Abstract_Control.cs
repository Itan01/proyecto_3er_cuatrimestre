using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_Control : IButton
{
    protected Model_Player _model;
    protected View_Player _view;
    protected KeyCode _key,_altKey;
    public Abstract_Control()
    {
        _key = KeyCode.None;
        _altKey=KeyCode.None;
        _model = null;
        _view=null;
    }
    public Abstract_Control Model(Model_Player Model)
    {
        _model=Model;
        return this; 
    }
    public Abstract_Control View(View_Player View)
    {
        _view = View;
        return this;
    }
    public Abstract_Control Key(KeyCode Key)
    {
        _key = Key;
        return this;
    }
    public Abstract_Control AltKey(KeyCode Key)
    {
        _altKey = Key;
        return this;
    }
    public virtual void Execute()
    {

    }
}
