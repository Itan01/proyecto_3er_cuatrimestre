using UnityEngine;
public class PL_Button
{
    protected KeyCode _key;

    public PL_Button()
    {
        _key = KeyCode.None;
    }
    public PL_Button SetKey(KeyCode Key)
    {
        _key = Key;
        return this;
    }

}
