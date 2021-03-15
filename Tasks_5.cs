using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {   
            // 1.
            Console.WriteLine(AlgOperation.Sum(1.2f, 2, 4, 6, 7, 8));
            Console.WriteLine(AlgOperation.Sum(1, 2, 4, 6, 7, 8));
            Console.WriteLine(AlgOperation.Div(512342,8989));
            Console.WriteLine(AlgOperation.Mul(512342, 8989));
            Console.WriteLine(AlgOperation.Sub(512342, 1, 2, 4, 6, 7, 8));

            //2.
            Hat russianCowboyItem = new Hat("Hat with ear flaps", 10);
            Boots fJokerItem = new Boots("Skiing", 50);

            IEquipped decript = russianCowboyItem;
            decript.ShowDescription();
            decript = fJokerItem;
            decript.ShowDescription();

            //3.

            Random random = new Random(Environment.TickCount);
            Armor skelArmor = new Armor(random.Next(0,10));
            Skeleton skel = new Skeleton(random.Next(0,500), random.Next(0, 20), skelArmor);
            Wolf woofer = new Wolf(random.Next(0, 500), random.Next(0, 15), random.Next(0, 150));

            FightConstruct(ref skel, ref woofer);
            
            Console.Read();

        }

        static void FightConstruct (ref Skeleton x, ref Wolf y)
        {

            string nameX = x.GetType().ToString();
            string nameY = y.GetType().ToString();
            Typing(ref nameX);
            Typing(ref nameY);

            Console.WriteLine($"{nameX} have {x.Hp} HP");
            Console.WriteLine($"{nameY} have {y.Hp} HP");

            int damageX;
            int damageY;


            if (x.Hp < y.Hp)
            {
                for(int rounder = 1; x.Hp > 0 && y.Hp > 0 && rounder<43; rounder++)
                {
                    Console.WriteLine($"\n---------- Round {rounder} ----------\n");

                    damageY = y.GetDamage(x.Hit());
                    damageX = x.GetDamage(y.Hit());

                    Log(nameY, damageY, y.Hit(), y.Hp);

                    Log(nameX, damageX, x.Hit(), x.Hp);

                }
            }
            else
            {
                for (int rounder = 1; x.Hp > 0 && y.Hp > 0 && rounder < 43; rounder++)
                {

                    Console.WriteLine($"\n---------- Round {rounder} ----------\n");

                    damageX = x.GetDamage(y.Hit());
                    damageY = y.GetDamage(x.Hit());

                    Log(nameX, damageX, x.Hit(), x.Hp);
                    Log(nameY, damageY, y.Hit(), y.Hp);

                }
            }

            if (y.Hp < 0)
                Console.WriteLine($"\n{nameX} win this battle!");
            else if(x.Hp < 0)
                Console.WriteLine($"\n{nameY} win this battle!");
            else
                Console.WriteLine("Draw.....");
        }

        static string Typing(ref string type)
        {

            type = type.Remove(0, type.IndexOf('.') + 1);

            return type;
        }

        static void Log (string name, int damage, int hit, int hp)
        {
            if(hp <= 0)
            {
                Console.WriteLine($"{name}: Gets {damage} damage, and hits on {hit}, and now is dead"); 
            }
            else
            {
                Console.WriteLine($"{name}: Gets {damage} damage, and hits on {hit}, and now has a {hp} HP");
            }
            
        }


    }

    class AlgOperation
    {
        /************ Сумма элементов ************/

        static public float Sum(params float[] source)
        {
            float sum = 0;

            foreach(var i in source)
            {
                sum += i;
            }

            return sum;
        }

        static public int Sum(params int[] source)
        {
            int sum = 0;

            foreach (var i in source)
            {
                sum += i;
            }
            return sum;
        }

        /************ Деление ************/
        static public float Div(float x, float y)
            => (float)(x / y);

        static public float Div(int x, int y)
        {
            return Div((float)x, (float)y);
        }

        /************ Умножение ************/
        static public double Mul(float x, float y)
            => (x * y);

        static public long Mul(int x, int y)
        {
            return (long) Mul((float)x, (float)y);
        }

        /************ Вычитание элементов из первого аргумента ************/
        static public float Sub(float sub, params float[] source)
        {
            return sub - Sum(source);
        }
        
        static public int Sub(int sub, params int[] source)
        {
            return sub - Sum(source);
        }
    }

    /******* 2 *******/
    enum EquipType
    {
        boots, 
        pants, 
        jacket, 
        hat
    }

    interface IEquipped
    {
        EquipType Type
        {
            get;
            set;
        }
        void ShowDescription();
    }


    class Hat : IEquipped
    {
        string name;

        uint pointOfDef;

        public EquipType Type { 

            get => EquipType.hat;

            set => Type = EquipType.hat; 
        }

        public Hat(string name, uint point)
        {
            this.name = name;
            this.pointOfDef = point;
        }

        public void ShowDescription()
        {
            Console.WriteLine($"\nThe hat protects you from rain, sun and what you don't want to see.\n" +
                $"Type of equip: {Type}\n" +
                $"Title: {this.name}\n" +
                $"Armor point: {this.pointOfDef}");
        }
    }

    class Boots : IEquipped
    {

        string name;

        uint pointOfDef;

        public EquipType Type { get => EquipType.boots; set => Type = EquipType.boots; }

        public Boots(string name, uint point)
        {
            this.name = name;
            this.pointOfDef = point;
        }

        public void ShowDescription()
        {
            Console.WriteLine($"\nBoots are needed for walking in the wasteland\n" +
                $"Type of equip: {Type}\n" +
                $"Title: {this.name}\n" +
                $"Armor point: {this.pointOfDef}");
        }
    }

    /******* 3 *******/
    abstract class Monster
    {
        abstract public int Hp
        {
            get;
            set;
        }

        abstract protected int DamageValue
        {
            get;
            set;
        }

        abstract public int GetDamage(int toMonster);

        abstract public int Hit();
    }

    struct Armor
    {
        public int procDamage;

        public Armor(int x)
        {
            this.procDamage = x;
        }


    }

    class Skeleton : Monster
    {
        private int hp;
        private int damageValue;

        public override int Hp {


            get => hp;

            set 
            {
                if (value <= 0)
                {
                    value = 1;
                }

                hp = value;

            }
        }

        protected override int DamageValue { 

            get => damageValue;

            set
            {
                if(value < 0)
                {
                    value = 0;
                }

                damageValue = value;
            } 
        }

        private Armor armor;

        public Skeleton(int hitPoint, int damage, Armor armor)
        {
            this.Hp = hitPoint;
            this.DamageValue = damage;
            this.armor = armor;
        }

        public override int GetDamage(int toMonster)
        {
            int fullDamage = (toMonster - this.armor.procDamage);

            if (fullDamage < 0)
                fullDamage = 0;

            this.hp -= fullDamage;

            return fullDamage;
        }

        public override int Hit()
            => damageValue;
    }

    class Wolf : Monster
    {

        private int hp;
        private int damageValue;
        private int canines;

        public override int Hp {

            get => hp;

            set 
            {
                if(value <= 0)
                {
                    hp = 1;
                }

                hp = value;
            }
        }

        protected override int DamageValue
        {
            get => damageValue;

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                damageValue = value;

            }

        }

        private int Canines
        {
            set
            {
                if(value > 100)
                {
                    value = 100;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                canines = value;
            }
        }

        public Wolf(int hp, int damage, int canies)
        {
            this.Hp = hp;
            this.DamageValue = damage;
            this.Canines = canies;
        }

        public override int GetDamage(int toMonster)
        {
            this.hp -= toMonster;

            return toMonster;
        }

        public override int Hit()
            => damageValue + (canines / 10);

    }
}
