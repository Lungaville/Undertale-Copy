using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV {
    class Battle : Scene {
        private Player player;
        private Enemy enemy;
        // state
        private enum State {
            Option,
            Attack,
            Act,
            Item,
            EnemyTurn
        }
        private State currentState;
        // options button/index
        private BattleOption[] battleOptions;
        private ActOption[] actOptions;
        private int selectedOption;
        private int selectedAct;
        // player attack
        private AttackSlider attackSlider;
        // enemy attack
        private EnemyAttack enemyAttack;
        private PlayerHeart Heart;
        public Battle(Player player) : base() {
            // add player
            this.player = new Player(player);
            this.player.onBattle = true;
            // make enemy
            enemy = new Enemy(this.player);
            Add(enemy);
            // set State.Option
            battleOptions = new BattleOption[4];
            for (int i = 0; i < 4; i++) {
                battleOptions[i] = new BattleOption(i);
                Add(battleOptions[i]);
            }
            // set State.Act
            actOptions = new ActOption[enemy.actOption.Count()];
            for (int i = 0; i < actOptions.Count(); i++) {
                actOptions[i] = new ActOption(i,enemy.actOption[i]);
                Add(actOptions[i]);
                actOptions[i].Visible = false;
            }
            // set State.Attack
            attackSlider = new AttackSlider();
            attackSlider.slider.Visible = false;
            Add(attackSlider);
            // set State.Item

            // set State.EnemyAttack
            enemyAttack = new EnemyAttack();
            enemyAttack.Visible = false;
            Add(enemyAttack);
            // set default index/state
            selectedOption = 0;
            selectedAct = 0;
            currentState = State.Option;

            Heart = new PlayerHeart(this.player);
            Add(Heart);
        }

        public override void Update() {
            base.Update();
            
            switch (currentState) {
                case State.Option:
                    onOption();
                    break;
                case State.Attack:
                    onAttack();
                    break;
                case State.Act:
                    onAct();
                    break;
                case State.Item:
                    onItem();
                    break;
                case State.EnemyTurn:
                    onEnemyTurn();
                    break;
            }

        }

        public void onOption(){
            // select option
            if (Input.KeyPressed(Key.Left)) {
                if (selectedOption > 0) {
                    battleOptions[selectedOption].menuIdle.Alpha = 1;
                    selectedOption--;
                    battleOptions[selectedOption].menuIdle.Alpha = 0;
                }
            }
            if (Input.KeyPressed(Key.Right)) {
                if (selectedOption < 3) {
                    battleOptions[selectedOption].menuIdle.Alpha = 1;
                    selectedOption++;
                    battleOptions[selectedOption].menuIdle.Alpha = 0;
                }
            }

            // execute option
            if (Input.KeyPressed(Key.Space)) {
                if (selectedOption == 0) { // attack
                    attackSlider.slider.Visible = true;
                    currentState = State.Attack;
                }

                if (selectedOption == 1) { // act
                    currentState = State.Act;
                    for (int i = 0; i < actOptions.Count(); i++) {
                        actOptions[i].Visible = true;
                    }
                }

                if(selectedOption == 2){ // item
                    // state = State.Item;
                }

                // mercy
                if (selectedOption == 3 && enemy.canMercy()) { // mercy success
                    battleEnded();
                }else if (selectedOption == 3) { // mercy fail
                    currentState = State.EnemyTurn;
                    enemyAttack.Visible = true;
                }
            }
        }

        public void onAttack() {
            if(Input.KeyPressed(Key.Space)){
                attackSlider.slider.Visible = false;

                enemy.attacked(attackSlider.X);
                if (enemy.hp <= 0) {
                    battleEnded();
                } else {
                    enemyAttack.Visible = true;
                    currentState = State.EnemyTurn;
                }
            }
        }

        public void onAct() {
            // set color if there's only 1 - 2 act option
            if (actOptions.Count() == 1) {
                actOptions[0].txt.Color = Color.Blue;
            }

            // select act
            if (Input.KeyPressed(Key.A)) {
                if (selectedAct > 0) {
                    actOptions[selectedAct].txt.Color = Color.Grey;
                    selectedAct--;
                    actOptions[selectedAct].txt.Color = Color.Blue;
                }
            }
            if (Input.KeyPressed(Key.D)) {
                if (selectedAct < actOptions.Count() - 1) {
                    actOptions[selectedAct].txt.Color = Color.Grey;
                    selectedAct++;
                    actOptions[selectedAct].txt.Color = Color.Blue;
                }
            }

            // back to option
            if (Input.KeyPressed(Key.X)) {
                currentState = State.EnemyTurn;
                for (int i = 0; i < actOptions.Count(); i++) {
                    actOptions[i].Visible = false;
                }
            }

            // execute act
            if (Input.KeyPressed(Key.Space)) {
                switch (enemy.actOption[selectedAct]) {
                    case 0:
                        enemy.convinceCount++;
                        break;
                    case 1:
                        enemy.complimentCount++;
                        break;
                    case 2:
                        enemy.hugCount++;
                        break;
                    case 3:
                        enemy.persuadeCount++;
                        break;
                    case 4:
                        enemy.petCount++;
                        break;
                    case 5:
                        enemy.singLulabby = true;
                        break;
                }

                // change state
                enemyAttack.Visible = true;
                currentState = State.EnemyTurn;
                for (int i = 0; i < actOptions.Count(); i++) {
                    actOptions[i].Visible = false;
                }
            }
        }
                
        public void onItem(){

        }

        public void onEnemyTurn(){
            Console.WriteLine(player.hp);
            // enemy done attack
            if (Input.KeyPressed(Key.Q)) {
                enemyAttack.Visible = false;
                currentState = State.Option;
            }
            // player heart move
            if(Input.KeyDown(Key.Up) && Heart.Y > 250) {
             //   Heart.playerHeart.Y-=2;
                Heart.Y -= 2;
            }else if (Input.KeyDown(Key.Down) && Heart.Y < 390) {
              //  Heart.playerHeart.Y += 2;
                Heart.Y += 2;
            }
            if (Input.KeyDown(Key.Left) && Heart.X > 250) {
               // Heart.playerHeart.X -= 2;
                Heart.X -= 2;
            } else if (Input.KeyDown(Key.Right) && Heart.X < 390) {
               // Heart.playerHeart.X += 2;
                Heart.X += 2;
            }
           

        }

        public void battleEnded() {
            player.onBattle = false;

            // enemy killed/spared
            if(enemy.status==-1){ // killed
                player.battleEnded(enemy.gold, enemy.exp);
                Console.WriteLine("enemy killed");
            }else if(enemy.status==1){ // spared
                player.battleEnded(enemy.gold / 2);
                Console.WriteLine("enemy mercy");
            }
            // TO DO - player dead

            // return to map
            if(player.area==0){
                switch (player.subArea) {
                    case 0:
                        Game.SwitchScene(new Ruins0(player));
                        break;
                    case 1:
                        Game.SwitchScene(new Ruins1(player));
                        break;
                    case 2:
                        Game.SwitchScene(new Ruins2(player));
                        break;
                    case 3:
                        Game.SwitchScene(new Ruins3(player));
                        break;
                    case 4:
                        Game.SwitchScene(new Ruins4(player));
                        break;
                    case 5:
                        Game.SwitchScene(new Ruins5(player));
                        break;
                    case 6:
                        Game.SwitchScene(new Ruins6(player));
                        break;
                }
            } else if (player.area == 1) {

            }else if(player.area == 2){

            }
        }
        
    }

    class BattleOption : Entity {
        public Image menuIdle;
        public Image menuActive;
        public BattleOption(int index) : base(index==0? 20 : 10+index*160,410) {
            menuIdle = new Image(@"image/battleMenu/idle" +index+".png");
            menuActive = new Image(@"image/battleMenu/active" + index + ".png");
             
            AddGraphic(menuActive);
            AddGraphic(menuIdle);

        }
    }

    class ActOption : Entity{
        public Text txt;

        public ActOption(int index, int actOption) : base (index == 0 ? 20 : 10 + index * 160, 380){
            switch (actOption) {
                case 0:
                    txt = new Text("Convince", 16);
                    break;
                case 1:
                    txt = new Text("Compliment", 16);
                    break;
                case 2:
                    txt = new Text("Hug", 16);
                    break;
                case 3:
                    txt = new Text("Persuade", 16);
                    break;
                case 4:
                    txt = new Text("Pet", 16);
                    break;
                case 5:
                    txt = new Text("Sing Lulabby", 16);
                    break;
            }

            txt.Color = Color.Grey;
            AddGraphic(txt);
        }
    }

    class AttackSlider : Entity{
        public Image slider;
        public bool arahKanan;

        public AttackSlider() : base( 20, 370){
            slider = Image.CreateRectangle(30, 30, Color.Red);
            arahKanan = true;
            AddGraphics(slider);
        }

        public override void Update() {
            base.Update();

            if (arahKanan && X >= 600){
                arahKanan = false;
            }else if(!arahKanan && X <= 20){
                arahKanan = true;
            }

            X += arahKanan ? 10 : -10;
        }
    }
    class PlayerHeart : Entity
    {
        public Image playerHeart;
        Player p;
        BoxCollider collider = new BoxCollider(20, 20, Global.Type.PLAYER);
        public PlayerHeart(Player pk) : base(240, 240)
        {
            p = pk;
            playerHeart = new Image(@"image/battle/playerHeart0.png");
            AddGraphic(playerHeart);
            playerHeart.CenterOrigin();
            AddCollider(collider);
            collider.CenterOrigin();
        }
        public override void Update()
        {
            base.Update();
            if (collider.Overlap(X,Y,Global.Type.PARTICLE))
            {
                p.hp -= 1;
                Console.WriteLine(p.hp);
            }
            
        }
        public override void Render()
        {
            base.Render();
            Collider.Render();
        }

    }
    class EnemyAttack : Entity
    {
        Image area;
        int waktuSpawn = 100;
        int ctr = 0;
        public EnemyAttack() : base(240, 240)
        {
            area = Image.CreateRectangle(160, 160, Color.None);
            area.OutlineColor = Color.White;
            area.OutlineThickness = 3;
           AddGraphic(area);

            
        }
        public override void Update()
        {
            base.Update();
            ctr++;
            if (ctr>=waktuSpawn)
            {
                ctr = 0;
                Scene.Add(new FroggitAttack01());
            }

        }

    }
  
   

}
