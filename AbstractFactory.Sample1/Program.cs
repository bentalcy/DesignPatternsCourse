﻿using System;


namespace AbstractFactory.Sample1 {

    class Program {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main() {
            // Create and run the African animal world
            ContinentFactory africa = new AfricaFactory();
            AnimalWorld world = new AnimalWorld(africa);
            world.RunFoodChain();

            // Create and run the American animal world
            ContinentFactory america = new AmericaFactory();
            world = new AnimalWorld(america);
            world.RunFoodChain();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' abstract class
    /// </summary>
    abstract class ContinentFactory {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class
    /// </summary>
    class AfricaFactory : ContinentFactory {
        public override Herbivore CreateHerbivore() {
            return new Wildebeest();
        }
        public override Carnivore CreateCarnivore() {
            return new Lion();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class
    /// </summary>
    class AmericaFactory : ContinentFactory {
        public override Herbivore CreateHerbivore() {
            return new Bison();
        }
        public override Carnivore CreateCarnivore() {
            return new Wolf();
        }
    }

    /// <summary>
    /// The 'AbstractProductA' abstract class
    /// </summary>
    abstract class Herbivore {
    }

    /// <summary>
    /// The 'AbstractProductB' abstract class
    /// </summary>
    abstract class Carnivore {
        public abstract void Eat(Herbivore h);
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    class Wildebeest : Herbivore {
        public override string ToString() { return nameof(Wildebeest); }
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    class Lion : Carnivore {
        public override void Eat(Herbivore h) {
            Console.WriteLine(this.ToString() + " devours " + h.ToString());
        }
        public override string ToString() { return nameof(Lion); }
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    class Bison : Herbivore {
        public override string ToString() { return nameof(Bison); }
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    class Wolf : Carnivore {
        public override void Eat(Herbivore h) {
            Console.WriteLine(this.ToString() + " eats " + h.ToString());
        }
        public override string ToString() { return nameof(Wolf); }
    }

    /// <summary>
    /// The 'Client' class 
    /// </summary>
    class AnimalWorld {
        private readonly Herbivore _herbivore;
        private readonly Carnivore _carnivore;

        // Constructor
        public AnimalWorld(ContinentFactory factory) {
            _carnivore = factory.CreateCarnivore();
            _herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain() {
            _carnivore.Eat(_herbivore);
        }
    }
}
