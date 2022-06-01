// See https://aka.ms/new-console-template for more information
using System;

namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la cantidad de personajes:");
            int cant = Convert.ToInt32(Console.ReadLine());
            var listaPj = new List<Personaje>();
            for (int i = 0; i < cant; i++)
            {
                var carac = new Caracteristicas();
                var datos = new Datos();
                var pj = new Personaje(carac, datos);
                listaPj.Add(pj);
            }

            Console.WriteLine("Personajes:");
            foreach (var item in listaPj)
            {
                Console.WriteLine("\nTipo de personaje: {0}", item.Dat.Tipo);
                Console.WriteLine("Nombre del personaje: {0}", item.Dat.Nombre);
                Console.WriteLine("Apodo del personaje: {0}", item.Dat.Apodo);
                Console.WriteLine("Fecha de nacimiento: {0}", item.Dat.FechaNac);
                Console.WriteLine("Edad: {0} años", item.Dat.Edad);
                Console.WriteLine("Salud: {0}", item.Dat.Salud);
                Console.WriteLine("Velocidad: {0}", item.Car.Velocidad);
                Console.WriteLine("Destreza: {0}", item.Car.Destreza);
                Console.WriteLine("Fuerza: {0}", item.Car.Fuerza);
                Console.WriteLine("Nivel: {0}", item.Car.Nivel);
                Console.WriteLine("Armadura: {0}", item.Car.Armadura);
            }
        }
    }
}