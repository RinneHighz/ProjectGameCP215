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


            int[] prohibitTilesLayer0 = [322, 323, 483, 3083, 3084, 3163, 3164, 3243, 3244];
            var maleactor0 = new MaleActor(Layer0.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer0,prohibitTilesLayer0,2);
            collisionDetectionUnit.AddDetector(1,2);

            int[] prohibitTilesLayer1 = [2999, 3000, 3002, 3079, 3080, 3082, 3159, 3160, 3162, 3083, 3084, 3085, 3086, 3163, 3164, 3165, 3166, 3243, 3244, 3245, 3246, 92, 93, 172, 173, 252 ,253];
            var maleactor1 = new MaleActor(Layer1.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer1,prohibitTilesLayer0,2);
            collisionDetectionUnit.AddDetector(1,2);

            int[] prohibitTilesLayer2 = [2306, 2307, 2386, 2387, 2466, 2467, 2546, 2547, 791, 792, 871, 872, 1420, 
                                            3430, 3510, 3590, 3433, 3434,1908, 1909, 1988,1989, 3513, 3514, 2068, 2069, 3593, 3594, 2550, 2551, 
                                            1387, 1466, 1467, 1468, 1545, 1546, 1547, 1548, 1549, 1626, 1627, 1628, 1707, 1391, 1470, 1471, 1472, 1480, 1481, 1550, 1551, 1552, 1560, 1561, 1630, 
                                            1631, 1632, 1640, 1641, 1642, 1711, 1720, 1721, 1722, 1799, 1800, 1801, 1802,3083, 3084, 3085, 3086, 3163, 3164, 3165, 3166, 3243, 3244, 3245, 3246, 2963, 2964];
            var maleactor2 = new MaleActor(Layer2.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer2,prohibitTilesLayer2,2);
            collisionDetectionUnit.AddDetector(1,2);
            
            int[] prohibitTilesLayer3 = [1349, 1429, 1350, 3024, 3025, 3026, 1430, 3104, 3105, 3106,2099, 2100, 1504, 1506, 2179, 2180, 1584, 1586, 2259, 2260,1512, 1513,
                                            1619, 1620, 1621, 1622, 1623, 1624, 1699, 1700, 1701, 1702, 1703, 1704, 1779, 1780, 1781, 1782, 1783, 1784, 4155, 4156, 1859, 1860, 1861, 1862, 1863, 1864, 4235, 4236, 1939, 1940, 1941, 1942, 1943, 1944, 2019, 2020, 2021, 2022, 2023, 2024,
                                            1385, 1386, 1387, 1388, 1389, 1465, 1466, 1467, 1468, 1469, 1545, 1546, 1547, 1548, 1549, 1625, 1626, 1627, 1628, 1629, 1705, 1706, 1707, 1708, 1709,
                                            1479, 1480, 1481, 1482, 1559, 1560, 1561, 1562, 1639, 1640, 1641, 1642, 1719, 1720, 1721, 1722, 1799, 1800, 1801, 1802, 1879, 1880, 1881, 1882,
                                            2201, 2202, 2203, 2204, 2205, 2281, 2282, 2283, 2284, 2285, 2361, 2362, 2363, 2364, 2365, 2441, 2442, 2443, 2444, 2445,1809, 1810, 1889, 1890,1487, 1488, 1489, 1567, 1568, 1569,
                                            791, 792, 871, 872,2808, 2887, 2728,2896, 2976,3290, 3291, 3370, 3371, 3450, 3451, 3530, 3531, 3285, 3286, 3365, 3366, 3445, 3446, 3525, 3526,
                                            3045, 3046, 3125, 3126, 3205, 3206, 3842, 2843];
            var maleactor3 = new MaleActor(Layer3.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer3,prohibitTilesLayer3,2);
            collisionDetectionUnit.AddDetector(1,2);

            int[] prohibitTilesLayer4 = [1820, 1577, 1740, 1662, 1900, 1818, 1736, 1351, 1431, 1900, 1820, 1902, 1579, 1578, 1422, 1016, 1017, 1096, 1097,1386, 1387, 1388, 1466, 1467, 1468, 1545, 1546, 1547, 1548, 1549, 1625, 1626, 1627, 1628, 1629, 1707, 1016, 1017, 1096, 1097,137, 217, 375, 296, 297, 298, 135, 455, 215,
                                            3049, 3050, 3051, 3052, 3129, 3130, 3131, 3132, 3209, 3210, 3211, 3212,1647,1648];
            var maleactor4 = new MaleActor(Layer4.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer4,prohibitTilesLayer4,2);
            collisionDetectionUnit.AddDetector(1,2);

            int[] prohibitTilesLayer5 = [1480, 1481, 1559, 1560, 1561, 1562, 1719, 1720, 1721, 1722 ,1799, 1800, 1801, 1879, 1880, 1881, 1882,1391, 1470, 1471, 1472 ,1550, 1551, 1552, 1630, 1631, 1632, 1711,1649, 1650, 1647, 1648, 
                                            2808, 2887, 2888, 2816, 2896, 2976, 2976, 2970, 2728];
            var maleactor5 = new MaleActor(Layer5.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer5,prohibitTilesLayer5,2);
            collisionDetectionUnit.AddDetector(1,2);

             int[] prohibitTilesLayer6 = [1488, 1568, 1962,1961, 1647,1648,1487,1567,1960,2042,2043,2044,
                                            1480,1481,1559, 1560, 1561, 1562, 1639, 1640, 1641, 1642,
                                            1719, 1720, 1721, 1722, 1799, 1800, 1801, 1879, 1880, 1881, 2895, 2975
                                            ];
            var maleactor6 = new MaleActor(Layer6.TileCenter(1,0));
            TileMapBuilder.AddCollisions(Layer6,prohibitTilesLayer6,2);
            collisionDetectionUnit.AddDetector(1,2);

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




