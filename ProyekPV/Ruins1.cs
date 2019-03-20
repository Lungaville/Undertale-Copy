using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV {
    class Ruins1 : Scene {
        readonly Image map;
        Player p;

        public Ruins1(Player player) : base() {
            // add player
            p = new Player(player);
            p.area = 0;
            p.subArea = 1;
            Add(p);

            // set camera
            ApplyCamera = true;
            CameraFocus = p;

            // set map
            map = new Image("image/MapUndertale/Ruins/map2.png");
            p.map = new Texture("image/MapUndertale/Ruins/map2.png");
            AddGraphic(map, 0, 0);
        }

        public override void Update() {
            base.Update();

            Console.WriteLine("Posisi :" + p.X + "-" + p.Y);

            // logic pindah map
            if (p.X >= 210 && p.X <= 290 && p.Y >= 845) {
                p.X = 150;
                p.Y = 370;
                Game.SwitchScene(new Ruins0(p));
            }
            if (p.X >= 210 && p.X <= 290 && p.Y<=275)
            {
                p.X = 380;
                p.Y = 980;
                Game.SwitchScene(new Ruins2(p));

            }

            // cek masuk battle
            if (Input.KeyPressed(Key.LControl)) {
                Game.SwitchScene(new Battle(p));
            }
        }
    }
}
