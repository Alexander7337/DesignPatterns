namespace Factory
{
    using System;

    #region Interfaces
    public interface IDetails
    {
        int Id { get; set; }
        string Name { get; set; }
    }
    public interface IFactory
    {
        IDetails CreatePerson(string name);
    }
    #endregion

    #region Models
    public class Person : IDetails
    {
        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
    public class Product : IDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    #endregion

    #region Factories
    public class PersonFactory : IFactory
    {
        private static int _id = -1;

        public IDetails CreatePerson(string name)
        {
            return new Person(++_id, name);
        }
    }

    public class Factory
    {
        //list of factories
        public IDetails CreateInstance(string name)
        {
            IDetails details = null;
            foreach (var t in typeof(Factory).Assembly.GetTypes())
            {
                if (typeof(IFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var factory = (IFactory)Activator.CreateInstance(t);
                    details = factory.CreatePerson(name);
                }
            }

            return details;
        }
    }
    #endregion
}
