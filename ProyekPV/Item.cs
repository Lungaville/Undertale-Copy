using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class Item
    {
        public int ID { get; set; }
        public string name { get; set; }
        public bool equipable { get; set; }
        public int equipType { get; set; }
        // item stats
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        // item desc
        public string desc { get; set; }

        public Item(int ID)
        {
            this.ID = ID;
            switch (this.ID) {
                case 0:
                    name = "Monster candy";
                    equipable = false;
                    hp = 10;
                    break;
                case 1:
                    name = "Butterscotch Pie";
                    equipable = false;
                    hp = 9999;
                    break;
                case 2:
                    name = "Snowman Piece";
                    equipable = false;
                    hp = 45;
                    break;
                case 3:
                    name = "Cinnamon Bunny";
                    equipable = false;
                    hp = 17;
                    break;
                case 4:
                    name = "Hot Cat";
                    equipable = false;
                    hp = 28;
                    break;
                case 5:
                    name = "Stick";
                    equipable = true;
                    atk = 0;
                    equipType = 0;
                    break;
                case 6:
                    name = "Bandage";
                    equipable = true;
                    def = 0;
                    equipType = 1;
                    break;
                case 7:
                    name = "Toy Knife";
                    equipable = true;
                    atk = 3;
                    equipType = 0;
                    break;
                case 8:
                    name = "Faded Ribbon";
                    equipable = true;
                    def = 4;
                    equipType = 1;
                    break;
                case 9:
                    name = "Ballet Shoes";
                    equipable = true;
                    atk = 7;
                    equipType = 0;
                    break;
                case 10:
                    name = "Old Tutu";
                    equipable = true;
                    def = 10;
                    equipType = 1;
                    break;
                case 11:
                    name = "Burnt Pan";
                    equipable = true;
                    atk = 10;
                    equipType = 0;
                    break;
                case 12:
                    name = "Stained Apron";
                    equipable = true;
                    def = 110;
                    equipType = 1;
                    break;
                case 13:
                    name = "Real Knife";
                    equipable = true;
                    atk = 99;
                    equipType = 0;
                    break;
                case 14:
                    name = "The Locket";
                    equipable = true;
                    def = 99;
                    equipType = 1;
                    break;
                case 15:
                    name = "Broke Helmet";
                    equipable = true;
                    def = 3;
                    equipType = 2;
                    break;
                case 16:
                    name = "Ring of health";
                    equipable = true;
                    // give +5 HP every turn
                    equipType = 2;
                    break;
                case 17:
                    name = "Magic stick";
                    equipable = true;
                    atk = 3;
                    def = 6;
                    equipType = 2;
                    break;
                case 18:
                    name = "Aquila ring";
                    equipable = true;
                    atk = 12;
                    def = 8;
                    equipType = 2;
                    break;
                case 19:
                    name = "Heart locket";
                    equipable = true;
                    hp = 30;
                    equipType = 2;
                    break;


            }
        }
    }
}
