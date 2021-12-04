//Script to allow melee animation attacks
//Creater: king
//Date: 12/3/21
using UnityEngine;

public class MeleeAnimationEvent : MonoBehaviour
{
    public void LightAttack()
    {
        this.GetComponentInChildren<MeleeCombat>().LightAttack();
    }
    public void HeavyAttack()
    {
        this.GetComponentInChildren<MeleeCombat>().HeavyAttack();
    }
}
