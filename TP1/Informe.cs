namespace tp1
{
    class Informe
    {
        private int montoTotalGanado;
        private int totalEnvios;
        private double promedioEnviosXCadete;
        private Cadeteria cadeteria = null;
        public int MontoTotalGanado { get => montoTotalGanado; set => montoTotalGanado = value; }
        public int TotalEnvios { get => totalEnvios; set => totalEnvios = value; }
        public double PromedioEnviosXCadete { get => promedioEnviosXCadete; set => promedioEnviosXCadete = value; }
        public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }

        public Informe(Cadeteria cadeteria)
        {
            Cadeteria = cadeteria;
            foreach (Pedidos pedido in Cadeteria.ListaPedidos)
            {
                if (pedido.Estado == "Entregado")
                {
                    totalEnvios++;
                }
            }
            MontoTotalGanado = TotalEnvios*500;
            if (Cadeteria.ListaCadetes != null)
            {
                PromedioEnviosXCadete = TotalEnvios/Cadeteria.ListaCadetes.Count;
            }
        }

        

        public void MostrarInforme(){
            Console.WriteLine("--------Informe de la Cadeteria "+ Cadeteria.Nombre + "--------");
            Console.WriteLine("Cantidad de Envios REALIZADOS: "+ TotalEnvios);
            Console.WriteLine("Promedio de Envios por cadete: "+PromedioEnviosXCadete);
            Console.WriteLine("Monto total generado: "+ MontoTotalGanado);
            Console.WriteLine("");
            Console.WriteLine("*INFORMACION POR CADETES");

            Console.WriteLine(Cadeteria.ListaCadetes[0].Nombre);
            
            
            foreach (Cadete cadete in Cadeteria.ListaCadetes)
            {
                Console.WriteLine("");
                Console.WriteLine("[Cadete "+cadete.Nombre+"]");
                int cantPedidos = 0;
                foreach (Pedidos pedido in Cadeteria.ListaPedidos)
                {
                    if (pedido.Estado =="Entregado")
                    {
                        cantPedidos ++;
                    }
                }
                Console.WriteLine("Cantidad de Pedidos Entregados: "+ cantPedidos);
                Console.Write("Nros de Pedidos EnCamino:");
                foreach (Pedidos pedido in Cadeteria.ListaPedidos)
                {
                    if (pedido.Estado == "EnCamino")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
                Console.WriteLine("");
                Console.Write("Nros de Pedidos Entregados:");
                foreach (Pedidos pedido in Cadeteria.ListaPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
            }
        }
    }
    
}