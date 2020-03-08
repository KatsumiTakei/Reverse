using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class InputManager
{
    public static void ManualUpdate()
    {
        Keyboard.ManualUpdate();
    }
}

public enum eHIDType
{
    None,

    Keyboard,
    Mouse,
    Pad1,
    Pad2,
}

public enum eInputType
{
    MoveUpKeyDown,
    MoveUpKey,
    MoveUpKeyUp,
    MoveDownKeyDown,
    MoveDownKey,
    MoveDownKeyUp,
    MoveRightKeyDown,
    MoveRightKey,
    MoveRightKeyUp,
    MoveLeftKeyDown,
    MoveLeftKey,
    MoveLeftKeyUp,

    MoveRuKey,
    MoveRdKey,
    MoveLuKey,
    MoveLdKey,

    AttackAndDecideKeyDown,
    AttackAndDecideKey,
    AttackAndDecideKeyUp,

    CanselKeyDown,
    CanselKey,
    CanselKeyUp,

    SpaceKeyDown,
}
