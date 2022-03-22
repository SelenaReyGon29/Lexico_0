namespace Léxico_0
{
    /*
        Requerimiento 1: Agregar la notación matematica a los números, ejemplo: 3.5e-8
        Requerimiento 2: Programar el reconocimiento de los códigos de linea, sin que 
                        sea considerado como un Token, ejemplo: 
                        x26=5//hola mundo;
    */
    public class Lexico : Token
    {
        StreamReader Archivo;
        StreamWriter Log;
        public Lexico()
        { 
            Archivo = new StreamReader("C:\\Léxico_0\\Prueba.cpp");
            Log     = new StreamWriter("C:\\Léxico_0\\Prueba.log");
            Log.AutoFlush = true;
        }
           public void Cerrar()
        {
            Archivo.Close();
            Log.Close();
        }
          public void NextToken()
        {
            char c;
            string Buffer = "";
            
            while(char.IsWhiteSpace(c = (char) Archivo.Read()));

            bool NoHuboDiagonal = true;
            if( c == '/') //OPERADOR INCREMENTO
            {
                Buffer+=c;
                NoHuboDiagonal = true;
                setClasificacion(Tipos.OperadorFactor);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '=' )
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoFactor);
                    Archivo.Read();
                }
                else if ((c=(char) Archivo.Peek()) == '/')
                {
                    NoHuboDiagonal=false;
                    c = (char)Archivo.Read();
                    while(FinArchivo() == false && (c = (char) Archivo.Read())!= (10))
                    {
                        Buffer="";
                        NoHuboDiagonal = false;

                    }
                }
            }
            else if(char.IsLetter(c))
            {
                Buffer += c;
                while(char.IsLetterOrDigit(c = (char) Archivo.Peek()))
                {
                    Buffer+=c;
                    Archivo.Read();
                }
                setClasificacion(Tipos.Identicador);
            }
            else if(char.IsDigit(c))
            {
                Buffer += c;
                while(char.IsDigit(c = (char) Archivo.Peek()))
                {
                    Buffer += c;
                    Archivo.Read();
                }
                if(c == '.')
                {
                    Buffer+=c;
                    Archivo.Read();
                    if(char.IsDigit(c = (char) Archivo.Peek()))
                    {
                        while(char.IsDigit(c = (char) Archivo.Peek()))
                        {
                            Buffer+=c;
                            Archivo.Read();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error Lexico:  Se espera un digito");   
                        Log.WriteLine("Error Lexico:  Se espera un digito");       
                    } 
                }
                if(c == 'E' || c =='e')
                {
                    Buffer +=c;
                    Archivo.Read();
                    if(!(char.IsDigit(c = (char) Archivo.Peek())))
                    {
                        Buffer +=c;
                        Archivo.Read();
                    }
                    while(char.IsLetterOrDigit(c = (char) Archivo.Peek()))
                    {
                        Buffer+=c;
                        Archivo.Read();
                    }
                }
                setClasificacion(Tipos.Numero);
            }
            else if(c == ';')
            {
                Buffer+=c;
                setClasificacion(Tipos.FinSentencia);
            } 
            /*else if(c == '=')
            {
                Buffer+=c;
                setClasificacion(Tipos.Asignacion);
            }
            else if(c == '*' || c=='%' || c=='/')
            //{
                Buffer+=c;
                setClasificacion(Tipos.OperadorFactor);
            }
            else if(c == '+' || c=='-')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTermino);
            }*/
            else if(c == '&')
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
                if((c=(char) Archivo.Peek())=='&')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorLogico);
                    Archivo.Read();
                }
            }
            else if(c == '|')
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
                if((c=(char) Archivo.Peek())=='|')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorLogico);
                    Archivo.Read();
                }
            }
            else if(c == '!')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorLogico);
                if((c=(char) Archivo.Peek())=='=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }
            /* else if(c == '>' || c == '<')//Mayor o menor
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorRelacional);
            }*/
            else if(c == '>')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorLogico);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }
            else if(c == '<')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorLogico);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }
            else if(c == '=')
            {
                Buffer+=c;
                setClasificacion(Tipos.Asignacion);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }
            else if(c == '"')
            {
                Buffer+=c;
                setClasificacion(Tipos.Cadena);
                while((c = (char)Archivo.Read()) != '"'){
                    Buffer+=c;
                }
                Buffer+=c;
            }
            //OPERADOR TERNARIOS
            else if(c == '?')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTernario);
            }
            else if(c == ':') //INICIALIZACIÓN
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.Inicializacion);
                    Archivo.Read();
                }
            }
            else if(c == '-' || c == '+') //OPERADOR INCREMENTO
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTermino);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '-' || ((c=(char) Archivo.Peek()) == '+'|| (c=(char) Archivo.Peek()) == '=' ))
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoTermino);
                    Archivo.Read();
                }
            }
            /*else if(c == '+') //OPERADOR INCREMENTO
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTermino);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '+' || (c=(char) Archivo.Peek()) == '=' )
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoTermino);
                    Archivo.Read();
                }
            }*/
            else if(c == '*' || c == '%') //OPERADOR INCREMENTO
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorFactor);//OperadorLogico
                if((c=(char) Archivo.Peek()) == '=' )
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoFactor);
                    Archivo.Read();
                }
            }
            else
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
            }
            if (NoHuboDiagonal == true)    
            {                   
                setContenido(Buffer);
                Log.WriteLine(getContenido() + " | " + getClasificacion());
            }
        }
        public bool FinArchivo()
        {
            return Archivo.EndOfStream;
        }
    }
}  
