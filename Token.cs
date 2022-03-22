namespace Léxico_0
{
    public class Token
    {
        private string Contenido=" ";
        private Tipos Clasificacion;
        public enum Tipos 
        {
            Identicador, Numero, Caracter, Asignacion, Inicializacion,
            OperadorLogico, OperadorRelacional, OperadorTernario,
            OperadorTermino, OperadorFactor, IncrementoTermino, IncrementoFactor, 
            FinSentencia, Cadena, Comentarios
        }
        public void setContenido(string Contenido)
        {
            this.Contenido = Contenido;
        }
        public void setClasificacion(Tipos Clasificacion)
        {
            this.Clasificacion = Clasificacion;
        }
        public string getContenido()
        {
            return this.Contenido;
        }
        public Tipos getClasificacion()
        {
            return this.Clasificacion;
        }
    }
}