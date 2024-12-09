using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game10
{
    public class DirectionCommand
    {
        //public enum CommandEnum { None, Stop, Move};
        //public CommandEnum Command { get; private set; }
        public bool NoneCommand { get; private set; }
        public Vector2 Direction { get; private set; }

        public bool HasCommand() { return !NoneCommand; }

        public DirectionCommand(Vector2? direction)
        {
            if (direction == null)
                NoneCommand = true;
            else
            {
                NoneCommand = false;
                Direction = direction.Value;
            }
        }

        public bool IsOpposite(Vector2 motionDirection)
        {
            if (NoneCommand)
                return false;

            if (Direction == Vector2.Zero || motionDirection == Vector2.Zero)
                return false;

            return Direction.X == -motionDirection.X &&
                Direction.Y == -motionDirection.Y;
        }

    }
}
