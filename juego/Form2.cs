using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace juego
{
    public partial class Form2 : Form
    { 

        string[] ataques_agua = new string[] { "hidrobomba", "Burbuja" };//arreglo de caracteres de los posibles ataques tipo agua
        string[] ataques_fuego = new string[] { "lanzallamas", "fuego" };//arreglo de caracteres de los posibles ataques tipo fuego
        string[] ataques_elect = new string[] { "rayo", "trueno" };//arreglo de caracteres de los posibles ataques tipo electrico
        string[] ataques_planta = new string[] { "golpe", "rafaga" };//arreglo de caracteres de los posibles ataques tipo planta
        string[] mis_ataques = new string[4];//arreglo de caracteres que guarda los ataques del jugador
        string[] ataques_cpu = new string[4];//arreglo de caracteres que guarda los ataques del CPU
        int ran = 0;
        bool turnojugado=false;//variable que indica que el jugador jugo su turno o ataco
        bool ataque_agua, ataque_elect, ataque_planta, ataque_fuego, ataque_agua_cpu, ataque_elect_cpu, ataque_planta_cpu, ataque_fuego_cpu, movimiento;
        //variables de tipo que indican el tipo de ataque que fue usado en base a estos se despliega la imagen correspondiente al ataque
        int posicion_mia,posicion_mia2, posicion_cpu, posicion_cpu2;//variables auxiliares para simular el acercamiento de los pokemon al conecta los golpes
        int vida_cpu = 100;//puntos de vida de la CPU
        int vida_jugador = 100;//puntos de vida del jugador
        int aux_posicion_x = 227;
        int aux_posicion_y = 100;

        int meta_arriba = 0, limite_abajo = 0, limite_izquierda = 0, limite_derecha= 0;

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        //timer que controla el ataque de la CPU (esta parte fue el conflicto del juego ya que antes de optar por usar otro timer se perdio bastante tiempo tratando de implementar otras soluciones)
        private void timer2_Tick(object sender, EventArgs e)
        {
            Atacar_cpu();//funcion de ataque de la CPU
        }


       //uso de sobrecarga de constructores
        public Form2()
        {   //inicializacion de los componentes de la ventana

            InitializeComponent();
        }
        public Form2(String pokemon_actual)
        {
            string mi_pokemon = pokemon_actual;//se recupera el nombre del pokemon traido desde la otra ventana
            InitializeComponent();//inicializacion de los componentes de la ventana
            Random posicion = new Random();
            int pic = posicion.Next(0, 9);
            switch (pic)
            {
                case 1:
                    pictureBox8.Left = 62;
                    pictureBox7.Left = 62;
                    break;
                case 2:
                    pictureBox8.Left = 95;
                    pictureBox7.Left = 95;
                    break;
                case 3:
                    pictureBox8.Left = 128;
                    pictureBox7.Left = 128;
                    break;
                case 4:
                    pictureBox8.Left = 161;
                    pictureBox7.Left = 161;
                    break;
                case 5:
                    pictureBox8.Left = 194;
                    pictureBox7.Left = 194;
                    break;
                case 6:
                    pictureBox8.Left = 227;
                    pictureBox7.Left = 227;
                    break;
                case 7:
                    pictureBox8.Left = 260;
                    pictureBox7.Left = 260;
                    break;
                case 8:
                    pictureBox8.Left = 293;
                    pictureBox7.Left = 293;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }
       //metodo que carga el formulario de la ventana
        private void Form2_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            timer1.Start();//inicio del timer del jugador


            //esta parte del codigo representaba la eleccion del pokemon de la CPU
            //dado que el primer rival era el "facil" tendriamos una ventaja elemental contra el 
            // al final se opto por un nivel normal debido al problema con los ataques coordinados del CPU
            
            ran = rnd.Next(0, 2);//aleatorio entre 0 y 1
            mis_ataques[0] = ataques_agua[ran];//asignacion de los ataques del jugador en base al aleatorio y los arreglos de ataques
            mis_ataques[1] = ataques_elect[ran];//asignacion de los ataques del jugador en base al aleatorio y los arreglos de ataques
            mis_ataques[2] = ataques_planta[ran];//asignacion de los ataques del jugador en base al aleatorio y los arreglos de ataques
            mis_ataques[3] = ataques_fuego[ran];//asignacion de los ataques del jugador en base al aleatorio y los arreglos de ataques
            label2.Text = mis_ataques[0];//se muestra en pantalla los ataques disponibles y con que tecla se usan 
            label3.Text = mis_ataques[1];
            label4.Text = mis_ataques[2];
            label5.Text = mis_ataques[3];
            int ran2 = rnd.Next(0, 2);//aleatorio entre 0 y 1
            ataques_cpu[0] = ataques_agua[ran2];//asignacion de los ataques del CPU en base al aleatorio y los arreglos de ataques
            ataques_cpu[1] = ataques_elect[ran2];//asignacion de los ataques del CPU en base al aleatorio y los arreglos de ataques
            ataques_cpu[2] = ataques_planta[ran2];//asignacion de los ataques del CPU en base al aleatorio y los arreglos de ataques
            ataques_cpu[3] = ataques_fuego[ran2];//asignacion de los ataques del CPU en base al aleatorio y los arreglos de ataques

            posicion_mia = pictureBox1.Left;//recuperacion de la posicion del pokemon del jugador y CPU servira más adelante
            posicion_mia2 = pictureBox1.Top;
            posicion_cpu = pictureBox2.Left;
            posicion_cpu2 = pictureBox2.Top;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Atacar();//funcion de ataque del jugador
        
        }
        private void Atacar(){

          
            Random rnd = new Random();//uso de la clase random
            if (movimiento && turnojugado)
            {
                pictureBox7 = pictureBox7;
               
            }

            else
            {
                if (ataque_agua && turnojugado)//condiciona que evalua el tipo de atque usado y si el turno es del jugador
                {
                    if (mis_ataques[0] == "hidrobomba")//se evalua si el ataque del jugador coincide con un elemento del arreglo de ataque 
                    {
                        pictureBox2.Image = Properties.Resources.dragon;//impresion del pokemon del CPU
                        pictureBox2.Update();//actualizacion simulando la animacion
                        pictureBox2.Image = Properties.Resources.agua;//se usa el sprite correspondiente al ataque y se muestra
                        pictureBox2.Update();//actualizacion simulando la animacion
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;//se coloca el pokemon del rival del fondo para hacer simulacion de ser golpeado por el ataque
                        pictureBox1.Left += 30;//simulacion de acercamiento del pokemon del jugador
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 30);//daño aleatorio segun el ataque 
                        vida_cpu = vida_cpu - reducir;//reduccion de puntos de vida en base al daño

                        // apartir de este parte el codigo tiene un funcionamiento similar en base al tipo de ataque que ejecuto

                    }
                    if (mis_ataques[0] == "Burbuja" && turnojugado)
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.agua_Canon;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 20);
                        vida_cpu = vida_cpu - reducir;

                    }
                }

                if (ataque_fuego && turnojugado)
                {
                    if (mis_ataques[3] == "lanzallamas")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.fuego;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 20);
                        vida_cpu = vida_cpu - reducir;

                    }
                    if (mis_ataques[3] == "fuego")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.aro_de_fuego;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 30);
                        vida_cpu = vida_cpu - reducir;

                    }

                }

                if (ataque_elect && turnojugado)
                {
                    if (mis_ataques[1] == "trueno")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.onda_electrica;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 20);
                        vida_cpu = vida_cpu - reducir;

                    }
                    if (mis_ataques[1] == "rayo")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.rayo;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 30);
                        vida_cpu = vida_cpu - reducir;

                    }


                }
                if (ataque_planta)
                {
                    if (mis_ataques[2] == "rafaga")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.rafaga_planta;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 20);
                        vida_cpu = vida_cpu - reducir;

                    }
                    if (mis_ataques[2] == "golpe")
                    {
                        pictureBox2.Image = Properties.Resources.dragon;
                        pictureBox2.Update();
                        pictureBox2.Image = Properties.Resources.golpe;
                        pictureBox2.Update();
                        pictureBox2.BackgroundImage = Properties.Resources.dragon;
                        pictureBox1.Left += 30;
                        pictureBox1.Top -= 30;

                        int reducir = rnd.Next(10, 30);
                        vida_cpu = vida_cpu - reducir;

                    }


                }

                if (pictureBox7.Bounds.IntersectsWith(pictureBox5.Bounds))
                {
                    timer1.Stop();//se detienen ambos timer
                    timer2.Stop();
                    MessageBoxButtons buttons = MessageBoxButtons.OK;//declaracion de un buton de una ventana emergente
                    DialogResult result;//varible usada para mostrar un mensaje en la ventana emergente

                    // Displays the MessageBox.
                    result = MessageBox.Show("Haz ganado felicidades", "", buttons);//impresion del mensaje de victoria
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {

                        this.Close();//una vez que se presiona el boton se cierra esta ventana

                    }
                }
            }
            //condicional que revisa si el jugador jugo su turno 
            if (turnojugado)
            {
                //uso de las posiciones anteriormente tomadas para simular que el pokemon regreso a su sitio
                pictureBox1.Left = posicion_mia;
                pictureBox1.Top = posicion_mia2;
                pictureBox2.Left = posicion_cpu;
                pictureBox2.Top = posicion_cpu2;
                pictureBox2.Image = Properties.Resources.dragon;

                 label7.Text = "TURNO DEL CPU";
            }
            timer2.Start();//inicio del ataque del CPU
            if (vida_cpu <= 0)//condicional que evalua si el CPU puede seguir luchando si se cumple el CPU perdio
            {
                timer1.Stop();//se detienen ambos timer
                timer2.Stop();
                MessageBoxButtons buttons = MessageBoxButtons.OK;//declaracion de un buton de una ventana emergente
                DialogResult result;//varible usada para mostrar un mensaje en la ventana emergente

                // Displays the MessageBox.
                result = MessageBox.Show("Haz ganado felicidades", "", buttons);//impresion del mensaje de victoria
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    
                    this.Close();//una vez que se presiona el boton se cierra esta ventana
                   
                }
            }
            label13.Text = vida_cpu.ToString();//muestra de la vida del pokemon del CPU
            timer2.Start();
            
        }
        private void Atacar_cpu()
        {
           
            Random rnd = new Random();//uso de la clase random
            int aleatorio = rnd.Next(1, 5);//random entre 1 y 4 que son los atques del pokemon
            int mov = rnd.Next(0, 2);
            //condicionales que evaluan el tipo de ataque a usar en base al aleatorio

            if (aleatorio == 1)
            {
                ataque_agua_cpu = true;
                

            }
            if (aleatorio == 2)
            {
                ataque_elect_cpu = true;
                
            }
            if (aleatorio == 3)
            {
                ataque_planta_cpu = true;
               
            }
            if (aleatorio == 4)
            {
                ataque_fuego_cpu = true;
               
            }

           
            if (mov == 0 && turnojugado )
            {
                aleatorio = 1;
                if (aleatorio == 1)
                {
                   if(pictureBox8.Left - 66 <  62)
                   {
                        pictureBox8.Left = 62;
                    }
                   else
                   {
                        pictureBox8.Left = pictureBox8.Left - 66;
                       
                   }
                
                }
                if (aleatorio == 2)
                {
                    if (pictureBox8.Top < 100)
                    {
                        pictureBox8.Top = 100;
                    }
                    else
                    {
                        pictureBox8.Top = pictureBox8.Top - 66;
                        aux_posicion_y = aux_posicion_y - 66;
                    }
                }
                if (aleatorio == 3)
                {
                    if (pictureBox8.Top + 66 >334)
                    {
                        pictureBox8.Top = 334;
                    }
                    else
                    {

                        pictureBox8.Top = pictureBox8.Top + 66;
                        
                    }
                }
                if (aleatorio == 4)
                {
                    if (pictureBox8.Left + 66 > 294)
                    {
                        pictureBox8.Left = 294;
                    }
                    else
                    {
                        pictureBox8.Left = pictureBox8.Left + 66;
                        
                    }
                }


                label7.Text = "TURNO DEL JUGADOR";
            }
            else
            {
                //a partir de esta parte el codigo es igual al ataque del jugador
                if (ataque_agua_cpu && turnojugado)
                {
                    if (ataques_cpu[0] == "hidrobomba")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.agua;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 30);
                        vida_jugador = vida_jugador - reducir;

                    }
                    if (ataques_cpu[0] == "Burbuja")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.agua_Canon;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 20);
                        vida_jugador = vida_jugador - reducir;

                    }

                    ataque_agua_cpu = false;
                    turnojugado = false;
                }

                if (ataque_fuego_cpu && turnojugado)
                {
                    if (ataques_cpu[3] == "lanzallamas")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.fuego;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 20);
                        vida_jugador = vida_jugador - reducir;

                    }
                    if (ataques_cpu[3] == "fuego")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.aro_de_fuego;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 30);
                        vida_jugador = vida_jugador - reducir;

                    }
                    ataque_fuego_cpu = false;

                    turnojugado = false;
                }

                if (ataque_elect_cpu && turnojugado)
                {
                    if (ataques_cpu[1] == "trueno")
                    {
                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.onda_electrica;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 20);
                        vida_jugador = vida_jugador - reducir;

                    }
                    if (ataques_cpu[1] == "rayo")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.rayo;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 30);
                        vida_jugador = vida_jugador - reducir;

                    }
                    ataque_elect_cpu = false;
                    turnojugado = false;
                }
                if (ataque_planta_cpu && turnojugado)
                {
                    if (ataques_cpu[2] == "rafaga")
                    {
                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.rafaga_planta;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 20);
                        vida_jugador = vida_jugador - reducir;

                    }
                    if (ataques_cpu[2] == "golpe")
                    {

                        pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox1.Update();
                        pictureBox1.Image = Properties.Resources.golpe;
                        pictureBox1.Update();
                        pictureBox1.BackgroundImage = Properties.Resources.tortuga_preview_rev_2;
                        pictureBox2.Left -= 30;
                        pictureBox2.Top += 30;

                        int reducir = rnd.Next(10, 30);
                        vida_jugador = vida_jugador - reducir;

                    }
                    ataque_planta_cpu = false;

                    turnojugado = false;
                }
                if (ataque_agua_cpu == false)
                {
                    pictureBox1.Left = posicion_mia;
                    pictureBox1.Top = posicion_mia2;
                    pictureBox2.Left = posicion_cpu;
                    pictureBox2.Top = posicion_cpu2;
                    pictureBox2.Image = Properties.Resources.dragon;
                }
                pictureBox1.Image = Properties.Resources.tortuga_preview_rev_2;

                if (vida_jugador <= 0)//condicional que evalua la vida del pokemon del jugador si el condicional cumple se accede al codigo en el interior
                {
                    timer1.Stop();//se detienen ambos timer
                    timer2.Stop();
                    pictureBox3.Visible = true;//se muestra una pantalla de game over que esta oculta y solo aparece si el jugador pierde

                }
                if (pictureBox8.Bounds.IntersectsWith(pictureBox6.Bounds))
                {
                    timer1.Stop();//se detienen ambos timer
                    timer2.Stop();
                    pictureBox3.Visible = true;//se muestra una pantalla de game over que esta oculta y solo aparece si el jugador pierde
                }

                label14.Text = vida_jugador.ToString();//impresion de la vida del pokemon del jugador
                label7.Text = "TURNO DEL JUGADOR";
            }
           
           
        }
       //manejadores de eventos que indican si las teclas fueron presionadas 
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //cuando una tecla es presiona comienza el turno del jugador y dependiendo de la tecla se usa cierto ataque
            if (e.KeyCode == Keys.Left)
            {
                ataque_agua = true;
                turnojugado = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                ataque_planta = true;
                turnojugado = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                ataque_fuego = true;
                turnojugado = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                ataque_elect = true;
                turnojugado = true;
            }

            if (e.KeyCode == Keys.A)
            {
               
                turnojugado = true;
                if (pictureBox7.Left - 66 < 62)
                {
                    pictureBox7.Left = 62;
                }
                else
                {
                    pictureBox7.Left = pictureBox7.Left - 66;
                }
            }
            if (e.KeyCode == Keys.W)
            {
              
                if (pictureBox7.Top - 66 < 100)
                {
                    pictureBox7.Top = 100;
                }
                else
                {
                    pictureBox7.Top = pictureBox7.Top - 66;
                }
               
                turnojugado = true;

            }
            if (e.KeyCode == Keys.S)
            {
                if (pictureBox7.Top + 66 > 334)
                {
                    pictureBox7.Top = 100;
                }
                else
                {
                    pictureBox7.Top = pictureBox7.Top + 66;
                }
                turnojugado = true;

            }
            if (e.KeyCode == Keys.D)
            {
                if (pictureBox7.Left + 66 > 293)
                {
                    pictureBox7.Left = 293;
                }
                else
                {
                    pictureBox7.Left = pictureBox7.Left + 66;
                }
                turnojugado = true;

            }

        }
        //manejadores de eventos que indican si las teclas fueron soltadas
        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            //si una tecla es soltada "da por finalizado el turno del jugador" y evita que gane usando solo una tecla
            if (e.KeyCode == Keys.Left)
            {
                ataque_agua = false;
             
            }
            if (e.KeyCode == Keys.Right)
            {
                ataque_planta = false;
               
            }
            if (e.KeyCode == Keys.Up)
            {
                ataque_fuego = false;
               
            }
            if (e.KeyCode == Keys.Down)
            {
                ataque_elect = false;
               
            }
        }
    }
}
