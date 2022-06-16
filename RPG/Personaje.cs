namespace RPG
{
    public class Personaje
    {
        private Caracteristicas car;
        private Datos dat;

        public Caracteristicas Car { get => car; set => car = value; }
        public Datos Dat { get => dat; set => dat = value; }

        public Personaje() { }

        public Personaje(Caracteristicas car, Datos dat)
        {
            this.Car = car;
            this.Dat = dat;
        }
    }
}