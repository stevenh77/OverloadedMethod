using System;
using System.Collections.Generic;
using System.Reflection;

namespace OverloadedMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new Processor();
            var shapes = new List<Shape> {new Circle(), new Triangle()};

            foreach (var shape in shapes)
            {
                // option 1
                processor.GetType()
                         .GetMethod("Execute",
                                    BindingFlags.Instance | BindingFlags.NonPublic,
                                    null,
                                    new[] { shape.GetType() },
                                    null)
                          .Invoke(processor,
                                  new object[] { shape });

                // or option 2 (requires known definition of all drived types and a line per type)
                if (shape is Circle) processor.Execute(shape as Circle);
                else if (shape is Triangle) processor.Execute(shape as Triangle);

                // option 3... BOOM!!!!  Works a treat :)
                processor.Execute(shape as dynamic);
            }
        }
    }

    class Shape { }

    class Circle : Shape 
    {
        public int Circumference { get { return 10; } }
    }

    class Triangle : Shape
    {
        public int HypotenuseLength { get { return 20; } }
    }

    class Processor
    {
        internal void Execute(Circle circle)
        {
            Console.WriteLine("Executing with a Circle with circumference {0}!", circle.Circumference);
        }

        internal void Execute(Triangle triangle)
        {
            Console.WriteLine("Executing with a Triangle hypotenuse length of {0}!", triangle.HypotenuseLength);
        }    
    }
}
