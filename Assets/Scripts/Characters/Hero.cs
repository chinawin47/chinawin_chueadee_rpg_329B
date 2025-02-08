using UnityEngine;

public class Hero : Characters
{
    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
        }
    }
}
