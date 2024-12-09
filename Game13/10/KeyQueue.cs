using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game10
{
    public class KeyQueue
    {
        Queue<Keys> keyQueue = new Queue<Keys>();

        Keys[] allowedKeys = [Keys.Space, Keys.Left, Keys.Right, Keys.Up, Keys.Down];

        public void EnqueueAll(List<Keys> keys)
        {
            foreach (var key in keys)
                Enqueue(key);
        }
        public void Enqueue(Keys key)
        {
            if (!allowedKeys.Contains(key))
                return;
            keyQueue.Enqueue(key);
        }

        public DirectionCommand PeekCommand()
        {
            if (keyQueue.Count == 0)
                return new DirectionCommand(null);
            var key = keyQueue.Peek();
            return new DirectionCommand(DirectionKey.DirectionOf(key));
        }
        public DirectionCommand GetCommand()
        {
            if (keyQueue.Count == 0)
                return new DirectionCommand(null);
            var key = keyQueue.Dequeue();
            return new DirectionCommand(DirectionKey.DirectionOf(key));
        }
    }
}
