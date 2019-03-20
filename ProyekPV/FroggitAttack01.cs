using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
namespace ProyekPV
{
    class FroggitAttack01 : Entity
    {
        Image gambarParticle;
        Random rand;
        public FroggitAttack01() : base()
        {
            rand = new Random();
            this.X = rand.Next(160) + 240;
            this.Y = 240;
            gambarParticle = new Image(@"image/projectile/projectile0.png");
            Graphic = gambarParticle;
            Graphic.CenterOrigin();
            SetHitbox(25, 25, Global.Type.PARTICLE);
            Collider.CenterOrigin();
        }
        public override void Update()
        {
            base.Update();
            this.Y++;
            if (this.Y>400)
            {
                RemoveSelf();
            }
        }
        public override void Render()
        {
            base.Render();
            Collider.Render();
        }

    }
    class FroggitPatern 
    {
        public Scene temp;
        FroggitAttack01 a = new FroggitAttack01();
        public FroggitPatern(Scene a)
        {
            temp = a;
            
        }
        public void tes()
        {
            temp.Add(a);
        }
    }

}
