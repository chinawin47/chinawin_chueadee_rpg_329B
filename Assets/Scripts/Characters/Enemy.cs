using UnityEngine;

public class Enemy : Characters
{
 void Update()
    {
        switch(state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
        }
    }
}
