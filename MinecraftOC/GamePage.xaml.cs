using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinecraftOC
{
    /// <summary>
    /// Interakční logika pro GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        ObservableCollection<Item> ItemsCol = new ObservableCollection<Item>();
        public GamePage()
        {
            InitializeComponent();
            energy_label.Content = energy;
            day_label.Content = 1;
            level_label.Content = 1;
            //xp_label.Content = xp;
            armor_label.Content = armor;
            armor_bar.Value = armor;
            energy_bar.Minimum = 0;
            energy_bar.Maximum = maxenergy;
            energy_bar.Value = energy;

            CraftBox.Items.Add("Sword");
            CraftBox.Items.Add("Pickaxe");
            CraftBox.Items.Add("Helmet");
            CraftBox.Items.Add("Chestplate");
            CraftBox.Items.Add("Leggins");
            CraftBox.Items.Add("Boots");

            die_frame.Visibility = Visibility.Hidden;
        }
        //DECLARED VARIABLES
        public int day = 2;
        public int level = 2;
        public int energy = 3;
        public int maxenergy = 3;
        public string location = "forest"; //1-taiga, 2-forest, 3-jungle, 4-desert
        public int hp = 20;
        public int hunger = 20;
        public int armor = 0;
        string sword = "Wood";
        string pickaxe = "Wood";
        public int xp = 2;
        public int maxxp = 8;
        public int house = 1;
        public int quest_completed = 0;
        public void Quest(int quest_number, string quest_goal)
        {
            if(quest_completed == quest_number-2)
            {
                 quest_label.Content = quest_number+ ". QUEST: " + quest_goal;
                quest_completed++;
            }
            Debug.WriteLine(quest_completed +" & "+ quest_number);
            
        }
        //XP ADD v
        public void XPADD(int a, int b)
        {
            Random rnd = new Random();
            int xpADD = rnd.Next(a, b);

            xp = xp + xpADD;
        }
       
        //HP LOOSE v
        public void HPLOOSE(int a, int b)
        {
            Random rnd = new Random();
            int hpLOOSE = rnd.Next(a, b);

            hp = hp - hpLOOSE;

            if (hp < 0)
            {
                hp = 0;
            }

            hp_label.Content = hp;
            hp_bar.Value = hp;
            NoHP();

            if(hp <= 0)
            {
                die_frame.Visibility = Visibility.Visible;
                die_frame.Source = new Uri("GameOver.xaml", UriKind.Relative);
            }
        }
        //WARNING WHEN STARVING v
        public void HungerWarning()
        {
            if (hunger < 8)
            {
                warning_label.Content = "YOU ARE STARVING, EAT SOMETHING!";
            }
        }
        //LOSS OF HUNGER POINTS v
        public void HungerLoosing(int a, int b)
        {
            Random rnd = new Random();
            int random12 = rnd.Next(a, b);

            for (int i = 0; i <= random12; i++)
            {
                hunger_label.Content = hunger--;
            }
            hunger_bar.Value = hunger;
            HungerWarning();
            if (hunger < 0)
            {
                hunger_bar.Value = hunger;
                hunger_label.Content = hunger;
            }
            if (hunger <= 0)
            {
                die_frame.Visibility = Visibility.Visible;
                die_frame.Source = new Uri("GameOver.xaml", UriKind.Relative);
            }
        }
        //LOSS OF ENERGY v
        public void EnergyLoosing()
        {
            energy = energy - 1;
            energy_label.Content = energy;

            energy_bar.Minimum = 0;
            energy_bar.Maximum = maxenergy;
            energy_bar.Value = energy;
        }
        //NO WARNING v
        public void NoWarning()
        {
            warning_label.Content = "";
        }
        //NO ENERGY WARNING v
        public void NoEnergy()
        {
            warning_label.Content = "You don't have energy. Go Sleep.";
        }
        //NO HP WARNING v
        public void NoHP()
        {
            if (hp < 7)
            {
                warning_label.Content = "YOU HAVE LOW HP, HEAL YOURSELF!";
            }
        }
        public void RemoveItem(ObservableCollection<Item> ItemsCol, Item instance, int howMuch)
        {
            for (int b = 0; b < howMuch; b++)
            {
                ItemsCol.Remove(this.ItemsCol.SingleOrDefault(i => i.Name == instance.Name));
            }

        }

        int apple_value = 1;
        int beef_value = 1;
        int wood_value = 1;

        public void ExploreForest()
        {
            int chance_apple = 0;
            int chance_beef = 0;
            int chance_wood = 0;
            int wood_amount = 0;

            Random rand = new Random();
            chance_apple = rand.Next(1, 101);
            chance_beef = rand.Next(1, 101);
            chance_wood = rand.Next(1, 101);
            wood_amount = rand.Next(3, 12);

            HPLOOSE(1, 3);
            EnergyLoosing();

            if (chance_apple <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Apple" }, 1);


                Item apple = new Item
                {
                    Name = "Apple",
                    Value = apple_value,
                    Type = "Food",
                    HungerAddValue = 3
                };

                ItemsCol.Add(apple);
                apple_value++;
                status_label.Content = "You found something.";
            }
            if (chance_beef <= 30) // probability of 30%
            {
                RemoveItem(ItemsCol, new Item { Name = "Beef" }, 1);

                Item beef = new Item
                {
                    Name = "Beef",
                    Value = beef_value,
                    Type = "Food",
                    HungerAddValue = 5
                };

                ItemsCol.Add(beef);
                beef_value++;
                status_label.Content = "You found something.";
            }
            if (chance_wood <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Wood" }, 1);

                for (int i = 0; i < wood_amount; i++)
                {
                    wood_value++;
                }
                Item wood = new Item
                {
                    Name = "Wood",
                    Value = wood_value,
                    Type = "Material",
                    HungerAddValue = 0
                };

                ItemsCol.Add(wood);
                wood_value++;
                status_label.Content = "You found something.";
            }
            if (chance_apple > 60 && chance_wood > 60 && chance_beef > 30)
            {
                status_label.Content = "You found nothing.";
            }

            inventory.ItemsSource = ItemsCol;
        }
        int pumpkin_value = 1;
        int fish_value = 1;
        int cobblestone_value = 1;
        public void ExploreTaiga()
        {
            int chance_pumpkin = 0;
            int chance_fish = 0;
            int chance_cobblestone = 0;
            int cobblestone_amount = 0;

            Random rand = new Random();
            chance_pumpkin = rand.Next(1, 101);
            chance_fish = rand.Next(1, 101);
            chance_cobblestone = rand.Next(1, 101);
            cobblestone_amount = rand.Next(3, 12);

            HPLOOSE(1, 3);
            EnergyLoosing();

            if (chance_pumpkin <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Pumpkin" }, 1);

                Item pumpkin = new Item
                {
                    Name = "Pumpkin",
                    Value = pumpkin_value,
                    Type = "Food",
                    HungerAddValue = 3
                };

                ItemsCol.Add(pumpkin);
                pumpkin_value++;
                status_label.Content = "You found something.";
            }
            if (chance_fish <= 30) // probability of 30%
            {
                RemoveItem(ItemsCol, new Item { Name = "Fish" }, 1);

                Item fish = new Item
                {
                    Name = "Fish",
                    Value = fish_value,
                    Type = "Food",
                    HungerAddValue = 5
                };

                ItemsCol.Add(fish);
                fish_value++;
                status_label.Content = "You found something.";
            }
            if (chance_cobblestone <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Cobblestone" }, 1);

                for (int i = 0; i < cobblestone_amount; i++)
                {
                    cobblestone_value++;
                }
                Item cobblestone = new Item
                {
                    Name = "Cobblestone",
                    Value = cobblestone_value,
                    Type = "Material",
                    HungerAddValue = 0
                };

                ItemsCol.Add(cobblestone);
                cobblestone_value++;
                status_label.Content = "You found something.";
            }
            if (chance_cobblestone > 60 && chance_pumpkin > 60 && chance_fish > 30)
            {
                status_label.Content = "You found nothing.";
            }

            inventory.ItemsSource = ItemsCol;
        }
        int melon_value = 1;
        int porkchop_value = 1;
        int clay_value = 1;
        public void ExploreJungle()
        {
            int chance_melon = 0;
            int chance_porkchop = 0;
            int chance_clay = 0;
            int clay_amount = 0;

            Random rand = new Random();
            chance_melon = rand.Next(1, 101);
            chance_porkchop = rand.Next(1, 101);
            chance_clay = rand.Next(1, 101);
            clay_amount = rand.Next(3, 12);

            HPLOOSE(1, 3);
            EnergyLoosing();

            if (chance_melon <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Melon" }, 1);

                Item melon = new Item
                {
                    Name = "Melon",
                    Value = melon_value,
                    Type = "Food",
                    HungerAddValue = 3
                };

                ItemsCol.Add(melon);
                melon_value++;
                status_label.Content = "You found something.";
                Quest(3, "Find Iron Bar");
            }
            if (chance_porkchop <= 30) // probability of 30%
            {
                RemoveItem(ItemsCol, new Item { Name = "Pork Chop" }, 1);

                Item chicken = new Item
                {
                    Name = "Pork Chop",
                    Value = porkchop_value,
                    Type = "Food",
                    HungerAddValue = 5
                };

                ItemsCol.Add(chicken);
                porkchop_value++;
                status_label.Content = "You found something.";
            }
            if (chance_clay <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Clay" }, 1);

                for (int i = 0; i < clay_amount; i++)
                {
                    clay_value++;
                }
                Item clay = new Item
                {
                    Name = "Clay",
                    Value = clay_value,
                    Type = "Material",
                    HungerAddValue = 0
                };

                ItemsCol.Add(clay);
                clay_value++;
                status_label.Content = "You found something.";
            }
            if (chance_clay > 60 && chance_melon > 60 && chance_porkchop > 30)
            {
                status_label.Content = "You found nothing.";
            }

            inventory.ItemsSource = ItemsCol;
        }
        int potato_value = 1;
        int chicken_value = 1;
        int sand_value = 1;
        public void ExploreDesert()
        {
            int chance_potato = 0;
            int chance_chicken = 0;
            int chance_sand = 0;
            int sand_amount = 0;

            Random rand = new Random();
            chance_potato = rand.Next(1, 101);
            chance_chicken = rand.Next(1, 101);
            chance_sand = rand.Next(1, 101);
            sand_amount = rand.Next(3, 12);

            HPLOOSE(1, 3);
            EnergyLoosing();

            if (chance_potato <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Potato" }, 1);

                Item potato = new Item
                {
                    Name = "Potato",
                    Value = potato_value,
                    Type = "Food",
                    HungerAddValue = 3
                };

                ItemsCol.Add(potato);
                potato_value++;
                status_label.Content = "You found something.";
            }
            if (chance_chicken <= 30) // probability of 30%
            {
                RemoveItem(ItemsCol, new Item { Name = "Chicken" }, 1);

                Item chicken = new Item
                {
                    Name = "Chicken",
                    Value = chicken_value,
                    Type = "Food",
                    HungerAddValue = 5
                };

                ItemsCol.Add(chicken);
                chicken_value++;
                status_label.Content = "You found something.";
            }
            if (chance_sand <= 60) // probability of 60%
            {
                RemoveItem(ItemsCol, new Item { Name = "Sand" }, 1);

                for (int i = 0; i < sand_amount; i++)
                {
                    sand_value++;
                }
                Item sand = new Item
                {
                    Name = "Sand",
                    Value = sand_value,
                    Type = "Material",
                    HungerAddValue = 0
                };

                ItemsCol.Add(sand);
                sand_value++;
                status_label.Content = "You found something.";
            }
            else
            {
                status_label.Content = "You found nothing.";
            }

            inventory.ItemsSource = ItemsCol;
        }
        public void Mining(int i, int g, int d)
        {
            int chance_iron = 0;
            int chance_gold = 0;
            int chance_diamond = 0;
            if (energy > 0)
            {
                Random rand = new Random();
                chance_iron = rand.Next(1, 101);
                Random rand1 = new Random();
                chance_gold = rand1.Next(1, 101);
                Random rand2 = new Random();
                chance_diamond = rand2.Next(1, 101);

                HungerLoosing(2, 5);
                EnergyLoosing();
                Debug.WriteLine(chance_iron);

                if (chance_iron <= i)
                {
                    RemoveItem(ItemsCol, new Item { Name = "Iron Bar" }, 1);
                    Item iron = new Item
                    {
                        Name = "Iron Bar",
                        Value = iron_value,
                        Type = "Ore",
                        CraftingMaterial = "Iron",
                        HungerAddValue = 0
                    };
                    ItemsCol.Add(iron);
                    iron_value++;
                    status_label.Content = "You found something.";
                    Quest(4, "Kill Slime");
                }
                else
                {
                    status_label.Content = "You found nothing.";
                }
                if (chance_gold <= g)
                {
                    RemoveItem(ItemsCol, new Item { Name = "Golden Bar" }, 1);
                    Item gold = new Item
                    {
                        Name = "Golden Bar",
                        Value = gold_value,
                        Type = "Ore",
                        CraftingMaterial = "Gold",
                        HungerAddValue = 0
                    };
                    ItemsCol.Add(gold);
                    gold_value++;
                    status_label.Content = "You found something.";
                }
                if (chance_diamond <= d)
                {
                    RemoveItem(ItemsCol, new Item { Name = "Diamond" }, 1);
                    Item diamond = new Item
                    {
                        Name = "Diamond",
                        Value = diamond_value,
                        Type = "Ore",
                        CraftingMaterial = "Diamond",
                        HungerAddValue = 0
                    };

                    ItemsCol.Add(diamond);
                    diamond_value++;
                    status_label.Content = "You found something.";
                }

                inventory.ItemsSource = ItemsCol;
            }

            else
            {
                NoEnergy();
            }
            HungerWarning();
        }

        //BUTTONY////////////////////////////////////////////////////////////////////////////
        //SLEEP/////////////////////////////////////////////////
        public void Sleep_Click(object sender, RoutedEventArgs e)
        {
            HungerLoosing(2, 4);

            status_label.Content = "You went sleep. Day #" + day;

            day_label.Content = day++;
            energy_label.Content = maxenergy;
            energy = maxenergy;

            Random rnd = new Random();
            int hpADD = rnd.Next(3, 5);

            for (int i = 0; i < hpADD; i++)
            {
                hp_label.Content = hp++;
                hp_bar.Value = hp++;
                if (hp > 20)
                {
                    hp = 20;
                }

            }
            warning_label.Content = "";
            HungerWarning();

            energy_bar.Minimum = 0;
            energy_bar.Maximum = maxenergy;
            energy_bar.Value = energy;
        }
        //TRAVELLING////////////////////////////////////////////
        private void Taiga_Click(object sender, RoutedEventArgs e)
        {
            if (location != "taiga")
            {
                if (energy > 0)
                {
                    location = "taiga";
                    location_label.Content = "TAIGA";

                    EnergyLoosing();
                    HungerLoosing(0, 2);

                    status_label.Content = "You went to Taiga.";
                    NoWarning();
                    HungerWarning();
                }
                else
                {
                    NoEnergy();
                }
            }
            else
            {
                warning_label.Content = "You are in taiga already. Go somewhere else!";
            }

        }
        private void Forest_Click(object sender, RoutedEventArgs e)
        {
            if (location != "forest")
            {
                if (energy > 0)
                {
                    location = "forest";
                    location_label.Content = "FOREST";

                    EnergyLoosing();
                    HungerLoosing(0, 2);

                    status_label.Content = "You went to Forest.";
                    NoWarning();
                    HungerWarning();
                }
                else
                {
                    NoEnergy();
                }
            }
            else
            {
                warning_label.Content = "You are in forest already. Go somewhere else!";
            }
        }
        private void Jungle_Click(object sender, RoutedEventArgs e)
        {
            Quest(2,"Find Melon");
            if (location != "jungle")
            {
                if (energy > 0)
                {
                    location = "jungle";
                    location_label.Content = "JUNGLE";

                    EnergyLoosing();
                    HungerLoosing(0, 2);

                    status_label.Content = "You went to Jungle.";
                    NoWarning();
                    HungerWarning();
                }
                else
                {
                    NoEnergy();
                }
            }
            else
            {
                warning_label.Content = "You are in jungle already. Go somewhere else!";
            }
        }
        private void Desert_Click(object sender, RoutedEventArgs e)
        {
            if (location != "desert")
            {
                if (energy > 0)
                {
                    location = "desert";
                    location_label.Content = "DESERT";

                    EnergyLoosing();
                    HungerLoosing(0, 2);
                    status_label.Content = "You went to Desert.";
                    NoWarning();
                    HungerWarning();
                }
                else
                {
                    NoEnergy();
                }
            }
            else
            {
                warning_label.Content = "You are in desert already. Go somewhere else!";
            }
        }
        //FIGHT////////////////////////////////////////////////
        private void Fight_Click(object sender, RoutedEventArgs e)
        {
            if (energy > 0)
            {
                xp_label.Content = xp;//pocet xp
                xp_bar.Value = xp;//pocet xp v baru
                maxxp_label.Content = maxxp;//xp potrebne do dalsiho levelu

                if (xp > maxxp - 1)
                {
                    xp = 0;
                    xp_label.Content = xp;
                    level_label.Content = level++;
                    maxxp = maxxp + maxxp / 2;
                    xp_bar.Value = 0;

                    if(level >= 10)
                    {
                        Quest(13, "Get Diamond Helmet");
                    }

                    xp_bar.Maximum = maxxp;
                    maxxp_label.Content = maxxp;

                    xp = 0;

                    int levelstatus = level - 1;
                    status_label.Content = "Level UP! #" + levelstatus;
                }
                if (sword == "Wood")
                {
                    XPADD(2, 5);
                }
                if (sword == "Iron")
                {
                    XPADD(4, 7);
                }
                if (sword == "Gold")
                {
                    XPADD(6, 9);
                }
                if (sword == "Diamond")
                {
                    XPADD(8, 11);
                }

                if (armor >= 0 && armor <= 5)
                {
                    HPLOOSE(8, 10);
                }
                if (armor >= 6 && armor <= 10)
                {
                    HPLOOSE(6, 9);
                }
                if (armor >= 11 && armor <= 15)
                {
                    HPLOOSE(4, 7);
                }
                if (armor >= 16 && armor <= 20)
                {
                    HPLOOSE(2, 5);
                }

                if (location == "taiga")
                {
                    var skeletons = 0;
                    var zombies = 0;

                    Random rnd1 = new Random();
                    int mob1 = rnd1.Next(1, 4);
                    int mob2 = rnd1.Next(0, 3);

                    for (int x = 0; x < mob1; x++)
                    {
                        skeletons++;
                    }

                    for (int y = 0; y < mob2; y++)
                    {
                        zombies++;
                    }
                    status_label.Content = "You killed " + skeletons + "x skeleton and " + zombies + "x zombie.";
                }
                if (location == "forest")
                {
                    var zombies = 0;
                    var spiders = 0;

                    Random rnd1 = new Random();
                    int mob1 = rnd1.Next(1, 4);
                    int mob2 = rnd1.Next(0, 3);

                    for (int x = 0; x < mob1; x++)
                    {
                        zombies++;
                    }

                    for (int y = 0; y < mob2; y++)
                    {
                        spiders++;
                    }
                    status_label.Content = "You killed " + zombies + "x zombie and " + spiders + "x spider.";
                }
                if (location == "jungle")
                {
                    var creepers = 0;
                    var skeletons = 0;

                    Random rnd1 = new Random();
                    int mob1 = rnd1.Next(1, 4);
                    int mob2 = rnd1.Next(0, 3);

                    for (int x = 0; x < mob1; x++)
                    {
                        creepers++;
                    }

                    for (int y = 0; y < mob2; y++)
                    {
                        skeletons++;
                    }

                    status_label.Content = "You killed " + creepers + "x creeper and " + skeletons + "x skeleton.";
                }
                if (location == "desert")
                {
                    var spiders = 0;
                    var slimes = 0;

                    Random rnd1 = new Random();
                    int mob1 = rnd1.Next(1, 3);
                    int mob2 = rnd1.Next(0, 2);

                    for (int x = 0; x < mob1; x++)
                    {
                        spiders++;
                    }

                    for (int y = 0; y < mob2; y++)
                    {
                        slimes++;
                    }

                    status_label.Content = "You killed " + spiders + "x spider and " + slimes + "x slime.";
                    if(slimes > 0)
                    {
                        Quest(5, "Craft Iron Pickaxe");
                    }
                }
                EnergyLoosing();
            }
            else
            {
                NoEnergy();
            }
        }

        int iron_value = 1;
        int gold_value = 1;
        int diamond_value = 1;
        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            if (pickaxe == "Wood")
            {
                Mining(35,10,5);//25 5 2
            }
            if (pickaxe == "Iron")
            {
                Mining(50, 20,10);
            }
            if (pickaxe == "Gold")
            {
                Mining(75, 40, 30);
            }
            if (pickaxe == "Diamond")
            {
                Mining(90, 60, 50);
            }
        }

        private void Explore_Click(object sender, RoutedEventArgs e)
        {
            if (energy > 0)
            {
                if (location == "forest")
                {
                    ExploreForest();
                }
                if (location == "taiga")
                {
                    ExploreTaiga();
                }
                if (location == "desert")
                {
                    ExploreDesert();
                }
                if (location == "jungle")
                {
                    ExploreJungle();
                }
            }
            else
            {
                NoEnergy();
            }
            HungerWarning();
        }
        private void Eat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selItem = (Item)inventory.SelectedItem;

                if (selItem.HungerAddValue + hunger <= 20)
                {
                    if (selItem.Type == "Food")
                    {
                        ///
                        RemoveItem(ItemsCol, new Item { Name = selItem.Name }, 1);
                        RemoveItem(ItemsCol, new Item { Value = selItem.Value }, 1);

                        int hValue = selItem.HungerAddValue;

                        var new_value = selItem.Value - 1;

                        if (selItem.Value > 1)
                        {
                            Item newitem = new Item
                            {
                                Name = selItem.Name,
                                Value = new_value,
                                Type = "Food",
                                HungerAddValue = hValue
                            };
                            ItemsCol.Add(newitem);
                        }
                        ///
                        warning_label.Content = "";
                    }
                    else
                    {
                        warning_label.Content = "You can't eat material!";
                    }
                    hunger = hunger + selItem.HungerAddValue;
                    hunger_label.Content = hunger;
                    hunger_bar.Value = hunger;
                }
                else
                {
                    warning_label.Content = "You have full Hunger Bar!";
                }
                HungerWarning();
            }
            catch (Exception)
            {
                warning_label.Content = "Select Food!";
            }
        }
        public void UpdateItem(string itemName, int itemvalue, int minus, string itemtype)
        {
            RemoveItem(ItemsCol, new Item { Name = itemName }, 1);

            Item item = new Item
            {
                Name = itemName,
                Value = itemvalue - minus,
                Type = itemtype,
                HungerAddValue = 0
            };
            ItemsCol.Add(item);
        }
        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            if (house == 1)
            {
                if (ItemsCol.Any(a => a.Name == "Wood" && a.Value >= 64))
                {
                    if (ItemsCol.Any(b => b.Name == "Cobblestone" && b.Value >= 64))
                    {
                        if (ItemsCol.Any(c => c.Name == "Sand" && c.Value >= 16))
                        {
                            house_image.Source = new BitmapImage(new Uri("/Pictures/house2.png", UriKind.Relative));

                            UpdateItem("Wood", wood_value, 65, "Material");
                            UpdateItem("Cobblestone", cobblestone_value, 65, "Material");
                            UpdateItem("Sand", wood_value, 17, "Material");

                            maxenergy = 4;
                            maxenergy_label.Content = 4;
                            energy_bar.Maximum = maxenergy;
                            house = 2;

                            Quest(8, "Craft Diamond Sword");
                        }
                    }
                }
                else
                {
                    warning_label.Content = "You don't have all resources.";
                }
            }
            if (house == 2)
            {
                if (ItemsCol.Any(a => a.Name == "Wood" && a.Value >= 128))
                {
                    if (ItemsCol.Any(b => b.Name == "Clay" && b.Value >= 256))
                    {
                        if (ItemsCol.Any(c => c.Name == "Sand" && c.Value >= 64))
                        {
                            house_image.Source = new BitmapImage(new Uri("/Pictures/house3.png", UriKind.Relative));
                            house = 3;

                            UpdateItem("Wood", wood_value, 129, "Material");
                            UpdateItem("Clay", cobblestone_value, 257, "Material");
                            UpdateItem("Sand", wood_value, 65, "Material");

                            maxenergy = 5;
                            maxenergy_label.Content = 5;
                            energy_bar.Maximum = maxenergy;
                        }
                        else
                        {
                            warning_label.Content = "You don't have all resources.";
                        }
                    }
                    else
                    {
                        warning_label.Content = "You don't have all resources.";
                    }
                }
                else
                {
                    warning_label.Content = "You don't have all resources.";
                }
            }

            if (house == 1)
            {
                EnergyButton.ToolTip = "You need 64x Wood, 64x Cobblestone & 16x Sand";
            }
            if (house == 2)
            {
                EnergyButton.ToolTip = "You need 128x Wood, 256x Clay & 64x Sand";
            }
            if (house == 3)
            {
                EnergyButton.Visibility = Visibility.Hidden;
                EnergyButton.ToolTip = "MAX";
            }
            inventory.ItemsSource = ItemsCol;
        }
        int helmet_armor = 0;
        int chestplate_armor = 0;
        int leggins_armor = 0;
        int boots_armor = 0;
        public void CraftMaterial(string material)
        {
            var selItem = (Item)inventory.SelectedItem;

            try
            {
                if (selItem.Type == "Ore")
                {
                    if (selItem.CraftingMaterial == material)
                    {
                        string selected = CraftBox.Text;

                        if (selected == "Sword")
                        {
                            if (selItem.Value >= 2)
                            {
                                string path = "/Pictures/" + material + "_Sword.png";
                                image_sword.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                sword = material;
                                UpdateItem(selItem.Name, selItem.Value, 2, "Ore");
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                            if(sword == "Gold")
                            {
                                Quest(7, "Upgrade your house");
                            }
                            if(sword == "Diamond")
                            {
                                Quest(9, "Craft Diamond Pickaxe");
                            }
                            status_label.Content = "You crafted "+material+" Sword";
                        }
                        if (selected == "Pickaxe")
                        {
                            if (selItem.Value >= 3)
                            {
                                string path = "/Pictures/" + material + "_Pickaxe.png";
                                image_pickaxe.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                pickaxe = material;
                                UpdateItem(selItem.Name, selItem.Value, 3, "Ore");
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                            if(pickaxe == "Iron")
                            {
                                Quest(6, "Craft Golden Sword");
                            }
                            if(pickaxe == "Diamond")
                            {
                                Quest(10, "Craft Diamond Leggins");
                            }
                            status_label.Content = "You crafted " + material + " Pickaxe";
                        }
                        if (selected == "Helmet")
                        {
                            if (selItem.Value >= 5)
                            {
                                string path = "/Pictures/" + material + "_Helmet.png";
                                image_helmet.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                image_helmet_holder.Source = new BitmapImage(new Uri("/Pictures/item_holder.png", UriKind.Relative));
                                UpdateItem(selItem.Name, selItem.Value, 5, "Ore");
                                string helmet = material + "_helmet";
                                if (helmet == "Iron_helmet")
                                {
                                    helmet_armor = 2;
                                }
                                if (helmet == "Gold_helmet")
                                {
                                    helmet_armor = 3;
                                }
                                if (helmet == "Diamond_helmet")
                                {
                                    helmet_armor = 5;
                                    Quest(14, "Craft Diamond Boots");
                                }
                                status_label.Content = "You crafted " + material + " Helmet";
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                        }
                        if (selected == "Chestplate")
                        {
                            if (selItem.Value >= 8)
                            {
                                string path = "/Pictures/" + material + "_Chestplate.png";
                                image_chestplate.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                image_chestplate_holder.Source = new BitmapImage(new Uri("/Pictures/item_holder.png", UriKind.Relative));
                                UpdateItem(selItem.Name, selItem.Value, 8, "Ore");
                                string chestplate = material + "_chestplate";
                                if (chestplate == "Iron_chestplate")
                                {
                                    chestplate_armor = 3;
                                }
                                if (chestplate == "Gold_chestplate")
                                {
                                    chestplate_armor = 5;
                                }
                                if (chestplate == "Diamond_chestplate")
                                {
                                    chestplate_armor = 7;
                                }
                                status_label.Content = "You crafted " + material + " Chestplate";
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                        }
                        if (selected == "Leggins")
                        {
                            if (selItem.Value >= 7)
                            {
                                string path = "/Pictures/" + material + "_Leggins.png";
                                image_leggins.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                image_leggins_holder.Source = new BitmapImage(new Uri("/Pictures/item_holder.png", UriKind.Relative));
                                UpdateItem(selItem.Name, selItem.Value, 7, "Ore");
                                string leggins = material + "_leggins";
                                if (leggins == "Iron_leggins")
                                {
                                    leggins_armor = 2;
                                }
                                if (leggins == "Gold_leggins")
                                {
                                    leggins_armor = 3;
                                }
                                if (leggins == "Diamond_leggins")
                                {
                                    leggins_armor = 5;
                                    Quest(11, "Get at least 12 Armor");
                                }
                                status_label.Content = "You crafted " + material + " Leggins";
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                        }
                        if (selected == "Boots")
                        {
                            if (selItem.Value >= 4)
                            {
                                string path = "/Pictures/" + material + "_Boots.png";
                                image_boots.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                                image_boots_holder.Source = new BitmapImage(new Uri("/Pictures/item_holder.png", UriKind.Relative));
                                UpdateItem(selItem.Name, selItem.Value, 4, "Ore");
                                string boots = material + "_boots";
                                if (boots == "Iron_boots")
                                {
                                    boots_armor = 1;
                                }
                                if (boots == "Gold_boots")
                                {
                                    boots_armor = 2;
                                }
                                if (boots == "Diamond_boots")
                                {
                                    boots_armor = 3;
                                    Quest(15, "Get full diamond armor");
                                }
                                status_label.Content = "You crafted " + material + " Boots";
                            }
                            else
                            {
                                warning_label.Content = "You don't have enough material!";
                            }
                        }
                    }
                }
                else
                {
                    warning_label.Content = "Select Iron Bar, Golden Bar or Diamond to craft something!";
                }
            }
            catch (Exception)
            {
                warning_label.Content = "Select Iron Bar, Golden Bar or Diamond to craft something!";
            }
            armor = helmet_armor + chestplate_armor + leggins_armor + boots_armor;
            armor_label.Content = armor;
            armor_bar.Value = armor;
            if(armor >= 12)
            {
                Quest(12, "Get level 10");
            }
            if(armor == 20)
            {
                quest_label.Content = "CG! You completed all quests in " + day + " days";
            }
        }
        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selItem = (Item)inventory.SelectedItem;
                string craftingMaterial = selItem.CraftingMaterial;
                CraftMaterial(craftingMaterial);
            }
            catch (Exception)
            {
                warning_label.Content = "Select Iron Bar, Golden Bar or Diamond to craft something!";
            }
        }      
    }
}