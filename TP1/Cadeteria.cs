namespace tp1
{
    class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes = new List<Cadete>();
        private int nroPedidosCreados;

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

        public void AsignarPedido()
        {
            if (ListaCadetes.Count > 0)
            {
                Random random = new Random();
                int indiceAleatorio = random.Next(0, ListaCadetes.Count);
                Cadete cadeteAleatorio = ListaCadetes[indiceAleatorio]; // Elijo un cadete de manera aleatoria

                Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
                NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados

                cadeteAleatorio.AgregarPedido(nuevoPedido); // Agrega el pedido a la lista de pedidos del cadete Elegido

                Console.WriteLine("Pedido nro "+nuevoPedido.Nro+" asignado al cadete: " + cadeteAleatorio.Nombre);
            }
            else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
            }
        }


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
        }

        public void CambiarEstado() // Este metodo recibe por parametro la id del pedido a entregar, busca que cadete lo posee y lo cambia de estado
        {

            Console.WriteLine("Ingrese el ID del pedido a cambiar de estado: ");
            if (int.TryParse(Console.ReadLine(), out int idPedido))
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

                foreach (Cadete cadete in ListaCadetes)
                {
                    for (int i = 0; i < cadete.ListaPedidos.Count; i++)
                    {
                        if (idPedido == cadete.ListaPedidos[i].Nro)
                        {
                            cadete.ListaPedidos[i].Estado = nuevoEstado;
                            Console.WriteLine("El pedido nro " + idPedido + " cambio de estado a : " + nuevoEstado);
                            return;
                        }
                    }
                }
                // Aquí puedes llamar al método en la Cadeteria para cambiar el estado del pedido con "idPedido" al "nuevoEstado"
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void AltaPedido(int idPedido){ // Esta funcion da de alta un pedio por una id recibida
            foreach (Cadete cadete in ListaCadetes)
            {
                var pedidoAlta = cadete.ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoAlta != null)
                {
                    cadete.ListaPedidos.Remove(pedidoAlta);
                    Console.WriteLine("El pedido " + idPedido + " ha sido dado de alta correctamente");
                    return;
                }
            }
            Console.WriteLine("No se encontro el pedido " + idPedido + ".");
        }

    }

}