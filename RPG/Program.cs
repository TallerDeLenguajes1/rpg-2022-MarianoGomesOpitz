// See https://aka.ms/new-console-template for more information
using System;

namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var listaPj = new List<Personaje>(); //Agrego al primer personaje
            var carac = new Caracteristicas();
            var datos = new Datos();
            var pj = new Personaje(carac, datos);
            listaPj.Add(pj);

            int peleas = rand.Next(1, 11), empate = 0; //Ingreso la cantidad de peleas que habrá
            Console.WriteLine($"Cantidad de peleas que habrá: {peleas}");
            while (peleas > 0)
            {
                if (empate == 0) //Me aseguro que no la pelea anterior no haya sido empate para no cargar otro personaje
                {
                    carac = new Caracteristicas(); //Agrego otro personaje para luchar
                    datos = new Datos();
                    pj = new Personaje(carac, datos);
                    listaPj.Add(pj);
                }

                Console.WriteLine("\nPersonajes a luchar:"); //Muestro la información relevante de los personajes a luchar
                foreach (var item in listaPj)
                {
                    Console.WriteLine("\nTipo de personaje: {0}", item.Dat.Tipo);
                    Console.WriteLine("Nombre del personaje: {0}", item.Dat.Nombre);
                    Console.WriteLine("Apodo del personaje: {0}", item.Dat.Apodo);
                    Console.WriteLine("Salud: {0}", item.Dat.Salud);
                }

                Console.WriteLine("\n\nPresione para iniciar la pelea"); //Ingresar algo para iniciar la pelea, dando tiempo para ver la información de los personajes
                char p = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                var f = new Funciones();
                for (int i = 0; i < 3; i++)
                {
                    f.procesoDeAtaque(listaPj[0], listaPj[1]); //Primero ataca uno y luego ataca otro
                    if (listaPj[1].Dat.Salud <= 0) //Al finalizar un ataque, pregunto si el defensor quedó con la vida al cero, para detener la batalla
                    {
                        break;
                    }

                    f.procesoDeAtaque(listaPj[1], listaPj[0]);
                    if (listaPj[0].Dat.Salud <= 0)
                    {
                        break;
                    }
                }

                if (listaPj[0].Dat.Salud == listaPj[1].Dat.Salud) //Determino al ganador
                {
                    Console.WriteLine("\nEmpate"); //Si fue un empate, la pelea se repetirá
                    empate = 1;
                }
                else
                {
                    if (listaPj[0].Dat.Salud < listaPj[1].Dat.Salud) //Elimino al personaje que menos vida tiene, es decir, el que perdió
                    {
                        listaPj.RemoveAt(0);
                    }
                    else
                    {
                        listaPj.RemoveAt(1);
                    }

                    foreach (var item in listaPj) //Muestro quién es el que ganó la pelea
                    {
                        Console.WriteLine("\n\nGanador de la pelea: {0}", item.Dat.Apodo);
                        int chance = rand.Next(2);
                        double bonus = 0;
                        if (chance == 0) //Decido que bonus darle al ganador
                        {
                            bonus = 10;
                            item.Dat.Salud += bonus; //Le doy un bonus de vida
                            Console.WriteLine("{0} ha ganado +{1} de salud", item.Dat.Apodo, bonus);
                        }
                        else
                        {
                            bonus = rand.Next(5, 11);
                            item.Car.Fuerza += bonus / item.Car.Fuerza; //Le doy un bonus de fuerza
                            Console.WriteLine("{0} ha ganado un {1}% en fuerza", item.Dat.Apodo, bonus);
                        }
                    }
                    empate = 0;
                }
                peleas--;
            }

            Console.WriteLine("\nGanador definitivo:"); //Muestro al personaje que logró sobrevivir
            Console.WriteLine("Tipo de personaje: {0}", listaPj[0].Dat.Tipo);
            Console.WriteLine("Apodo del personaje: {0}", listaPj[0].Dat.Apodo);
            Console.WriteLine("Salud: {0}", listaPj[0].Dat.Salud);
            Console.WriteLine("Velocidad: {0}", listaPj[0].Car.Velocidad);
            Console.WriteLine("Destreza: {0}", listaPj[0].Car.Destreza);
            Console.WriteLine("Fuerza: {0}", listaPj[0].Car.Fuerza);
            Console.WriteLine("Nivel: {0}", listaPj[0].Car.Nivel);
            Console.WriteLine("Armadura: {0}", listaPj[0].Car.Armadura);
        }
        public class Funciones
        {
            public void procesoDeAtaque(Personaje atacante, Personaje defensor) //Cálculos que hacen posible un ataque
            {
                Console.WriteLine($"\nAtacante: {atacante.Dat.Apodo}. Defensor: {defensor.Dat.Apodo}");
                var rand = new Random();

                double PD = atacante.Car.Destreza * atacante.Car.Fuerza * atacante.Car.Nivel; //Poder de disparo
                double ED = rand.Next(1, 101); //Efectividad de disparo
                double VA = PD * ED; //Valor de ataque
                double PDEF = defensor.Car.Armadura * defensor.Car.Velocidad; //Poder de defensa
                double MDP = 50000; //Máximo daño provocable
                double DP = (((VA * ED) - PDEF) / MDP) * 100; //Daño provocado

                Console.WriteLine($"Daño ejercido por {atacante.Dat.Apodo}: {DP}");
                defensor.Dat.Salud -= DP;
                Console.WriteLine($"Salud actual de {defensor.Dat.Apodo}: {defensor.Dat.Salud}");
            }
        }
    }
}