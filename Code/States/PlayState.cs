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
        public MaleActor maleActor;
        ProgressBar hpBar;
        Label scoreLabel, levelLabel, damageLabel, maxHpLabel, expLabel;

        Vector2 previousMaleActorPosition;
        TextureRegion[] tiles;
        TileMap TileMap;
        CollisionDetection collisionDetectionUnit;
        Song backgroundMusic;
        Vector2 screenSize;

        private float elapsedTime = 0f; // เก็บเวลาที่ผ่านไปทั้งหมด
        private float duration = 0f; //
        private float spawnInterval = 5f; // ระยะเวลาที่จะเกิดศัตรูใหม่
        private int slimeCount = 1; // จำนวน Slime ที่จะเกิดในแต่ละครั้ง
        private int bossCount = 2; // จำนวน Boss ที่จะเกิดในแต่ละครั้ง
        private Random random = new Random(); // สำหรับสุ่มตำแหน่ง
        public bool isLevelUp { get; set; } = false; // ตัวแปรตรวจสอบสถานะ LevelUp


        public PlayState(CameraMan cameraMan, Vector2 screenSize, ExitNotifier exitNotifier, Actor all, CollisionDetection CollisionDetection)
        {
            this.exitNotifier = exitNotifier;
            this.all = all;
            this.collisionDetectionUnit = CollisionDetection;
            this.screenSize = screenSize;

            maleActor = new MaleActor(new Vector2(screenSize.X / 2 - 100, screenSize.Y / 2), this, exitNotifier);
            // maleActor.Add(cameraMan);

            backgroundMusic = Song.FromUri("Song01",
                      new Uri("Content/Resource/Sound/PlayStateBGM.ogg", UriKind.Relative));

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = 4;

            previousMaleActorPosition = maleActor.Position;

            hpBar = new ProgressBar(new Vector2(200, 20), max: maleActor.maxHp, Color.Black, Color.Green)
            {
                Position = new Vector2(50, 50),
                Value = maleActor.hp
            };


            damageLabel = new Label("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Damage: " + maleActor.damage)
            {
                Position = new Vector2(50, 200)
            };
            maxHpLabel = new Label("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Max HP: " + maleActor.maxHp)
            {
                Position = new Vector2(50, 250)
            };
            expLabel = new Label("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Exp: " + maleActor.exp +"/"+ maleActor.expToNextLevel)
            {
                Position = new Vector2(50, 300)
            };



            scoreLabel = new Label("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Score: 0")
            {
                Position = new Vector2(50, 100)
            };

            Actor visual = new Actor();

            var texture1 = TextureCache.Get("Content/Resource/TileMap/map.png");
            var backgroundimg = new SpriteActor(texture1);
            Add(backgroundimg);

            var builder = new TileMapBuilder();

            var CollideLayer = builder.CreateSimple(
                "Content/Resource/TileMap/CombineTileSet.png", new Vector2(48, 48), countX: 80, countY: 80, "Content/Resource/TileMap/map.csv");

            visual.Add(CollideLayer);

            int[] prohibitTiles = [0];



            TileMapBuilder.AddCollisions(CollideLayer, prohibitTiles, 3);

            collisionDetectionUnit.AddDetector(1, 3);

            levelLabel = new Label("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Level: 1")
            {
                Position = new Vector2(50, 150)
            };
            uiLayer.Add(levelLabel);

            uiLayer.Add(hpBar);
            uiLayer.Add(scoreLabel);


            uiLayer.Add(damageLabel);
            uiLayer.Add(maxHpLabel);
            uiLayer.Add(expLabel);



            visual.Add(maleActor);
            visual.Add(enermy);

            Add(visual);
            Add(uiLayer);






        }


        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            elapsedTime += deltaTime;
            if (elapsedTime >= spawnInterval)
            {
                elapsedTime = 0f;
                spawnInterval = Math.Max(1f, spawnInterval * 0.9f); // ลดระยะเวลาการเกิดให้ถี่ขึ้น
                slimeCount++;
                // if (slimeCount % 5 == 0) bossCount++; // เพิ่ม Boss ทุก ๆ 5 รอบของ Slime

                SpawnEnemies();
            }




            var keyInfo = GlobalKeyboardInfo.Value;

            if (keyInfo.IsKeyPressed(Keys.Space))
            {
                exitNotifier.Invoke(this, 0);
            }

            for (int i = 0; i < enermy.ChildCount; i++)

            {
                duration += deltaTime;
                if (duration > 0.1f)
                {
                    enermy.GetChild(i).ClearAction();
                    enermy.GetChild(i).AddAction(new RandomMover(enermy.GetChild(i), maleActor));
                    // duration = 0;
                }

            }

            hpBar.Value = maleActor.hp;
            hpBar.Max = maleActor.maxHp;
            scoreLabel.Text = "Score: " + maleActor.score;
            levelLabel.Text = "Level: " + maleActor.level;
            damageLabel.Text = "Damage: " + maleActor.damage;
            maxHpLabel.Text = "Max HP: " + maleActor.maxHp;
            expLabel.Text = "Exp: " + maleActor.exp + "/" + maleActor.expToNextLevel;


            if (hpBar.Value <= 0)
            {
                exitNotifier.Invoke(this, 1);
            }

        }

        public int GetScore()
        {
            return maleActor.score;
        }

        // var start_button = new TextureRegion(TextureCache.Get("btn_start.png"), new RectF(0, 0, 300, 100));
        //     var imgbutton = new ImageButton(start_button);
        //     imgbutton.Position = new Vector2(800, 700);
        //     imgbutton.ButtonClicked += Button1_ButtonClicked;
        //     Add(imgbutton);


        // public void ShowUpgradeOptions()
        // {
        //     // สร้างตัวเลือกเลเวลอัพ
        //     var upgradeMenu = new Actor();



        //     var option1 = new Button("Content/Resource/Font/PixelFont.ttf", 50,
        //        Color.Black, "Increase Damage", new Vector2(500, 100));

        //     option1.ButtonClicked += (btn) =>
        //     {
        //         maleActor.damage += 5;
        //         upgradeMenu.Detach(); // เอาเมนูออก
        //     };

        //     var option2 = new Button("Content/Resource/Font/PixelFont.ttf", 50,
        //         Color.Black, "Increase Max Hp", new Vector2(500, 100));

        //     option2.ButtonClicked += (btn) =>
        //     {
        //         maleActor.maxHp += 20;
        //         maleActor.hp = maleActor.maxHp;
        //         upgradeMenu.Detach(); // เอาเมนูออก
        //     };

        //     // ตำแหน่งของปุ่ม
        //     option1.Position = new Vector2(screenSize.X / 2 - option1.RawSize.X / 2, screenSize.Y / 2 - 50);
        //     option2.Position = new Vector2(screenSize.X / 2 - option2.RawSize.X / 2, screenSize.Y / 2 + 100);

        //     upgradeMenu.Add(option1);
        //     upgradeMenu.Add(option2);
        //     Add(upgradeMenu); // เพิ่มเมนูใน PlayState
        // }


        private void SpawnEnemies()
        {
            var screenBounds = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);

            // เพิ่ม Slime
            for (int i = 0; i < slimeCount; i++)
            {
                var position = RandomUtil.Position(screenSize);
                var slime = new Slime(position);
                enermy.Add(slime);
            }

            // เพิ่ม Boss
            for (int i = 0; i < bossCount; i++)
            {
                var position = RandomUtil.Position(screenSize);
                var boss = new Boss(position);
                enermy.Add(boss);
            }
        }
    }
}




