namespace RPG
{
    public class Personaje
    {
        private Datos dat;
        private Caracteristicas car;

        public Datos Dat { get => dat; set => dat = value; }
        public Caracteristicas Car { get => car; set => car = value; }

        public Personaje() {
        }

        public Personaje(RootNames names) { //Envío de clase
            this.Dat = new Datos(names);
            this.Car = new Caracteristicas();
        }

    }
}