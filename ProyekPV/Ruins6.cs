using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class Ruins6 : Scene
    {
        readonly Image map;
        Player p;

        public Ruins6(Player player) : base()
        {
            // add player
            p = new Player(player);
            p.area = 0;
            p.subArea = 6;
            Add(p);

            // set camera
            ApplyCamera = true;
            CameraFocus = p;

            // set map
            map = new Image("image/MapUndertale/Ruins/map7.png");
            p.map = new Texture("image/MapUndertale/Ruins/map7.png");
            AddGraphic(map, 0, 0);
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Posisi :" + p.X + "-" + p.Y);

            // logic pindah map
            if (p.X <= 35 && p.Y <= 290 && p.Y >= 200)
            {
                p.X = 4845;
                p.Y = 355;
                Game.SwitchScene(new Ruins5(p));
            }
            /*
            if (p.X >= 4865)
            {
            map 7
                p.X = 95;
                p.Y = 250;
                Game.SwitchScene(new Ruins6(p));
                Console.WriteLine("tes");
            }
            */

            // cek masuk battle
            if (Input.KeyPressed(Key.LControl))
            {
                Game.SwitchScene(new Battle(p));
            }
        }
    }
}
