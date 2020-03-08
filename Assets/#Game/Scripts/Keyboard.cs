using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class InputManager
{
    static class Keyboard
    {
        #region variable
        static KeyCode attackAndDecide = KeyCode.Z;
        static KeyCode cansel = KeyCode.X;

        static KeyCode moveRight = KeyCode.RightArrow;
        static KeyCode moveLeft = KeyCode.LeftArrow;
        static KeyCode moveUp = KeyCode.UpArrow;
        static KeyCode moveDown = KeyCode.DownArrow;

        static KeyCode space = KeyCode.Space;

        #endregion variable

        public static sbyte PlayerIndex { get; set; } = 1;//    HACK


        #region private
        static void KeyDown(eInputType inputType, params KeyCode[] keys)
        {
            int keyMax = keys.Length;
            for (int keyIndex = 0; keyIndex < keyMax; keyIndex++)
            {
                if (!Input.GetKeyDown(keys[keyIndex]))
                    return;
            }

            EventManager.BroadcastMultipleInput(inputType, PlayerIndex, eHIDType.Keyboard);
        }

        static void KeyUp(eInputType inputType, params KeyCode[] keys)
        {
            int keyMax = keys.Length;
            for (int keyIndex = 0; keyIndex < keyMax; keyIndex++)
            {
                if (!Input.GetKeyUp(keys[keyIndex]))
                    return;
            }
            EventManager.BroadcastMultipleInput(inputType, PlayerIndex, eHIDType.Keyboard);

        }
        static void Key(eInputType inputType, params KeyCode[] keys)
        {
            int keyMax = keys.Length;
            for (int keyIndex = 0; keyIndex < keyMax; keyIndex++)
            {
                if (!Input.GetKey(keys[keyIndex]))
                    return;
            }
            EventManager.BroadcastMultipleInput(inputType, PlayerIndex, eHIDType.Keyboard);
        }
        #endregion private
        #region public

        public static void ManualUpdate()
        {
            KeyDown(eInputType.AttackAndDecideKeyDown, attackAndDecide);
            Key(eInputType.AttackAndDecideKey, attackAndDecide);
            KeyUp(eInputType.AttackAndDecideKeyUp, attackAndDecide);

            MoveKeys();

            KeyDown(eInputType.CanselKeyDown, cansel);
            Key(eInputType.CanselKey, cansel);
            KeyUp(eInputType.CanselKeyUp, cansel);

            KeyDown(eInputType.SpaceKeyDown, space);
        }

        static void MoveKeys()
        {
            Key(eInputType.MoveRightKey, moveRight);
            Key(eInputType.MoveLeftKey, moveLeft);
            Key(eInputType.MoveUpKey, moveUp);
            Key(eInputType.MoveDownKey, moveDown);

            Key(eInputType.MoveRuKey, moveRight, moveUp);
            Key(eInputType.MoveRdKey, moveRight, moveDown);
            Key(eInputType.MoveLuKey, moveLeft, moveUp);
            Key(eInputType.MoveLdKey, moveLeft, moveDown);

        }

        #endregion public

    }
}