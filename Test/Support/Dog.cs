using System.Collections.Generic;

namespace Test.Support
{
    internal sealed class Dog
    {
        public Dog(string name)
        {
            Name = name;
            Friends = new HashSet<Dog>();
        }

        public string Name { get; }
        public ISet<Dog> Friends { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var dog = obj as Dog;
            return dog != null && Name == dog.Name && Friends.SetEquals(dog.Friends);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}