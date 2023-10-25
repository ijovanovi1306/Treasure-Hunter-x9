using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public abstract class Character : Sprite
    {
        protected int health;
        protected int speed;
        protected bool weaponR;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public bool WeaponR
        {
            get { return weaponR; }
            set { weaponR = value; }
        }


        public Character(string path, int x, int y) : base(path, x, y)
        {
        }

       
        public override int X
        {
            get { return x; }
            set
            {
                if (value < GameOptions.LeftEdge)
                    x = GameOptions.LeftEdge;
                else if (value > GameOptions.RightEdge - this.Width)
                    x = GameOptions.RightEdge - this.Width;
                else
                    x = value;
            }
        }

        public override int Y
        {
            get { return y; }
            set
            {
                if (value < GameOptions.UpEdge)
                    y = GameOptions.LeftEdge;
                else if (value > GameOptions.DownEdge - this.Height)
                    y= GameOptions.DownEdge - this.Height;
                else
                    y = value;
            }
        }

    }
    public class MainCharacter : Character
    {
        
        private int kills;
        private int treasure;

        public int Kills
        {
            get { return kills; }
            set { kills = value; }
        }
        
        public int Treasure
        {
            get { return treasure; }
            set { treasure = value; }
        }

        public MainCharacter(string path, int x, int y) : base(path, x, y)
        {
            
        }
    }

    public class Caster : MainCharacter
    {
        public Caster(string path, int x, int y) : base(path, x, y)
        {
            this.Health = 100;
            this.Speed = 5;
            this.WeaponR = true;
            this.Kills = 0;
            this.Treasure = 0;
        }
    }
    public class Archer : MainCharacter
    {
        public Archer(string path, int x, int y) : base(path, x, y)
        {
            this.Health = 100;
            this.Speed = 6;
            this.WeaponR = true;
            this.Kills = 0;
            this.Treasure = 0;
        }
    }
    public class Assassin : MainCharacter
    {
        public Assassin(string path, int x, int y) : base(path, x, y)
        {
            this.Health = 100;
            this.Speed = 7;
            this.WeaponR = true;
            this.Kills = 0;
            this.Treasure = 0;
        }
    }

    public class Treasure : Sprite
    {
        public Treasure(string path, int x, int y) : base(path, x, y)
        {
        }
    }

    public class Monster : Character
    {

        public Monster(string path, int x, int y) : base(path, x, y)
        {
        }
        public void MonsterMove(MainCharacter M)
        {
            this.PointToSprite(M);
            this.MoveSteps(this.Speed);
            Game.WaitMS(1);
        }
        public void MonsterSpawn(MainCharacter M)
        {
            this.Health = 25;
            this.SetVisible(true);
            Random r = new Random();
            this.X = r.Next(GameOptions.LeftEdge, GameOptions.RightEdge);
            this.Y = 0;
        }
    }

    public class Skeleton : Monster
    {
        public Skeleton(string path, int x, int y) : base(path, x, y)
        {
            this.Health = 35;
            this.Speed = 4;
        }
    }

    public class Demon : Monster
    {
        public Demon(string path, int x, int y) : base(path, x, y)
        {
            this.Health = 200;
            this.Speed = 4;
        }
    }

    public abstract class Weapon : Sprite
    {
        protected int speed;
        protected int damage;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Weapon(string path, int x, int y) : base(path, x, y)
        {
        }
    }

    public class MagicBall : Weapon
    {
        public MagicBall(string path, int x, int y) : base(path, x, y)
        {
            this.Speed = 10;
            this.Damage = 40;
        }
    }

    public class Dagger : Weapon
    {
        public Dagger(string path, int x, int y) : base(path, x, y)
        {
            this.Speed = 25;
            this.Damage = 25;
        }
    }

    public class Arrow : Weapon
    {
        public Arrow(string path, int x, int y) : base(path, x, y)
        {
            this.Speed = 15;
            this.Damage = 35;
        }
    }








}
