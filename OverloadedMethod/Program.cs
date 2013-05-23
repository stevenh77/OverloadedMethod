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
                processor.Execute(shape);
            }
        }
    }

    class Shape { }

    class Circle : Shape { }

    class Triangle : Shape { }

    class Processor
    {
        internal void Execute(Shape shape)
        {
            if (shape.GetType() == typeof(Shape)) 
                throw new Exception("Method intended for derived objects");

            MethodInfo method = this.GetType().GetMethod("Execute", 
                                                         BindingFlags.Instance | BindingFlags.NonPublic, 
                                                         null, 
                                                         new [] { shape.GetType() }, 
                                                         null);
            method.Invoke(this, new object[] { shape });
        }

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
