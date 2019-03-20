using Otter;

namespace ProyekPV {
    class Ruins0 : Scene {
        private readonly Image map;
        private Player player;
        private Menu menu;
        private bool onMenu;

        public Ruins0(Player p) : base() {
            // add player
            player = new Player(p);
            player.area = 0;
            player.subArea = 0;
            Add(player);

            // set camera
            ApplyCamera = true;
            CameraFocus = player;

            // set map
            map = new Image("image/MapUndertale/Ruins/map1.png");
            AddGraphic(map, 0, 0);

            // set menu
            menu = new Menu(player.items,player.armor,player.weapon,player.accessories);
            menu.Visible = false;
            Add(menu);
            onMenu = false;
        }

        public override void Update() {
            base.Update();

            if (!onMenu) {
                menu.Visible = false;

                // logic pindah map
                if (player.X >= 140 && player.X <= 230 && player.Y <= 350) {
                    player.X = 230;
                    player.Y = 640;
                    Game.SwitchScene(new Ruins1(player));
                }

                // cek masuk battle
                if (Input.KeyPressed(Key.LControl)) {
                    Game.SwitchScene(new Battle(player));
                }

                if (Input.KeyReleased(Key.Z)) {
                    menu.open(CameraX,CameraY);
                    menu.Visible = true;
                    onMenu = true;
                }
            } else {
                if (!menu.onMenu) {
                    menu.Visible = false;
                    onMenu = false;
                    
                    player.items = menu.items;
                    player.armor = menu.armor;
                    player.weapon = menu.weapon;
                    player.accessories = menu.accessories;
                }
            }
        }

    }
}
