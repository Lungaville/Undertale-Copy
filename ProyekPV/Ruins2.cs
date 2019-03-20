using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class Ruins2 : Scene
    {
        readonly Image map;
        Player p;

        public Ruins2(Player player) : base()
        {
            // add player
            p = new Player(player);
            p.area = 0;
            p.subArea = 2;
            Add(p);

            // set camera
            ApplyCamera = true;
            CameraFocus = p;

            // set map
            map = new Image("image/MapUndertale/Ruins/map3.png");
            p.map = new Texture("image/MapUndertale/Ruins/map3.png");
            AddGraphic(map, 0, 0);
        }

        public override void Update()
        {
            base.Update();

            Console.WriteLine("Posisi :" + p.X + "-" + p.Y);

            // logic pindah map
            if (p.X >= 340 && p.X <= 430 && p.Y >= 990)
            {
                p.X = 260;
                p.Y = 315;
                Game.SwitchScene(new Ruins1(p));
            }
            if (p.X >= 360 && p.X <= 410 && p.Y <=300)
            {
                p.X = 320;
                p.Y = 470;
                Game.SwitchScene(new Ruins3(p));

            }

            // cek masuk battle
            if (Input.KeyPressed(Key.LControl))
            {
                Game.SwitchScene(new Battle(p));
            }
        }
    }
}
