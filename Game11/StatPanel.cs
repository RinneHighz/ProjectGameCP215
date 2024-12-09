
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game11
{
    public class StatPanel : Panel
    {
        public StatPanel()
            : base(new Vector2(300, 300), Color.Ivory, Color.Black, 2)
        {
            CreateLabels();
            CreateNumbers();
        }

        private void CreateLabels()
        {
            var str = CreateLabel("STR");
            var dex = CreateLabel("DEX");
            var intel = CreateLabel("INT");

            str.Position = new Vector2(50, 50);
            Alignment.SetPosition(str, dex, AlignDirection.Down);
            Alignment.SetPosition(dex, intel, AlignDirection.Down);

            Add(str);
            Add(dex);
            Add(intel);
        }

        private Text CreateLabel(string s)
        {
            return new Text("consola.ttf", 25, Color.Black, s);
        }

        private void CreateNumbers()
        {
            var str = CreateNumber(16);
            var dex = CreateNumber(18);
            var intel = CreateNumber(6);

            str.Position = new Vector2(250, 50);
            Alignment.SetPosition(str, dex, AlignDirection.Down);
            Alignment.SetPosition(dex, intel, AlignDirection.Down);

            Add(str);
            Add(dex);
            Add(intel);
        }
        private Text CreateNumber(int number)
        {
            var text = new Text("consola.ttf", 25, Color.Blue, number.ToString());
            Alignment.SetOrigin(text, Align.Right);
            return text;
        }
    }
}
