//Script to allow melee animation attacks
//Creater: king
//Date: 12/3/21
using UnityEngine;

public class MeleeAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// This will trigger the logic that handles dealing light damage to enemys 
    /// </summary>
    public void LightAttack()
    {
        this.GetComponentInChildren<MeleeCombat>().LightAttack();
    }
    /// <summary>
    /// This will trigger the logic that handles dealing heavydamage to enemys 
    /// </summary>
    public void HeavyAttack()
    {
        this.GetComponentInChildren<MeleeCombat>().HeavyAttack();
    }
}
