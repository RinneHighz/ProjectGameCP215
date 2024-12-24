
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace ProjectGameCP215
{
    public class StatPanel : Panel
    {

    public StatPanel() : base (new Vector2(800,200),backgroundColor: Color.White,outlineColor: Color.Black,outlineWidth:2)
        {
            

        }
        private void CreateLabels()
        {
        //    var your_score = CreateLabel("Your Score");
        //    var top = CreateLabel("Top 5");

        //    your_score.Position = new Vector2(50,50);
        //    Alignment.SetPosition(your_score,top,AlignDirection.Down);

        //    Add(your_score);
        //    Add(top);



        }

        private Text CreateLabel(string s)
        {
            return new Text("Content/Resource/Font/PixelFont.ttf",25,Color.Black,s);
        }
       
    }
}


 