using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace juego
{
    class PokemonsJugador
    {
        static List<String> nombre_pokemon = new List<string>();
        static List<int> salud_pokemon = new List<int>();
        static List<String> podereDisponibles = new List<string>();

        public  PokemonsJugador ()
        {
            Random rnd = new Random();
            for (int i = 0; i<5; i++)
            {
                
                salud_pokemon.Add(100);
               
            }
            
        }

    }

}
