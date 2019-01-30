﻿using System;
using System.Collections.Generic;

namespace AbstractFactory.Sample2 {
    class Program {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        public static void Main() {
            // Create and run the African animal world
            var africa = new AnimalWorld<Africa>();
            africa.RunFoodChain();

            // Create and run the American animal world
            var america = new AnimalWorld<America>();
            america.RunFoodChain();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' interface. 
    /// </summary>
    interface IContinentFactory {
        IHerbivore CreateHerbivore();
        ICarnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class.
    /// </summary>
    class Africa : IContinentFactory {
        public IHerbivore CreateHerbivore() {
            return new Wildebeest();
        }

        public ICarnivore CreateCarnivore() {
            return new Lion();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class.
    /// </summary>
    class America : IContinentFactory {
        public IHerbivore CreateHerbivore() {
            return new Bison();
        }

        public ICarnivore CreateCarnivore() {
            return new Wolf();
        }
    }

    /// <summary>
    /// The 'AbstractProductA' interface
    /// </summary>
    interface IHerbivore {
    }

    /// <summary>
    /// The 'AbstractProductB' interface
    /// </summary>
    interface ICarnivore {
        void Eat(IHerbivore h);
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    class Wildebeest : IHerbivore {
        public override string ToString() { return nameof(Wildebeest); }
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    class Lion : ICarnivore {
        public void Eat(IHerbivore h) {
            Console.WriteLine(this.ToString() + " devours " + h.ToString());
        }
        public override string ToString() { return nameof(Lion); }
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    class Bison : IHerbivore {
        public override string ToString() { return nameof(Bison); }
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    class Wolf : ICarnivore {
        public void Eat(IHerbivore h) {
            Console.WriteLine(this.ToString() + " eats " + h.ToString());
        }
        public override string ToString() { return nameof(Wolf); }
    }

    /// <summary>
    /// The 'Client' interface
    /// </summary>
    interface IAnimalWorld {
        void RunFoodChain();
    }

    /// <summary>
    /// The 'Client' class
    /// </summary>
    class AnimalWorld<T> : IAnimalWorld where T : IContinentFactory, new() {
        private readonly IHerbivore _herbivore;
        private readonly ICarnivore _carnivore;
        private T _factory;

        /// <summary>
        /// Contructor of Animalworld
        /// </summary>
        public AnimalWorld() {
            // Create new continent factory
            _factory = new T();

            // Factory creates carnivores and herbivores
            _carnivore = _factory.CreateCarnivore();
            _herbivore = _factory.CreateHerbivore();
        }

        /// <summary>
        /// Runs the foodchain, that is, carnivores are eating herbivores.
        /// </summary>
        public void RunFoodChain() {
            _carnivore.Eat(_herbivore);
        }
    }
}
