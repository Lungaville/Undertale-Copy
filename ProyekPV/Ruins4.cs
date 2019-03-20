using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class Ruins4 : Scene
    {
        readonly Image map;
        Player p;

        public Ruins4(Player player) : base()
        {
            // add player
            p = new Player(player);
            p.area = 0;
            p.subArea = 4;
            Add(p);

            // set camera
            ApplyCamera = true;
            CameraFocus = p;

            // set map
            map = new Image("image/MapUndertale/Ruins/map5.png");
            p.map = new Texture("image/MapUndertale/Ruins/map5.png");
            AddGraphic(map, 0, 0);
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Posisi :" + p.X + "-" + p.Y);

            // logic pindah map
            if (p.X >= 280 && p.X <= 350 && p.Y >= 470)
            {
                p.X = 330;
                p.Y = 290;
                Game.SwitchScene(new Ruins3(p));
            }
            if (p.X >= 1791 && p.X <= 1873 && p.Y <= 180)
            {
                p.X = 380;
                p.Y = 440;
                Game.SwitchScene(new Ruins5(p));

            }

            // cek masuk battle
            if (Input.KeyPressed(Key.LControl))
            {
                Game.SwitchScene(new Battle(p));
            }
        }
    }
}
