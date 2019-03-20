using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace ProyekPV
{
    public class Player
    {
        public int posX;
        public int posY;
        public string sprites="bawah0.png";
        public bool right, left, up, down;
        public int ctrAnimasi=0;
        Timer animasiJalan;
        public Player(int posX,int posY)
        {
            this.posX = posX;
            this.posY = posY;
            setAnimasiJalan();
        }
        public void setAnimasiJalan()
        {
            animasiJalan = new Timer(100);
            animasiJalan.Elapsed += (sender, e) => WalkAnimation(sender, e, this);
            animasiJalan.Start();
        }
        public void changeImageWalk(string arah, int batas)
        {
            ctrAnimasi = ctrAnimasi % batas;
            sprites = arah + ctrAnimasi + ".png";
           ctrAnimasi++;
           ctrAnimasi = ctrAnimasi % batas;
        }
        private static void WalkAnimation(Object source, ElapsedEventArgs e,Player player)
        {
            if (player.up)
            {
                player.changeImageWalk("atas", 4);
            }
            else if (player.down)
            {
                player.changeImageWalk("bawah", 4);
            }
            else if (player.left)
            {
                player.changeImageWalk("kiri", 2);
            }
            else if(player.right)
            {
                player.changeImageWalk("kanan", 2);
            }
            else
            {
                player.ctrAnimasi = 0;
                player.sprites = player.sprites.Substring(0, player.sprites.Length - 5) + "0.png";
            }
        }

    }

}
