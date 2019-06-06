namespace DesignPatterns
{
    using Bridge;
    using Builder;
    using ChainOfResponsibilities;
    using Composite;
    using Flyweight;
    using Memento;
    using Prototype;
    using Proxy;
    using System;
    using F = Factory;
    using Use = Mediator;
    using Mediator;

    public class Program
    {
        public static void Main()
        {
            //Builder
            var builder = new CodeBuilder("Person").AddField("string", "Name").AddField("int", "Age");
            Console.WriteLine(builder.ToString());

            //Factory
            var factory = new F.Factory();
            var person = factory.CreateInstance("Alexander");
            // Numbers of models should be as pointers to the Factories if we want to have different models instatiated from one Factory
            Console.WriteLine(person);

            //Prototype
            Point one = new Point { X = 1, Y = 2 };
            Point two = new Point { X = 3, Y = 4 };
            Line line = new Line { Start = one, End = two };
            Line copiedLine = line.DeepCopyJson();
            copiedLine.Start.X = 57;

            Console.WriteLine(line);
            Console.WriteLine(copiedLine);

            //Flyweight
            var sentence = new Sentence("Hasta la vista");
            sentence[0].IsCapitalized = true;
            Console.WriteLine(sentence);

            //Proxy
            var person1 = new PersonProxy(new Person { Age = 21 });
            Console.WriteLine(person1.Drive());
            person1.Age = 14;
            Console.WriteLine(person1.Drink());

            //Bridge
            var circle = new Circle();
            var square = new Square();
            Console.WriteLine(circle.ToString());
            Console.WriteLine(square.ToString());

            //Composite
            var manyValues = new ManyValues
            {
                new SingleValue { Value = 5 },
                new SingleValue { Value = 3 },
                new SingleValue { Value = 2 }
            };
            var some = manyValues.Sum();

            //ChainOfResponsibility
            var game = new Game();
            var goblin = new Goblin(game);
            var goblinKing = new GoblinKing(game);

            //Mediator
            Use.Mediator mediator = new Use.Mediator();
            var p1 = new Participant(mediator);
            var p2 = new Participant(mediator);
            p1.Say(2);
            p2.Say(4);

            //Memento
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);
            // restore to m1
            ba.Restore(m1);
            Console.WriteLine(ba);
            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }

    
}
