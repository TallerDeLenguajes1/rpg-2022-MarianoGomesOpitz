public class Personaje
{
    public class Caracteristicas
    {
        private int velocidad, destreza, fuerza, nivel, armadura;

        public int Velocidad { get => velocidad; set => velocidad = value; } // 1 - 10
        public int Destreza { get => destreza; set => destreza = value; } // 1 - 5
        public int Fuerza { get => fuerza; set => fuerza = value; } // 1 - 10
        public int Nivel { get => nivel; set => nivel = value; } // 1 - 10
        public int Armadura { get => armadura; set => armadura = value; } // 1 - 10

        public Caracteristicas()
        {

        }
    }

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
        }
    }
}