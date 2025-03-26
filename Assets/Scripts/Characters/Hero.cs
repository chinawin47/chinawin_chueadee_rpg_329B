using UnityEngine;

public class Hero : Character
{
    void Start()
    {
        inventoryItems = new Item[16];
    }

    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            case CharState.WalkToEnemy:
                WalkToEnemyUpdate();
                break ;
            case CharState.Attack:
                AttackUpdate(); 
                break ;
            case CharState.WalkToMagicCast:
                WalkToMagicCastUpdate();
                break;
        }
    }
}
