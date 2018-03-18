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

            UpdateLayout();
        }
        //DECLARED VARIABLES
        int day = 2;
        int level = 2;
        int energy = 3;
        int maxenergy = 3;
        string location = "forest"; //1-taiga, 2-forest, 3-jungle, 4-desert
        int hp = 20;
        int hunger = 20;
        int armor = 0;
        string sword = "wooden";
        string pickaxe = "wooden";
        int xp = 2;
        int maxxp = 8;

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
                hunger = 0;
                hunger_bar.Value = hunger;
                hunger_label.Content = hunger;
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

                    xp_bar.Maximum = maxxp;
                    maxxp_label.Content = maxxp;

                    xp = 0;

                    int levelstatus = level - 1;
                    status_label.Content = "Level UP! #" + levelstatus;
                }
                if (sword == "wooden")
                {
                    XPADD(2, 5);
                }
                if (sword == "iron")
                {
                    XPADD(4, 7);
                }
                if (sword == "golden")
                {
                    XPADD(6, 9);
                }
                if (sword == "diamond")
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
        List<Item> items = new List<Item>();
        private void Mine_Click(object sender, RoutedEventArgs e)
        {           
            Random rand = new Random();
            int chance_iron = rand.Next(1, 101);
            int chance_gold = rand.Next(1, 101);

            Debug.WriteLine(chance_iron);
            Debug.WriteLine(chance_gold);
            if (energy > 0)
            {
                HungerLoosing(2, 5);
                EnergyLoosing();

                if (pickaxe == "wooden")
                {
                    if (chance_iron <= 100) // probability of 20%
                    {
                        int iron_counter = iron_value++;

                        Item item = new Item
                        {
                            Name = "Iron Bar",
                            Value = iron_counter
                        };
                        
                        items.Add(item);
                        Debug.WriteLine(iron_counter);
                        status_label.Content = "You found Iron Bar.";
                    }
                    if (chance_gold <= 100) // probability of 20%
                    {
                        int gold_counter = gold_value++;

                        Item item = new Item
                        {
                            Name = "Golden Bar",
                            Value = gold_counter
                        };

                        items.Add(item);

                        status_label.Content = "You found Golden Bar.";
                    }
                    else
                    {
                        status_label.Content = "You found nothing.";
                    }
                    if(chance_iron <= 100 && chance_gold <= 100)
                    {
                        status_label.Content = "You found Iron Bar & Golden Bar.";
                    }
                    inventory.ItemsSource = items;
                }
            }
            else
            {
                NoEnergy();
            }
            HungerWarning();
        }
        //private void Mine_Click(object sender, RoutedEventArgs e)
        //{
         //   image_sword.ImageSource = new BitmapImage(new Uri("D:/neupavi15/MinecraftOC/MinecraftOC/MinecraftOC/Pictures/Diamond_Sword.png", UriKind.Relative));
        //}

    }
}