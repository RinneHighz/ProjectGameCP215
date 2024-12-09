using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class TextAnimation : Action
    {
        Text text;
        string str;
        float textSpeed;
        int charCount;
        float time;
        public TextAnimation(Text text, string str, float textSpeed) // textSpeed (char / second)
        {
            this.text = text;
            this.str = str;
            this.textSpeed = textSpeed;
            Restart();
        }
        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            time += deltaTime;
            charCount = (int)(textSpeed * time);
            if(charCount > str.Length)
                charCount = str.Length;

            text.Str = str.Substring(0, charCount);
            return IsFinished();
        }

        public bool IsFinished()
        {
            return charCount >= str.Length;
        }

        public void Restart()
        {
            charCount = 0;
            time = 0;
        }
    }
}
