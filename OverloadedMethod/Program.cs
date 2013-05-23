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

            }
        }
    }

    class Shape { }

    class Circle : Shape { }

    class Triangle : Shape { }

    class Processor
    {
        internal void Execute(Circle circle)
        {
            Console.WriteLine("Executing with a Circle!");
        }

        internal void Execute(Triangle triangle)
        {
            Console.WriteLine("Executing with a Triangle!");
        }    
    }
}
