using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ProyekPV
{
    class Menu : Entity
    {
        // other
        public bool onMenu { get; set; }
        private int selectIndex; // reset when enum changed on some cases
        private Select currentSelect;
        enum Select
        {
            StatusItem, // select status/item
            Status,
            Item,
            UseInfo, // select use/info
            Use,
            Info
        }
        // player items
        public List<Item> items { get; set; }
        public Item armor { get; set; }
        public Item weapon{ get; set; }
        public Item accessories { get; set; }
        // select item/status - Select.StatusItem
        private Image btnItem, btnStatus;
        private Text txtItem, txtStatus;
        // select items - Select.Item
        private Image[] btnItems;
        private Text[] txtItems;
        // select use/info - Select.UseInfo
        private Image btnUse, btnInfo;
        private Text txtUse, txtInfo;

        public Menu(List<Item> items,Item armor,Item weapon, Item accessories) : base(40, 40)
        {
            // other
            onMenu = false;
            selectIndex = 0;
            currentSelect = Select.StatusItem;
            // get player item
            this.items = items;
            this.armor = armor;
            this.weapon = weapon;
            this.accessories = accessories;
            // select item button - Select.StatusItem
            btnItem = Image.CreateRectangle(80, 40, Color.White);
            btnItem.SetPosition(0, 0);
            AddGraphic(btnItem);
            txtItem = new Text("Item", 16);
            txtItem.SetPosition(0, 0);
            AddGraphic(txtItem);
            // select status button - Select.StatusItem
            btnStatus = Image.CreateRectangle(80, 40, Color.White);
            btnStatus.SetPosition(0, 50);
            AddGraphic(btnStatus);
            txtStatus = new Text("Status", 16);
            txtStatus.SetPosition(0, 50);
            AddGraphic(txtStatus);
            // Select.Status

            // select items button - Select.Item
            btnItems = new Image[this.items.Count];
            txtItems = new Text[this.items.Count];
            for (int i = 0; i < this.items.Count; i++) {
                btnItems[i] = Image.CreateRectangle(80, 40, Color.White);
                btnItems[i].SetPosition(100, 50 * i);
                btnItems[i].Visible = false;
                AddGraphic(btnItems[i]);
                txtItems[i] = new Text(this.items[i].name, 16);
                txtItems[i].SetPosition(100, 50 * i);
                txtItems[i].Visible = false;
                AddGraphic(txtItems[i]);
            }
            // select use - Select.UseInfo
            btnUse = Image.CreateRectangle(80, 40, Color.White);
            btnUse.SetPosition(100, 50 * (this.items.Count + 2));
            btnUse.Visible = false;
            AddGraphic(btnUse);
            txtUse = new Text("use", 16);
            txtUse.SetPosition(100, 50 * (this.items.Count + 2));
            txtUse.Visible = false;
            AddGraphic(txtUse);
            // select use - Select.UseInfo
            btnInfo = Image.CreateRectangle(80, 40, Color.White);
            btnInfo.SetPosition(210, 50 * (this.items.Count + 2));
            btnInfo.Visible = false;
            AddGraphic(btnInfo);
            txtInfo = new Text("Info", 16);
            txtInfo.SetPosition(210, 50 * (this.items.Count + 2));
            txtInfo.Visible = false;
            AddGraphic(txtInfo);
            // use selected - Select.Use

            // info selected - Select.Use
        }

        public void open(float x, float y) {
            onMenu = true;
            currentSelect = Select.StatusItem;
            selectIndex = 0;

            btnItem.Visible = true;
            txtItem.Visible = true;
            btnStatus.Visible = true;
            txtStatus.Visible = true;

            X = x;
            Y = y;
        }

        public override void Update()
        {
            base.Update();

            if (onMenu) {
                switch (currentSelect) {
                    case Select.StatusItem:
                        onStatusItem();
                        break;
                    case Select.Status:
                        onStatus();
                        break;
                    case Select.Item:
                        onItem();
                        break;
                    case Select.UseInfo:
                        onUseInfo();
                        break;
                    case Select.Use:
                        onUse();
                        break;
                    case Select.Info:
                        onInfo();
                        break;
                }
            }
        }

        public void onStatusItem()
        {
            if (Input.KeyPressed(Key.Up)) {
                btnItem.Color = Color.Grey;
                btnStatus.Color = Color.White;
                selectIndex = 0;
                Console.WriteLine("w");
            } else if (Input.KeyPressed(Key.Down)) {
                btnItem.Color = Color.White;
                btnStatus.Color = Color.Grey;
                selectIndex = 1;
                Console.WriteLine("s");
            }

            if (Input.KeyPressed(Key.Z)) {
                if (selectIndex == 0) {
                    currentSelect = Select.Item;
                    for (int i = 0; i < items.Count; i++) {
                        btnItems[i].Visible = true;
                        txtItems[i].Visible = true;
                    }
                } else if (selectIndex == 1) {
                    currentSelect = Select.Status;
                    // set status text/btn to visible
                }
                selectIndex = 0;
            } else if (Input.KeyPressed(Key.X)) {
                onMenu = false;
            }
        }

        public void onStatus()
        {
            if (Input.KeyPressed(Key.X)) {
                // set status text/btn to invisible
                currentSelect = Select.StatusItem;
            }
        }

        public void onItem()
        {
            if (Input.KeyPressed(Key.Up) && selectIndex > 0) {
                btnItems[selectIndex].Color = Color.White;
                selectIndex--;
                btnItems[selectIndex].Color = Color.Grey;
            } else if (Input.KeyPressed(Key.Down) && selectIndex < items.Count - 1) {
                btnItems[selectIndex].Color = Color.White;
                selectIndex++;
                btnItems[selectIndex].Color = Color.Grey;
            }

            if (Input.KeyPressed(Key.Z)) {
                currentSelect = Select.UseInfo;
                btnUse.Visible = true;
                txtUse.Visible = true;
                btnInfo.Visible = true;
                txtInfo.Visible = true;
            } else if (Input.KeyPressed(Key.X)) {
                for (int i = 0; i < items.Count; i++) {
                    btnItems[i].Visible = false;
                    txtItems[i].Visible = false;
                }
                currentSelect = Select.StatusItem;
            }
        }

        public void onUseInfo()
        {
            if (Input.KeyPressed(Key.Left)) {
                btnUse.Color = Color.Grey;
                btnInfo.Color = Color.White;
            } else if (Input.KeyPressed(Key.Right)) {
                btnUse.Color = Color.White;
                btnInfo.Color = Color.Grey;
            }

            if (Input.KeyPressed(Key.Z)) {
                currentSelect = Select.Use;
            } else if (Input.KeyPressed(Key.X)) {
                btnUse.Visible = false;
                txtUse.Visible = false;
                btnInfo.Visible = false;
                txtInfo.Visible = false;
                currentSelect = Select.Item;
            }
        }

        public void onUse()
        {

        }

        public void onInfo()
        {

        }
    }
}
