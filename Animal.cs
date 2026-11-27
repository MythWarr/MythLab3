using System;
namespace sharp
{
    public class Animal
    {
        public string name{get;private set;}
        public Animal(string name)
        {
            this.name = name;
        }
        public virtual void MakeSound()
        {
            Console.WriteLine("default sound");
        }
    }
    public class Dog : Animal
    {
        public string name{get;private set;}
        public Dog(string name)
        {
            this.name = name;
        }
        public override void MakeSound()
        {
            Console.WriteLine("Гавв");
        }
    }
    public class Cat : Animal
    {
        public string name{get;private set;}
        public Cat(string name)
        {
            this.name = name;
        }
        public override void MakeSound()
        {
            Console.WriteLine("Мявк");
        }
    }
    public class Bird : Animal
    {
        public string name{get;private set;}
        public Bird(string name)
        {
            this.name = name;
        }
        public override void MakeSound()
        {
            Console.WriteLine("Чик-чирик");
        }
    }
}