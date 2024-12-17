using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework.Input;

namespace ProjectGameCP215
{
    public class PlayState : Actor
    {

        ExitNotifier exitNotifier;
        Actor all;
        Actor enermy = new Actor();
        MaleActor maleActor;
        ProgressBar hpBar;

        public PlayState(CameraMan cameraMan, Vector2 screenSize, ExitNotifier exitNotifier, Actor all)
        {
            this.exitNotifier = exitNotifier;
            this.all = all;

            maleActor = new MaleActor(screenSize / 2);
            maleActor.Add(cameraMan);
            
            hpBar = new ProgressBar(new Vector2(200, 20), max: maleActor.maxHp, Color.Black, Color.Green);
            hpBar.Position = new Vector2(50, 50);
            hpBar.Value = maleActor.hp;

            Actor visual = new Actor();

            for (int i = 0; i < 50; i++)
            {
                enermy.Add(new Slime(RandomUtil.Position(screenSize)));
            }

            visual.Add(hpBar);
            visual.Add(maleActor);
            visual.Add(enermy);

            Add(visual);
        }



        private Actor CreateTree()
        {
            var treeRegion = new TextureRegion(TextureCache.Get("Content/Resource/TileMap/TileSet.png"),
                                                new RectF(0, 32 * 15, 32, 32 * 3));
            var tree = new SpriteActor(treeRegion);
            tree.Origin = new Vector2(32 / 2, tree.RawSize.Y - 32 / 2);
            return tree;
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var keyInfo = GlobalKeyboardInfo.Value;

            if (keyInfo.IsKeyPressed(Keys.Space))
            {
                exitNotifier.Invoke(this, 0);
            }

            for (int i = 0; i < enermy.ChildCount; i++)
            {
                var slime = enermy.GetChild(i) as Slime;
                slime.AddAction(new RandomMover(slime, maleActor.Position));
            }

            hpBar.Value = maleActor.hp;

            if(hpBar.Value <= 0){
                exitNotifier.Invoke(this, 1);
            }

        }


    }

}