using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private int expDrop;
    public int ExpDrop { get { return expDrop;} }

 void Update()
    {
        switch(state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            case CharState.WalkToEnemy:
                WalkToEnemyUpdate();
                break;
            case CharState.Attack:
                AttackUpdate();
                break;
        }
    }

    protected override void Die()
    {
        base.Die();
        partManager.DIstributeTotalExp(expDrop);
    }

}
