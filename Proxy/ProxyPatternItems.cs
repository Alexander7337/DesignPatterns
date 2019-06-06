namespace Proxy
{
    public interface IPerson
    {
        int Age { get; set; }

        string Drink();

        string Drive();

        string DrinkAndDrive();
    }

    public class Person : IPerson
    {
        public int Age { get; set; }
        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class PersonProxy : IPerson
    {
        public Person Person { get; set; }
        private readonly string tooYound = "too young";
        private readonly string dead = "dead";

        public PersonProxy(Person person)
        {
            this.Person = person;
        }

        public int Age
        {
            get
            {
                return this.Person.Age;
            }
            set
            {
                this.Person.Age = value;
            }
        }

        public string Drink()
        {
            if (this.Age < 18)
            {
                return tooYound;
            }

            return Person.Drink();
        }

        public string Drive()
        {
            if (this.Age < 16)
            {
                return tooYound;
            }

            return Person.Drive();
        }

        public string DrinkAndDrive()
        {
            return dead;
        }
    }
}
