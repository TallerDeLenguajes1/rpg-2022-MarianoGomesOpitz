namespace RPG
{
    public class Caracteristicas
    {
        private double velocidad, destreza, fuerza, nivel, armadura;

        public double Velocidad { get => velocidad; set => velocidad = value; } // 1 - 10
        public double Destreza { get => destreza; set => destreza = value; } // 1 - 5
        public double Fuerza { get => fuerza; set => fuerza = value; } // 1 - 10
        public double Nivel { get => nivel; set => nivel = value; } // 1 - 10
        public double Armadura { get => armadura; set => armadura = value; } // 1 - 10

        public Caracteristicas()
        {
            var rand = new Random();
            velocidad = rand.Next(1, 11);
            destreza = rand.Next(1, 6);
            nivel = rand.Next(1, 11);
            fuerza = rand.Next(1, 11);
            armadura = rand.Next(1, 11);
        }
    }
}