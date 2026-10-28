using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PyoyectoTest.Models
{
    public class Pelicula
    {
        public string Titulo { get; set; }
        public int Duracion { get; set; }  //minutos
        public DateTime Publicacion { get; set; }
        public string Pais { get; set; }

        //CONSTRUCTOR
        public Pelicula(string titulo, int duracion, DateTime publicacion, string pais)
        {
            this.Titulo = titulo;
            this.Duracion = duracion;
            this.Publicacion = publicacion;
            this.Pais = pais;
        }

        //CONSTRUCTOR COPIA
        public Pelicula(Pelicula obj)
        {
            this.Titulo = obj.Titulo;
            this.Duracion = obj.Duracion;
            this.Publicacion = obj.Publicacion;
            this.Pais = obj.Pais;
        }

    }
}