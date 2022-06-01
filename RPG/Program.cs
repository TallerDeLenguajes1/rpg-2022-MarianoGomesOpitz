// See https://aka.ms/new-console-template for more information
using System;

namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var listaPj = new List<Personaje>();
            var carac = new Caracteristicas();
            var datos = new Datos();
            var pj = new Personaje(carac, datos);
            listaPj.Add(pj);

            int peleas = 10, empate = 0;
            while (peleas > 0)
            {
                if (empate == 0)
                {
                    carac = new Caracteristicas();
                    datos = new Datos();
                    pj = new Personaje(carac, datos);
                    listaPj.Add(pj);
                }

                Console.WriteLine("Personajes a luchar:");
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
                Console.WriteLine("\n\nPresione para iniciar la pelea");
                char p = Console.ReadKey().KeyChar;

                var f = new Funciones();
                for (int i = 0; i < 3; i++)
                {
                    f.procesoDeAtaque(listaPj[0], listaPj[1]);
                    if (listaPj[1].Dat.Salud <= 0)
                    {
                        break;
                    }

                    f.procesoDeAtaque(listaPj[1], listaPj[0]);
                    if (listaPj[0].Dat.Salud <= 0)
                    {
                        break;
                    }
                }

                if (listaPj[0].Dat.Salud == listaPj[1].Dat.Salud)
                {
                    Console.WriteLine("\nEmpate");
                    empate = 1;
                }
                else
                {
                    if (listaPj[0].Dat.Salud < listaPj[1].Dat.Salud)
                    {
                        listaPj.RemoveAt(0);
                    }
                    else
                    {
                        listaPj.RemoveAt(1);
                    }

                    foreach (var item in listaPj)
                    {
                        Console.WriteLine("\n\nGanador de la pelea: {0}", item.Dat.Apodo);
                        int chance = rand.Next(2);
                        double bonus = 0;
                        if (chance == 0)
                        {
                            bonus = 10;
                            item.Dat.Salud += bonus;
                            Console.WriteLine("{0} ha ganado +{1} de salud", item.Dat.Apodo, bonus);
                        }
                        else
                        {
                            bonus = (rand.Next(5, 11)) / item.Car.Fuerza;
                            item.Car.Fuerza += bonus;
                            Console.WriteLine("{0} ha ganado un {1}% en fuerza", item.Dat.Apodo, bonus);
                        }
                    }
                    empate = 0;
                }
                peleas--;
            }




        }
        public class Funciones
        {
            public void procesoDeAtaque(Personaje atacante, Personaje defensor)
            {
                Console.WriteLine($"\nAtacante: {atacante.Dat.Apodo}. Defensor: {defensor.Dat.Apodo}");
                var rand = new Random();

                double PD = atacante.Car.Destreza * atacante.Car.Fuerza * atacante.Car.Nivel; //Poder de disparo
                double ED = rand.Next(1, 101); //Efectividad de disparo
                double VA = PD * ED; //Valor de ataque
                double PDEF = defensor.Car.Armadura * defensor.Car.Velocidad; //Poder de defensa
                double MDP = 50000; //Máximo daño provocable
                double DP = (((VA * ED) - PDEF) / MDP) * 100; //Daño provocado

                Console.WriteLine($"\nValores: PD:{PD}, ED:{ED}, VA:{VA}, PDEF:{PDEF}, MDP:{MDP}, DP:{DP}");

                Console.WriteLine($"Daño ejercido por {atacante.Dat.Apodo}: {DP}");
                defensor.Dat.Salud -= DP;
                Console.WriteLine($"Salud actual de {defensor.Dat.Apodo}: {defensor.Dat.Salud}");
            }
        }
    }
}