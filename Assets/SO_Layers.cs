using UnityEngine;
[CreateAssetMenu(fileName = "LMask", menuName = "ScriptableObject/Data/LayerMask")]
public class SO_Layers : ScriptableObject
{
   public LayerMask _obstacles;
    public LayerMask _everything;
    public LayerMask _sounds;
    public LayerMask _interact;
}
