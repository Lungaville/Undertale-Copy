using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPV
{
    class MainMenu : Scene
    {
        private ButtonEntity buttonNewGame, buttonLoadGame;
        private bool newGameSelected;
        public float temp;
        private int timer = 1;
        private Random rand = new Random();
        private int randomtext;
        private BackgroundEntity logo = new BackgroundEntity(40, 50, "image/Mainmenu/Undertale_Logo.png");
        private ImageEntity background = new ImageEntity(0, 0, "image/Mainmenu/back.png");
        private AnimatingEntity sans = new AnimatingEntity(260, 210);
        private TalkEntity speech;
        private Text textspeech;

        public MainMenu() : base()
        {
            // add button image
            buttonNewGame = new ButtonEntity(120, 420);
            buttonLoadGame = new ButtonEntity(380, 420);
            TextEntity textnewgame = new TextEntity(125,430, "New Game");
            TextEntity textloadgame = new TextEntity(380, 430, "Load Game");
            // add button to scene
            randomtext = rand.Next(0, 3);
            Add(background);
            Add(logo);
            Add(sans);
            Add(buttonNewGame);
            Add(buttonLoadGame);
            Add(textnewgame);
            Add(textloadgame);
            newGameSelected = true;
        }

        public override void Update()
        {
            base.Update();

            // logic pindah map
            // Game.SwitchScene(new Map2(ref p));
            if (Input.KeyPressed(Key.Left))
            {
                buttonNewGame.button.Color = Color.FromBytes(71, 72, 67);
                buttonLoadGame.button.Color = Color.Black;
                newGameSelected = true;
            }
            if (Input.KeyPressed(Key.Right))
            {
                buttonNewGame.button.Color = Color.Black;
                buttonLoadGame.button.Color = Color.FromBytes(71, 72, 67);
                newGameSelected = false;
            }
            if (Input.KeyPressed(Key.Space))
            {
                if (newGameSelected)
                {
                    Player p = new Player(1000, 270);
                    Game.SwitchScene(new Ruins0(p));
                }
            }
            if (logo.backcomponent.Entity.GetGraphic<Image>().Alpha == 1)
            {
                Add(new BubbleChat(360, 210, "image/Mainmenu/talk.png"));
                timer++;
                Add(new TalkEntity(390, 220, randomtext,timer/15));
            }
        }
    }

    public class ButtonEntity : Entity
    {
        public Image button;

        public ButtonEntity(float x, float y) : base(x, y)
        {
            button = Image.CreateRectangle(100, 50, Color.White);
            button.Color = Color.Black;
            AddGraphic(button);
        }
    }
    public class ImageEntity : Entity
    {
        public ImageEntity(float x, float y, string imagePath) : base(x, y)
        {
            // Create an Image using the path passed in with the constructor
            var image = new Image(imagePath);
            // Center the origin of the Image
            // Add the Image to the Entity's Graphic list.
            AddGraphic(image);
        }
    }
    public class TalkEntity : Entity
    {
        public TalkEntity(float x, float y, int talk,int index) : base(x, y)
        {
            string stringdisplay="";
                switch (talk)
                {
                    case 0:
                        stringdisplay = "Do you wanna    have a bad time?";
                        break;
                    case 1:
                        stringdisplay = "Hey, let's play a game human.";
                        break;
                    case 2:
                        stringdisplay = "I'm sans, Sans  the skeleton.";
                        break;
                }
            if (index<=stringdisplay.Count())
            {
                string temp = stringdisplay.Substring(0, index);
                if (temp.Count() > 16)
                {
                    temp = stringdisplay.Substring(0, 16);
                    temp += "\n";
                }
                Text display = new Text(temp, 16);
                display.Color = Color.Black;
                AddGraphic(display);
                if (temp.Count() > 16)
                {
                    string temp2 = stringdisplay.Substring(16, index - 16);
                    Console.WriteLine(temp2);
                    Text display2 = new Text(temp2, 16);
                    display2.Color = Color.Black;
                    AddGraphic(display2, 0, 30);
                }
            }
            else
            {
                Text display = new Text(stringdisplay.Substring(0, 16),16);
                display.Color = Color.Black;
                AddGraphic(display);
                Text display2 = new Text(stringdisplay.Substring(16, stringdisplay.Count()-16), 16);
                display2.Color = Color.Black;
                AddGraphic(display2, 0, 30);
            }
        }
    }
    public class TextEntity : Entity
    {
        public TextEntity(float x, float y, string text) : base(x, y)
        {
            Text display = new Text(text, 20);
            display.Color = Color.White;
            AddGraphic(display);
        }
    }
    public class BackgroundComponent : Component
    {
        public override void Update()
        {
            base.Update();
            float alpha = Entity.GetGraphic<Image>().Alpha;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
            }
            else
            {
                alpha += 0.0025f;
            }
            Entity.GetGraphic<Image>().Alpha = alpha;
        }
    }

    public class BackgroundEntity : Entity
    {
        public BackgroundComponent backcomponent = new BackgroundComponent();
        public BackgroundEntity(float x, float y, string imagePath) : base(x, y)
        {
            var image = new Image(imagePath);
            image.Alpha = 0.0f;
            AddGraphic(image);
            AddComponents(backcomponent);
        }
    }

    public class BubbleChat : Entity
    {
        public BubbleChat(float x, float y, string imagePath) : base(x, y)
        {
            var image = new Image(imagePath);
            AddGraphic(image);
        }
    }

    public class AnimatingEntity : Entity
    {
        enum Animation
        {
            idle,idle2
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("image/Mainmenu/Sans.png", 112, 151);
        public AnimatingEntity(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.idle, "0,1,2,3,4,5,6,7,8,9,10,11,12", 4);
            spritemap.Play(Animation.idle);
            AddGraphic(spritemap);
        }
    }
}
