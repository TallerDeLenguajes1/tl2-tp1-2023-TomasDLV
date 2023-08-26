namespace tp1
{
    class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes;
        private int nroPedidosCreados;

        internal List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }

        public Cadeteria(string archivoInfoCadeteria, string archivoCadetes)
        {
            CargarInfoCadeteria(archivoInfoCadeteria);
            CargarCadetes(archivoCadetes);
            this.NroPedidosCreados = 0;
        }

        private void CargarInfoCadeteria(string archivoInfoCadeteria)
        {
            try
            {
                using (StreamReader reader = new StreamReader(archivoInfoCadeteria))
                {
                    string[] datos = reader.ReadLine().Split(',');
                    nombre = datos[0];
                    telefono = int.Parse(datos[1]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar la información de la cadetería: " + ex.Message);
            }
        }
        private void CargarCadetes(string archivoCadetes)
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
                Cadete cadeteAleatorio = ListaCadetes[indiceAleatorio];

                Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido necesito AGREGAR OBS
                NroPedidosCreados += 1;

                cadeteAleatorio.AgregarPedido(nuevoPedido); // Agrega el pedido a la lista de pedidos del cadete

                Console.WriteLine("Pedido asignado al cadete: " + cadeteAleatorio.Nombre);
            }
            else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
            }
        }


        public void ReasignarPedido(int idPedido, int nuevoIdCadete) //Asignar a un cadete en particular o random?
        {
            Cadete nuevoCadete = listaCadetes.FirstOrDefault(cadete => cadete.Id == nuevoIdCadete);

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

        public void EntregarPedido(int idPedido)
        {
            foreach (Cadete cadete in ListaCadetes)
            {
                for (int i = 0; i < cadete.ListaPedidos.Count; i++)
                {
                    if (idPedido == cadete.ListaPedidos[i].Nro)
                    {
                        cadete.ListaPedidos[i].Estado = "Entregado";
                        Console.WriteLine("El pedido nro " + idPedido + " ha sido entregado.");
                        return;
                    }
                }
            }

        }
    }
}