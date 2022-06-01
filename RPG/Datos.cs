namespace RPG
{
    public class Datos
    {
        private string tipo, nombre, apodo;
        private DateOnly fechaNac;
        private int edad, salud;

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateOnly FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; } // 0 - 300
        public int Salud { get => salud; set => salud = value; } // 100

        public Datos()
        {
            var rand = new Random();

            var tipoDispo = new string[] { "Scout", "Soldier", "Pyro", "Demoman", "Heavy", "Engineer", "Medic", "Sniper", "Spy" };
            var nombreDispo = new string[] { "Alberto", "Bernarda", "Carlos", "Daniela", "Estéfano", "Florencia", "Gerardo", "Helena", "Ignacio", "Jimena", "Lautaro", "Martina", "Nahuel", "Petunia", "Rodrigo", "Sabrina", "Tomás", "Urma" };
            var apodoDispo = new string[] { "Red Reaper", "Gunner", "Doomsday", "Dark Sword", "Insidious", "God's Punisher", "War Dog", "Iron Hand", "Blood Star", "Atomic Arsenic", "Atomic Apple", "Purple Sun", "Anger", "Guts Destructor", "Ego Monster" };

            tipo = tipoDispo[rand.Next(0, tipoDispo.Length)];
            nombre = nombreDispo[rand.Next(0, nombreDispo.Length)];
            apodo = apodoDispo[rand.Next(0, apodoDispo.Length)];
            fechaNac = new DateOnly(rand.Next(1723, 2023), rand.Next(1, 13), rand.Next(1, 32));
            edad = DateTime.Now.Year - fechaNac.Year;
            if (fechaNac.Month > DateTime.Now.Month)
            {
                edad--;
            }
            else
            {
                if (fechaNac.Month == DateTime.Now.Month && fechaNac.Day > DateTime.Now.Day)
                {
                    edad--;
                }
            }
            salud = 100;
        }
    }
}