using PyoyectoTest.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PyoyectoTest.Services
{
    public class PeliculasService
    {
        // DATOS MIEMBRO

        //METODOS
        public Pelicula ObtenerPelicula()
        {
            DateTime fechaPelicula = new DateTime(2014, 11, 25);
             Pelicula obj = new Pelicula("Interestelar", 120, fechaPelicula, "EEUU");
            return (obj);
        }


        public List<Pelicula> obtenerPeliculas()
        {
            DateTime fechaPelicula1 = new DateTime(2014, 11, 25);
            DateTime fechaPelicula2 = new DateTime(2016, 11, 26);
            DateTime fechaPelicula3 = new DateTime(2001, 11, 27);
            DateTime fechaPelicula4 = new DateTime(2022, 11, 11);

            Pelicula obj1 = new Pelicula("Interestelar", 120, fechaPelicula1, "EEUU");
            Pelicula obj2 = new Pelicula("El Origen", 135, fechaPelicula2, "EEUU");
            Pelicula obj3 = new Pelicula("Batman: Inicia", 145, fechaPelicula3, "EEUU");
            Pelicula obj4 = new Pelicula("Venom 3", 120, fechaPelicula4, "Canada");

            List<Pelicula> arregloPeliculas = new List<Pelicula> { obj1, obj2, obj3, obj4};
            
            
            
            return (arregloPeliculas);
        }


    }
}