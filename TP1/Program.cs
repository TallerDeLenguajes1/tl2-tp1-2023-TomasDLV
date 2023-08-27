namespace tp1
{
    class Program
    {
        static void Main()
        {
            Cadeteria cadeteria = new Cadeteria("cadeteria.csv", "cadetes.csv"); // Inicialia0a la Cadeteria con los archivos
            Console.WriteLine(cadeteria.ListaCadetes[0].Nombre);
            while (true)
            {
                Console.WriteLine("==== Gestión de Pedidos ====");
                Console.WriteLine("a) Dar de alta pedidos");
                Console.WriteLine("b) Asignar pedidos a cadetes");
                Console.WriteLine("c) Cambiar estado de pedidos");
                Console.WriteLine("d) Reasignar pedido a otro cadete");
                Console.WriteLine("e) Mostrar informe de pedidos");
                Console.WriteLine("x) Salir");

                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion.ToLower())
                {
                    case "a":
                        // Lógica para dar de alta pedidos
                        Console.WriteLine("Ingrese el id del pedido a realizar");
                        string inputId = Console.ReadLine();
                        int.TryParse(inputId, out int id);
                        cadeteria.AltaPedido(id);
                        break;
                    case "b":
                        // Lógica para asignar pedidos a cadetes
                        Console.WriteLine("Estamos asignando el pedido a un cadete disponible...");
                        cadeteria.AsignarPedido();
                        break;
                    case "c":
                        // Lógica para cambiar estado de pedidos
                        break;
                    case "d":
                        // Lógica para reasignar pedido a otro cadete
                        Console.WriteLine("Ingrese el id del pedido a reasignar");
                        string inputIdPedido = Console.ReadLine();
                        int.TryParse(inputIdPedido, out int idPedido);
                        Console.WriteLine("Ingrese el id del nuevo Cadete");
                        string inputIdNuevoCadete = Console.ReadLine();
                        int.TryParse(inputIdNuevoCadete, out int idNuevoCadete);

                        cadeteria.ReasignarPedido(idPedido,idNuevoCadete);
                        break;
                    case "e":
                        // Lógica para mostrar informe de pedidos
                        break;
                    case "x":
                        Console.WriteLine("Saliendo del programa.");
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine(); // Separador entre iteraciones
            }
        }
    }
}
