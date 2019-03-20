using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// to do
// 1. cek/get player item for mercy or reduce enemy stats

namespace ProyekPV {
    class Enemy : Entity {
        private Random r = new Random();
        private Player player;

        // sprite
        private enum Animation {
            Normal,
            Attack,
            Hurt
        }
        private Spritemap<Animation> spritemap;

        // attribute
        public string name { get; set; }
        public int id { get; set; } // froggit, icecap, lesser dog, pyrope, knight knight, toriel, papyrus, asgore
        public int hp { get; set; }
        public int atk { get; set; }
        public int exp { get; set; }
        public int gold { get; set; }
        public int status { get; set; } // 0 = inBattle, -1 = killed, 1 = mercy

        // mercy-related
        private int attackedCount;
        private int mercyAttemptCount;

        public int convinceCount { get; set; }
        public int complimentCount { get; set; }
        public int hugCount { get; set; }
        public int persuadeCount { get; set; }
        public int petCount { get; set; }
        public bool singLulabby { get; set; }
        public int[] actOption { get; set; } // convince = 0, sing Lulabby = 5, ...

        public Enemy(Player p, bool isBoss = false) : base(290, 10) {
            player = new Player(p);
            int[] spriteFrame = new int[1];

            if (!isBoss) {
                // random tipe musuh berdasarkan area player
                bool validRandom = false;
                while (!validRandom) {
                    int indexMusuh = r.Next(0, 5);
                    if (indexMusuh == 0 && p.area == 0) {
                        name = "Froggit";
                        validRandom = true;
                        // attribute
                        id = indexMusuh;
                        hp = 9;
                        atk = 6;
                        exp = 3;
                        gold = 16;
                        // mercy-related
                        actOption = new int[1];
                        actOption[0] = 0;

                        // game-related
                        spritemap = new Spritemap<Animation>(@"image\enemy\froggit.png", 112, 112);
                        spriteFrame = new int[2];
                    } else if (indexMusuh == 1 && (p.area == 0 || p.area == 1)) {
                        name = "Icecap";
                        validRandom = true;

                        id = indexMusuh;
                        hp = 23;
                        atk = 4;
                        exp = 5;
                        gold = 25;

                        actOption = new int[2];
                        actOption[0] = 0;
                        actOption[1] = 1;

                        spritemap = new Spritemap<Animation>(@"image\enemy\icecap.png", 106, 171);
                        spriteFrame = new int[9];
                    } else if (indexMusuh == 2 && p.area == 1) {
                        name = "Lesser Dog";
                        validRandom = true;

                        id = indexMusuh;
                        hp = 18;
                        atk = 11;
                        exp = 9;
                        gold = 42;

                        actOption = new int[1];
                        actOption[0] = 4;

                        spritemap = new Spritemap<Animation>(@"image\enemy\lesserdog.png", 102, 182);
                        spriteFrame = new int[2];
                    } else if (indexMusuh == 3 && (p.area == 1 || p.area == 2)) {
                        name = "Pyrope";
                        validRandom = true;

                        id = indexMusuh;
                        hp = 34;
                        atk = 18;
                        exp = 16;
                        gold = 63;

                        actOption = new int[2];
                        actOption[0] = 0;
                        actOption[1] = 2;

                        spritemap = new Spritemap<Animation>(@"image\enemy\pyrope.png", 137, 287);
                        spriteFrame = new int[41];
                    } else if (indexMusuh == 4 && p.area == 2) {
                        name = "Knight knight";
                        validRandom = true;

                        id = indexMusuh;
                        hp = 43;
                        atk = 26;
                        exp = 25;
                        gold = 23;

                        actOption = new int[0];
                        actOption[0] = 1;

                        spritemap = new Spritemap<Animation>(@"image\enemy\knightknight.png", 213, 229);
                        spriteFrame = new int[400];
                    }
                }
            } else {
                // set boss
            }

            // gambar
            for (int i = 0; i < spriteFrame.Count(); i++) {
                spriteFrame[i] = i;
            }
            spritemap.Add(Animation.Normal, spriteFrame, 30);
            spritemap.Play(Animation.Normal);
            AddGraphic(spritemap);

            // set default
            status = 0;

            attackedCount = 0;
            convinceCount = 0;
            complimentCount = 0;
            persuadeCount = 0;
            petCount = 0;
            hugCount = 0;
            mercyAttemptCount = 0;
            singLulabby = false;
        }

        public bool canMercy() {
            mercyAttemptCount++;

            if (id == 0 && (attackedCount >= 1 || convinceCount >= 1)) {
                status = 1;
                return true;
            } else if (id == 1 && (convinceCount >= 3 || complimentCount >= 2) && attackedCount == 0) {
                status = 1;
                return true;
            } else if (id == 2 && (petCount >= 2 && attackedCount == 0) || false) { // to do, cek jika player pakai item stick
                status = 1;
                return true;
            } else if (id == 3 && (attackedCount >= 3 || (convinceCount >= 3 && attackedCount == 0) || (convinceCount >= 1 && hugCount == 1 && attackedCount == 0))) {
                status = 1;
                return true;
            } else if (id == 4 && (hp <= 15 || false)) { // to do, cek jika player equip item ? atau semua monster telah di spare
                status = 1;
                return true;
            } else if (id == 5 && persuadeCount >= 1 && convinceCount >= 1 && mercyAttemptCount >= 17) {
                status = 1;
                return true;
            } else if (id == 6 && ((hp <= 25 && convinceCount >= 1) || (complimentCount >= 1 && convinceCount >= 1 && singLulabby && attackedCount == 0))) {
                status = 1;
                return true;
            } else if (id == 7 && player.level == 1 && player.exp == 0 && mercyAttemptCount >= 5 && attackedCount == 0) {
                status = 1;
                return true;
            }

            return false;
        }

        public void attacked(float sliderX) {
            float atkPercent = 1 - ((330 - sliderX) / 330);
            if (atkPercent < 0)
                atkPercent *= -1;

            hp -= (int)(player.atk * atkPercent);
        }
    }
}
