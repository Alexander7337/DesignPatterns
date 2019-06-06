namespace ChainOfResponsibilities
{
    using System;
    using System.Collections.Generic;

    public enum Statistic
    {
        Attack,
        Defense
    }

    public class StatQuery
    {
        public Statistic Statistic;
        public int Result;
    }

    public abstract class Creature
    {
        public Game Game { get; set; }
        public virtual int Attack { get; set; } = 1;
        public virtual int Defense { get; set; } = 1;
        public abstract void Query(object source, StatQuery q);

        public Creature(Game game)
        {
            this.Game = game;
        }
    }

    public class Goblin : Creature
    {
        public Goblin(Game game) : base(game)
        {
            this.Game.Creatures.Add(this);
        }

        public override void Query(object source, StatQuery sq)
        {
            if (ReferenceEquals(source, this))
            {
                switch (sq.Statistic)
                {
                    case Statistic.Attack:
                        sq.Result += this.Attack;
                        break;
                    case Statistic.Defense:
                        sq.Result += this.Defense;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                if (sq.Statistic == Statistic.Defense)
                {
                    sq.Result++;
                }
            }
        }

        public override int Defense
        {
            get
            {
                var q = new StatQuery { Statistic = Statistic.Defense };
                foreach (var c in this.Game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public override int Attack
        {
            get
            {
                var q = new StatQuery { Statistic = Statistic.Attack };
                foreach (var c in this.Game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }
    }

    public class GoblinKing : Creature
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack = 3;
            Defense = 3;
            this.Game.Creatures.Add(this);
        }

        public override void Query(object source, StatQuery sq)
        {
            if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack)
            {
                sq.Result++;
            }
        }
    }

    public class Game
    {
        public List<Creature> Creatures { get; set; }

        public Game()
        {
            this.Creatures = new List<Creature>();
        }
    }
}
