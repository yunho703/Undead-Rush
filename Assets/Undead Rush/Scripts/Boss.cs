using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Dead()
    {

        base.Dead();
        GameManager.instance.OnBossDefeated();
    }

}
