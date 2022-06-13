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

            Console.WriteLine("\nEl número de peleas se designará aleatoriamente entre 1 y 10. \nEn las peleas, cada atacante podrá atacar y defender 3 veces, al finalizar cada uno con sus acciones. \nGanará aquel que posea más vida, si ambos luchadores poseen la misma cantidad de vida, la misma se repetirá hasta que uno de los caiga");

            int peleas = rand.Next(1, 11), empate = 0; //Ingreso la cantidad de peleas que habrá
            Console.WriteLine($"\nCantidad de peleas que habrá: {peleas}");

            ProcesoDePelea(rand, listaPj, ref carac, ref datos, ref pj, ref peleas, ref empate);

            Ganador(listaPj);
        }

        private static void Ganador(List<Personaje> listaPj)
        {
            Console.WriteLine("\n//--------------------Ganador definitivo--------------------//"); //Muestro al personaje que logró sobrevivir
            Console.WriteLine($"Tipo de personaje: {listaPj[0].Dat.Tipo}");
            Console.WriteLine($"Nombre del personaje: {listaPj[0].Dat.Nombre}");
            Console.WriteLine($"Apodo del personaje: {listaPj[0].Dat.Apodo}");
            Console.WriteLine($"Fecha de nacimiento: {listaPj[0].Dat.FechaNac}");
            Console.WriteLine($"Edad: {listaPj[0].Dat.Edad}");
            Console.WriteLine($"Salud: {listaPj[0].Dat.Salud}");
            Console.WriteLine($"Velocidad: {listaPj[0].Car.Velocidad}");
            Console.WriteLine($"Destreza: {listaPj[0].Car.Destreza}");
            Console.WriteLine($"Fuerza: {listaPj[0].Car.Fuerza}");
            Console.WriteLine($"Nivel: {listaPj[0].Car.Nivel}");
            Console.WriteLine($"Armadura: {listaPj[0].Car.Armadura}");
        }

        private static void ProcesoDePelea(Random rand, List<Personaje> listaPj, ref Caracteristicas carac, ref Datos datos, ref Personaje pj, ref int peleas, ref int empate)
        {
            while (peleas > 0)
            {
                if (empate == 0) //Me aseguro que no la pelea anterior no haya sido empate para no cargar otro personaje
                {
                    carac = new Caracteristicas(); //Agrego otro personaje para luchar
                    datos = new Datos();
                    pj = new Personaje(carac, datos);
                    listaPj.Add(pj);
                }
                else
                {
                    Console.WriteLine("\n/----------La pelea ha acabado en empate, la misma debe repetirse----------/");
                    peleas++;
                }

                Console.WriteLine("\n\nPersonajes a luchar:"); //Muestro la información relevante de los personajes a luchar
                foreach (var item in listaPj)
                {
                    Console.WriteLine($"\nTipo de personaje: {item.Dat.Tipo}");
                    Console.WriteLine($"Nombre del personaje: {item.Dat.Nombre}");
                    Console.WriteLine($"Apodo del personaje: {item.Dat.Apodo}");
                }

                Console.WriteLine("\nPresione para iniciar la pelea"); //Ingresar algo para iniciar la pelea, dando tiempo para ver la información de los personajes
                char p = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                var f = new Funciones();
                Console.WriteLine("\n/---------------Pelea en proceso---------------/");
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
                    empate = 1;//Si fue un empate, la pelea se repetirá
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
                        Console.WriteLine($"\n\n/----------Ganador de la pelea: {item.Dat.Apodo}----------/");
                        item.Dat.Salud = 100;
                        int chance = rand.Next(2);
                        float bonus = 0;
                        if (chance == 0) //Decido que bonus darle al ganador
                        {
                            bonus = 10;
                            item.Dat.Salud += Convert.ToInt32(bonus); //Le doy un bonus de vida
                            Console.WriteLine("{0} ha ganado +{1} de salud", item.Dat.Apodo, bonus);
                        }
                        else
                        {
                            bonus = rand.Next(5, 11);
                            item.Car.Fuerza += Convert.ToInt32(bonus / item.Car.Fuerza); //Le doy un bonus de fuerza
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
            public void procesoDeAtaque(Personaje atacante, Personaje defensor) //Cálculos que hacen posible un ataque
            {
                Console.WriteLine($"\nAtacante: {atacante.Dat.Apodo} \nDefensor: {defensor.Dat.Apodo}");
                var rand = new Random();

                float PD = atacante.Car.Destreza * atacante.Car.Fuerza * atacante.Car.Nivel; //Poder de disparo
                float ED = rand.Next(1, 101); //Efectividad de disparo
                float VA = PD * ED; //Valor de ataque
                float PDEF = defensor.Car.Armadura * defensor.Car.Velocidad; //Poder de defensa
                float MDP = 50000; //Máximo daño provocable
                int DP = Convert.ToInt32((((VA * ED) - PDEF) / MDP) * 5); //Daño provocado

                Console.WriteLine($"Daño ejercido por {atacante.Dat.Apodo}: {DP}");
                defensor.Dat.Salud -= DP;
                Console.WriteLine($"Salud actual de {defensor.Dat.Apodo}: {defensor.Dat.Salud}");
            }
        }
    }
}