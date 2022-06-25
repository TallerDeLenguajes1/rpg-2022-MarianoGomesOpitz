namespace RPG
{
    public class Datos
    {
        private string tipo, nombre, apodo;
        private string fechaNac;
        private int edad;
        private int salud;

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public string FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; } // 0 - 300
        public int Salud { get => salud; set => salud = value; } // 100

        public Datos()
        {
        }

        public Datos(RootNames names)
        {
            var rand = new Random();

            var tipoDispo = new string[] { "Scout", "Soldier", "Pyro", "Demoman", "Heavy", "Engineer", "Medic", "Sniper", "Spy" };
            //var nombreDispo = new string[] { "Alberto", "Bernarda", "Carlos", "Daniela", "Estéfano", "Florencia", "Gerardo", "Helena", "Ignacio", "Jimena", "Lautaro", "Martina", "Nahuel", "Petunia", "Rodrigo", "Sabrina", "Tomás", "Urma" };
            var apodoDispo = new string[] { "Red Reaper", "Gunner", "Doomsday", "Dark Sword", "Insidious", "God's Punisher", "War Dog", "Iron Hand", "Blood Star", "Atomic Arsenic", "Atomic Apple", "Purple Sun", "Anger", "Guts Destructor", "Ego Monster" };

            int valorAleatorioNombre = rand.Next(0, names.Results.Count());//Valor de index aleatorio que definirá que nombre tocará

            tipo = tipoDispo[rand.Next(0, tipoDispo.Length)];

            //La clase posee nombre y apellido, así que los junto (La clase también posee los pronombres (Ms y Mr), pero no los veo necesarios para el programa)
            nombre = $"{names.Results[valorAleatorioNombre].Name.First} {names.Results[valorAleatorioNombre].Name.Last}";
            //Hay vaces que la clase devolverá un nombre con signos de pregunta (?), esto es porque el nombre está
            //en caracteres árabes, chino, etc, y no se convierten.
            //Pero decidí usarlos así, ya que aparenta que ese personaje no quiere revelar su nombre.
            //Osea, soy vago para arreglarlo, pero queda bien
            apodo = apodoDispo[rand.Next(0, apodoDispo.Length)];
            var fechaAux = new DateOnly(rand.Next(1723, 2023), rand.Next(1, 13), rand.Next(1, 29));
            fechaNac = Convert.ToString(fechaAux);
            edad = DateTime.Now.Year - fechaAux.Year;
            if (fechaAux.Month > DateTime.Now.Month)
            {
                edad--;
            }
            else
            {
                if (fechaAux.Month == DateTime.Now.Month && fechaAux.Day > DateTime.Now.Day)
                {
                    edad--;
                }
            }
            salud = 100;
        }
    }
}