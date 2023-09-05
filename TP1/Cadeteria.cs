namespace tp1
{
    class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes = new List<Cadete>();
        private int nroPedidosCreados;
        private List<Pedidos> listaPedidos = new List<Pedidos>();

        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }
        public string Nombre { get => nombre; }
        public int Telefono { get => telefono; }

        public Cadeteria(string archivoInfoCadeteria, string archivoCadetes)
        {
            CargarInfoCadeteria(archivoInfoCadeteria);
            CargarCadetes(archivoCadetes);
        }

        private void CargarInfoCadeteria(string archivoInfoCadeteria)
        {
            try
            {
                using (StreamReader reader = new StreamReader(archivoInfoCadeteria))
                {
                    string[] datos = reader.ReadLine().Split(',');
                    this.nombre = datos[0];
                    this.telefono = int.Parse(datos[1]);
                    NroPedidosCreados = int.Parse(datos[2]); // Carga el valor de nroPedidosCreados

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar la información de la cadetería: " + ex.Message);
            }
        }
        public void CargarCadetes(string archivoCadetes)
        {
            try
            {
                using (StreamReader reader = new StreamReader(archivoCadetes))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] datosCadete = line.Split(',');
                        int id = int.Parse(datosCadete[0]);
                        string nombre = datosCadete[1];
                        string direccion = datosCadete[2];
                        int telefono = int.Parse(datosCadete[3]);

                        Cadete cadete = new Cadete(id, nombre, direccion, telefono);
                        ListaCadetes.Add(cadete);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar la lista de cadetes: " + ex.Message);
            }
        }
        public void CrearPedido(){
            Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
            NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados
            listaPedidos.Add(nuevoPedido);
            Console.WriteLine("Se creo el pedido nro: "+nuevoPedido.Nro+ " y se lo agrego a la lista");
        }

        public void AsignarCadeteAPedido(int idCadete,int idPedido)
        {
            Cadete cadeteBuscado = listaCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadeteBuscado != null)
            {
                Pedidos pedidoBuscado = listaPedidos.FirstOrDefault(pedido =>pedido.Nro == idPedido);
                if (pedidoBuscado != null )
                {
                    if (pedidoBuscado.IdCadeteEncargado == null)
                    {
                        pedidoBuscado.IdCadeteEncargado = idCadete;
                        Console.WriteLine("Pedido nro "+ idPedido +" asignado al cadete: " + cadeteBuscado.Nombre);
                    }else
                    {
                        Console.WriteLine("El pedido ya tiene un cadete encargado.");
                        Console.WriteLine("¿Quieres reemplazarlo? SI: 1 | NO: 0");
                        if (Console.ReadLine()== "1")
                        {
                            pedidoBuscado.IdCadeteEncargado = idCadete;
                            Console.WriteLine("Pedido nro "+ idPedido +" asignado al cadete: " + cadeteBuscado.Nombre);
                        }else
                        {
                            Console.WriteLine("La asignacion de Pedido no fue concretada");
                        }
                    }
                    
                }else
                {
                    Console.WriteLine("El pedido que ingresaste no se encontro en la lista de pedidos");
                }

                
            }
            else
            {
                Console.WriteLine("No se encontro el cadete que ingresaste");
            }
        }

        /* No necesito una reasignacion de pedido si ya tengo una asignacionDePedido que si existe un cadete lo Reasignaria por otro


        public void ReasignarPedido(int idPedido, int nuevoIdCadete) // Asignar a un cadete en particular o random?
        {
            Cadete nuevoCadete = ListaCadetes.FirstOrDefault(cadete => cadete.Id == nuevoIdCadete); // DEBO SOLUCIONAR QUE PUEDA TOMAR VALORES NULL?

            if (nuevoCadete != null)
            {
                foreach (Cadete cadete in listaCadetes)
                {
                    Pedidos pedidoAReasignar = cadete.ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);

                    if (pedidoAReasignar != null)
                    {
                        cadete.ListaPedidos.Remove(pedidoAReasignar);
                        nuevoCadete.ListaPedidos.Add(pedidoAReasignar);
                        Console.WriteLine("Pedido reasignado al cadete: " + nuevoCadete.Nombre);
                        return; // Salimos del ciclo una vez encontrado y reasignado el pedido
                    }
                }
                Console.WriteLine("Pedido no encontrado en la lista de pedidos de ningún cadete.");
            }
            else
            {
                Console.WriteLine("Cadete no encontrado.");
            }
        }*/

        public void CambiarEstado() // Este metodo recibe por parametro la id del pedido a entregar, busca que cadete lo posee y lo cambia de estado
        {

            Console.WriteLine("Ingrese el ID del pedido a cambiar de estado: ");
            
            int.TryParse(Console.ReadLine(), out int idPedido);
            Pedidos pedidoEncontrado = listaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);

            if (pedidoEncontrado != null)
            {
                Console.WriteLine("Seleccione el estado al que cambiar:");
                Console.WriteLine("a) Pendiente");
                Console.WriteLine("b) En Camino");
                Console.WriteLine("c) Entregado");

                Console.Write("Opción: ");
                string opcionEstado = Console.ReadLine();

                string nuevoEstado = "";

                switch (opcionEstado.ToLower())
                {
                    case "a":
                        nuevoEstado = "Pendiente";
                        break;
                    case "b":
                        nuevoEstado = "EnCamino";
                        break;
                    case "c":
                        nuevoEstado = "Entregado";
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        return;
                }
                pedidoEncontrado.Estado = nuevoEstado;
                
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void AltaPedido(int idPedido){ // Esta funcion da de alta un pedio por una id recibida
            Pedidos pedidoEncontrado = listaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
            if (pedidoEncontrado != null)
            {
                listaPedidos.Remove(pedidoEncontrado);
                Console.WriteLine("Se elimino el Pedido "+ pedidoEncontrado.Nro + " exitosamente");
            }else
            {
                Console.WriteLine("No se encontro el pedido para dar de alta");
            }
            Console.WriteLine("No se encontro el pedido " + idPedido + ".");
        }
        public double JornalACobrar(int idCadete) {
            double cantPedidosEntregados = 0;
            foreach (Pedidos pedido in listaPedidos)
            {
                if (pedido.IdCadeteEncargado == idCadete && pedido.Estado == "Entregado")
                {
                    cantPedidosEntregados++;
                }
            }
            return 500 * cantPedidosEntregados;
        }
    }
}