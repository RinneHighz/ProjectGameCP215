using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Media;
using System;

namespace ProjectGameCP215
{
    public class PlayState : Actor
    {

        ExitNotifier exitNotifier;
        Actor all;
        Actor enermy = new Actor();
        Actor uiLayer = new Actor();
        MaleActor maleActor;
        ProgressBar hpBar;
        Label scoreLabel;
        Vector2 previousMaleActorPosition;
        TextureRegion[] tiles;
        TileMap TileMap;
        CollisionDetection collisionDetectionUnit;
        Song backgroundMusic;

        public PlayState(CameraMan cameraMan, Vector2 screenSize, ExitNotifier exitNotifier, Actor all, CollisionDetection CollisionDetection)
        {
            this.exitNotifier = exitNotifier;
            this.all = all;
            this.collisionDetectionUnit = CollisionDetection;

            maleActor = new MaleActor(screenSize / 2);
            maleActor.Add(cameraMan);

            backgroundMusic = Song.FromUri("Song01",
                      new Uri("Content/Resource/Sound/PlayStateBGM.ogg", UriKind.Relative));

            // MediaPlayer.Play(backgroundMusic);
            previousMaleActorPosition = maleActor.Position;

            hpBar = new ProgressBar(new Vector2(200, 20), max: maleActor.maxHp, Color.Black, Color.Green)
            {
                Position = new Vector2(50, 50),
                Value = maleActor.hp
            };

            scoreLabel = new Label("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50, Color.Brown, "Score: 0")
            {
                Position = new Vector2(50, 100)
            };

            Actor visual = new Actor();

            // for (int i = 0; i < 50; i++)
            // {
            //     enermy.Add(new Slime(RandomUtil.Position(screenSize)));
            // }

            uiLayer.Add(hpBar);
            uiLayer.Add(scoreLabel);






            var builder = new TileMapBuilder();
            var Layer0 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._frame.csv");
            var Layer1 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer1.csv");
            var Layer2 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer2.csv");
            var Layer3 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer3.csv");
            var Layer4 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer4.csv");
            var Layer5 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer5.csv");
            var Layer6 = builder.CreateSimple(
                "CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Castle Game._layer6.csv");


            visual.Add(Layer0);
            visual.Add(Layer1);
            visual.Add(Layer2);
            visual.Add(Layer3);
            visual.Add(Layer4);
            visual.Add(Layer5);
            visual.Add(Layer6);

            visual.Add(maleActor);
            visual.Add(enermy);
            Add(visual);
            Add(uiLayer);

            int[] prohibitTiles = [322, 323, 483, 3083, 3084, 3163, 3164, 3243, 3244];

            var maleactor = new MaleActor(Layer0.TileCenter(0, 0));
            TileMapBuilder.AddCollisions(Layer0, prohibitTiles, 2);
            collisionDetectionUnit.AddDetector(1, 2);








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
                slime.AddAction(new RandomMover(slime, maleActor));
            }

            // Update UI Layer to follow maleActor movement
            Vector2 movementDelta = maleActor.Position - previousMaleActorPosition;
            uiLayer.Position += movementDelta;
            previousMaleActorPosition = maleActor.Position;

            hpBar.Value = maleActor.hp;
            scoreLabel.Text = "Score: " + maleActor.score;

            if (hpBar.Value <= 0)
            {
                exitNotifier.Invoke(this, 1);
            }

        }

        public int GetScore()
        {
            return maleActor.score;
        }













    }

}
