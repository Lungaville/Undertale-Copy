using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class Ruins3 : Scene
    {
        readonly Image map;
        Player p;

        public Ruins3(Player player) : base()
        {
            // add player
            p = new Player(player);
            p.area = 0;
            p.subArea = 3;
            Add(p);

            // set camera
            ApplyCamera = true;
            CameraFocus = p;

            // set map
            map = new Image("image/MapUndertale/Ruins/map4.png");
            p.map = new Texture("image/MapUndertale/Ruins/map4.png");
            AddGraphic(map, 0, 0);
        }

        public override void Update()
        {
            base.Update();
         //   Console.WriteLine("Posisi :" + p.X + "-" + p.Y);

            // logic pindah map
            if (p.X >= 325 && p.X <= 405 && p.Y >= 545)
            {
                p.X = 386;
                p.Y = 306;
                Game.SwitchScene(new Ruins2(p));
            }
            if (p.X >= 285 && p.X <= 370 && p.Y <= 270)
            {
                Console.WriteLine("Tes");
                p.X = 320;
                p.Y = 455;
                Game.SwitchScene(new Ruins4(p));

            }

            // cek masuk battle
            if (Input.KeyPressed(Key.LControl))
            {
                Game.SwitchScene(new Battle(p));
            }
        }
    }
}
