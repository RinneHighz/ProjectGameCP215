using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ThanaNita.MonoGameTnt;

namespace Game04
{
    public class InputTester : Actor
    {
        OnScreenLogger logger;
        public InputTester()
        {
            logger = new OnScreenLogger(20, "consola.ttf", 35, Color.White);
            Add(logger);
        }

        public override void Act(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            var mouseInfo = GlobalMouseInfo.Value;
//            if (keyInfo.IsAnyKeyChanged()) {
                var pressed = keyInfo.GetPressedKeys();
                var released = keyInfo.GetReleasedKeys();
                var down = keyInfo.GetDownKeys();
                logger.Log($"PressedKeys: {Format(pressed, 15)}"
                        + $"ReleasedKeys: {Format(released, 15)}"
                        + $"DownKeys: {Format(down, 15)}");
//            }

            if (mouseInfo.IsPositionChanged() || mouseInfo.IsScrolled())
            {
                string msg = 
                    $"WorldPosition: {mouseInfo.WorldPosition}"
                    + $" DeltaScroll: {mouseInfo.DeltaScroll}"
                    ;
                logger.Log(msg);
            }

            if (mouseInfo.IsLeftButtonPressed() || mouseInfo.IsLeftButtonReleased())
            {
                string msg = $"IsLeftButtonDown: {mouseInfo.IsLeftButtonDown()}"
                    + $" WorldPosition: {mouseInfo.WorldPosition}"
                    + $" DeltaScroll: {mouseInfo.DeltaScroll}"
                    ;
                logger.Log(msg);
            }

            base.Act(deltaTime);
        }
        private static string Format(List<Keys> keys, int totalWidth)
        {
            return ("[" + String.Join(", ", keys) + "]").PadRight(totalWidth);
        }
    }
}
