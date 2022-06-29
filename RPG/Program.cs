// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace RPG
{
    internal class Program
    {

        ///////////////////////////////////////////////////////////////////////////////////////Programa principal
        static void Main(string[] args)
        {
            string archivoGanadores = "ganadores.csv", archivoJugadores = "jugadores.json";

            var rand = new Random();

            RootNames names = null;
            names = NombresApi(names);
            //Creación de la clase de la Api
            //Decido crearla ya, para que no me de inconvenientes por si se hace más de una pelea
            //Así que la clase tendrá 30 nombres en total (se puede cambiar la cantidad, y otras cosas modificando el link), reduciendo la posibilidad de que se repita alguno
            //Link dentro de la función
            do
            {

                Console.WriteLine("\n¿Desea ver la lista de ganadores previos, llevar a cabo unas batallas, o finalizar el programa? \n\"L\" para ver la lista, \"F\" para finalizar, o cualquier otro caracter para pelear");
                char pel = Console.ReadLine()[0];

                if (pel == 'l' || pel == 'L')
                {
                    LeerArchivo(archivoGanadores);
                }
                else
                {
                    if (pel == 'f' || pel == 'F')
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nAntes de iniciar la pelea ¿Desea guardar la información de los luchadores?");
                        Console.WriteLine("\"G\" para guardar o cualquier otro caracter para no hacerlo");
                        char guardar = Console.ReadLine()[0];
                        EleccionPelea(archivoGanadores, rand, guardar, archivoJugadores, names);
                    }
                }
            } while (true);
        }

        private static RootNames NombresApi(RootNames names)
        {
            string url = $"https://randomuser.me/api/?inc=name&?page=1&results=30&noinfo&?nat=us,%20es,%20nz,%20au,%20fr,%20fi";
            //Fuente de la api: https://randomuser.me/
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string texto = objReader.ReadToEnd();

                                names = JsonSerializer.Deserialize<RootNames>(texto);



                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return names;
        }


        ///////////////////////////////////////////////////////////////////////////////////////Se eligió pelear
        private static void EleccionPelea(string archivoGanadores, Random rand, char guardar, string archivoJugadores, RootNames names)
        {
            var listaPj = new List<Personaje>(); //Agrego al primer personaje
            Personaje pj;

            Console.WriteLine("\nElección del primer personaje");
            pj = EleccionOCreacionPersonaje(archivoJugadores, names);

            listaPj.Add(pj);


            var listaJugadores = new List<Personaje>();
            if (guardar == 'g' || guardar == 'G')
            {
                listaJugadores.Add(pj);
            }

            Console.WriteLine("\nEl número de peleas se designará aleatoriamente entre 1 y 10. \nEn las peleas, cada atacante podrá atacar y defender 3 veces, al finalizar cada uno con sus acciones. \nGanará aquel que posea más vida, si ambos luchadores poseen la misma cantidad de vida, la misma se repetirá hasta que uno de los caiga");

            int peleas = rand.Next(1, 11), empate = 0; //Ingreso la cantidad de peleas que habrá
            Console.WriteLine($"\nCantidad de peleas que habrá: {peleas}");

            ProcesoDePelea(rand, listaPj, ref peleas, ref empate, guardar, listaJugadores, archivoJugadores, names);

            Ganador(listaPj, archivoGanadores);

            if ((guardar == 'g' || guardar == 'G') && listaJugadores != null)
            {
                foreach (var item in listaJugadores)
                {
                    item.Dat.Salud = 100;
                }
                string textoJugadores = JsonSerializer.Serialize(listaJugadores);
                var escribirJson = new StreamWriter(File.Open(archivoJugadores, FileMode.Create));
                escribirJson.WriteLine(textoJugadores);
                escribirJson.Close();
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////Se decide si se crea o se elige un personaje
        private static Personaje EleccionOCreacionPersonaje(string archivoJugadores, RootNames names)
        {
            Personaje pj;
            Console.WriteLine("¿Desea crear un nuevo personaje o elegir uno?");
            Console.WriteLine("\"E\" para elegir uno o cualquier otro caracter para crear uno nuevo");
            char elegir = Console.ReadLine()[0];
            if (elegir == 'E' || elegir == 'e')
            {
                string textoJugadores = File.ReadAllText(archivoJugadores);
                var infoJugadores = JsonSerializer.Deserialize<List<Personaje>>(textoJugadores);

                if (infoJugadores.Count > 0)
                {
                    Console.WriteLine("\nPersonajes disponibles:");
                    foreach (var item in infoJugadores)
                    {
                        Console.WriteLine($"\nNombre del personaje: {item.Dat.Nombre}");
                        Console.WriteLine($"Apodo del personaje: {item.Dat.Apodo}");
                        Console.WriteLine($"Tipo de personaje: {item.Dat.Tipo}");
                        Console.WriteLine($"Fecha de nacimiento: {item.Dat.FechaNac}");
                        Console.WriteLine($"Edad: {item.Dat.Edad}");
                        Console.WriteLine($"Salud: {item.Dat.Salud}");
                        Console.WriteLine($"Velocidad: {item.Car.Velocidad}");
                        Console.WriteLine($"Destreza: {item.Car.Destreza}");
                        Console.WriteLine($"Fuerza: {item.Car.Fuerza}");
                        Console.WriteLine($"Nivel: {item.Car.Nivel}");
                        Console.WriteLine($"Armadura: {item.Car.Armadura}");
                    }

                    Console.WriteLine($"\nElija uno del 1 al {infoJugadores.Count}");
                    int eleccion = Convert.ToInt32(Console.ReadLine());
                    pj = infoJugadores[eleccion - 1];
                }
                else
                {
                    Console.WriteLine("\nNo hay personajes disponibles para elegir, se procederá a crear uno nuevo");
                    pj = new Personaje(names);
                }

            }
            else
            {
                pj = new Personaje(names);//Tengo que enviar la clase para ver que nombre le tocará al presonaje
            }

            return pj;
        }


        ///////////////////////////////////////////////////////////////////////////////////////Leo y muestro el archivo de los ganadores
        private static void LeerArchivo(string archivoGanadores)
        {
            var leer = new StreamReader(File.Open(archivoGanadores, FileMode.Open));
            Console.WriteLine($"\n{leer.ReadToEnd()}");
            leer.Close();
        }


        ///////////////////////////////////////////////////////////////////////////////////////Muestro al ganador de la batalla y lo guardo en el archivo de los ganadores
        private static void Ganador(List<Personaje> listaPj, string archivoGanadores)
        {
            Console.WriteLine("\n//--------------------Ganador definitivo--------------------//"); //Muestro al personaje que logró sobrevivir
            Console.WriteLine($"Nombre del personaje: {listaPj[0].Dat.Nombre}");
            Console.WriteLine($"Apodo del personaje: {listaPj[0].Dat.Apodo}");
            Console.WriteLine($"Tipo de personaje: {listaPj[0].Dat.Tipo}");
            Console.WriteLine($"Fecha de nacimiento: {listaPj[0].Dat.FechaNac}");
            Console.WriteLine($"Edad: {listaPj[0].Dat.Edad}");
            Console.WriteLine($"Salud: {listaPj[0].Dat.Salud}");
            Console.WriteLine($"Velocidad: {listaPj[0].Car.Velocidad}");
            Console.WriteLine($"Destreza: {listaPj[0].Car.Destreza}");
            Console.WriteLine($"Fuerza: {listaPj[0].Car.Fuerza}");
            Console.WriteLine($"Nivel: {listaPj[0].Car.Nivel}");
            Console.WriteLine($"Armadura: {listaPj[0].Car.Armadura}");

            var escribir = new StreamWriter(File.Open(archivoGanadores, FileMode.Append));
            string texto = $"Nombre: {listaPj[0].Dat.Nombre}, Apodo: {listaPj[0].Dat.Apodo}, Tipo de personaje: {listaPj[0].Dat.Tipo}, Fecha de nacimiento: {listaPj[0].Dat.FechaNac}, Momento en el que se llevó la batalla: {DateTime.Now}";
            escribir.WriteLine(texto);
            escribir.Close();


        }


        ///////////////////////////////////////////////////////////////////////////////////////El proceso entero de las batallas
        private static void ProcesoDePelea(Random rand, List<Personaje> listaPj, ref int peleas, ref int empate, char guardar, List<Personaje> listaJugadores, string archivoJugadores, RootNames names)
        {
            while (peleas > 0)
            {
                if (empate == 0) //Me aseguro que no la pelea anterior no haya sido empate para no cargar otro personaje
                {
                    Personaje pj;

                    pj = EleccionOCreacionPersonaje(archivoJugadores, names);

                    listaPj.Add(pj);

                    if (guardar == 'g' || guardar == 'G')
                    {
                        listaJugadores.Add(pj);
                    }
                }
                else
                {
                    Console.WriteLine("\n/----------La pelea ha acabado en empate, la misma debe repetirse----------/");
                    peleas++;
                }

                Console.WriteLine("\n\nPersonajes a luchar:"); //Muestro la información relevante de los personajes a luchar
                foreach (var item in listaPj)
                {
                    Console.WriteLine($"\nNombre del personaje: {item.Dat.Nombre}");
                    Console.WriteLine($"Apodo del personaje: {item.Dat.Apodo}");
                    Console.WriteLine($"Tipo de personaje: {item.Dat.Tipo}");
                }

                Console.WriteLine("\nPresione para iniciar la pelea"); //Ingresar algo para iniciar la pelea, dando tiempo para ver la información de los personajes
                char p = Console.ReadKey().KeyChar; ;
                Console.WriteLine("\n");
                Console.WriteLine("\n/---------------Pelea en proceso---------------/");
                for (int i = 0; i < 3; i++)
                {
                    char continuar;

                    Funciones.procesoDeAtaque(listaPj[0], listaPj[1]); //Primero ataca uno y luego ataca otro
                    Console.WriteLine("\nPresione para continuar");
                    continuar = Console.ReadKey().KeyChar;
                    if (listaPj[1].Dat.Salud <= 0) //Al finalizar un ataque, pregunto si el defensor quedó con la vida al cero, para detener la batalla
                    {
                        break;
                    }

                    Funciones.procesoDeAtaque(listaPj[1], listaPj[0]);
                    Console.WriteLine("\nPresione para continuar");
                    continuar = Console.ReadKey().KeyChar;
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

        static class Funciones
        {

            ///////////////////////////////////////////////////////////////////////////////////////Cada ataque independiente
            public static void procesoDeAtaque(Personaje atacante, Personaje defensor) //Cálculos que hacen posible un ataque
            {
                Console.WriteLine($"\n\nAtacante: {atacante.Dat.Apodo} \nDefensor: {defensor.Dat.Apodo}");
                var rand = new Random();

                float PD = atacante.Car.Destreza * atacante.Car.Fuerza * atacante.Car.Nivel; //Poder de disparo
                float ED = rand.Next(1, 101); //Efectividad de disparo
                float VA = PD * ED; //Valor de ataque
                float PDEF = defensor.Car.Armadura * defensor.Car.Velocidad; //Poder de defensa
                float MDP = 50000; //Máximo daño provocable
                int DP = Convert.ToInt32((((VA * ED) - PDEF) / MDP) * 5); //Daño provocado
                /*
                ¡¡¡ATENCIÓN!!! Se decidió cambiar un cálculo en la fórmula de ataque.
                En la última operación se multiplicaba por 100, y eso provocaba que el daño resultante sea
                mayor a 100, es decir, mataba al contrincante de un hit, por lo que se lo cambió por 5,
                un valor no tan grande que, sigue provocando daños mayores a 100, pero ya muy infrecuentemente ;)
                */

                Console.WriteLine($"Daño ejercido por {atacante.Dat.Apodo}: {DP}");
                defensor.Dat.Salud -= DP;
                Console.WriteLine($"Salud actual de {defensor.Dat.Apodo}: {defensor.Dat.Salud}");
            }
        }
    }
}