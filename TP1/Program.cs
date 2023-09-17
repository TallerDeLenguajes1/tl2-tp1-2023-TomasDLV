using tp1;
class Program
{
    static void Main()
    {
        Console.Clear();
        Cadeteria cadeteria = null; // Inicialia0a la Cadeteria con los archivos
        bool bandera = true;
        do
        {
            Console.WriteLine("Ingrese la opcion para cargar Cadeteria");
            Console.WriteLine("Seleccione: 1: CSV | 2: JSON");
            string input = Console.ReadLine();
            if (input == "1")
            {
                AccesoADatos accesoA = new AccesoCsv();
                cadeteria = accesoA.CargarInfoCadeteria();
                
                bandera = false;

            }
            else
            {
                if (input == "2")
                {
                    AccesoADatos accesoA = new AccesoJson();
                    cadeteria = accesoA.CargarInfoCadeteria();
                    bandera = false;
                }
            }
        } while (bandera);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Gestión de Pedidos ====");
            Console.WriteLine("1) Crear pedido");
            Console.WriteLine("2) Eliminar un pedido");
            Console.WriteLine("3) Asignar pedidos a cadetes");
            Console.WriteLine("4) Cambiar estado de pedidos");
            Console.WriteLine("5) Jornal a cobrar por cadete");
            Console.WriteLine("6) Mostrar informe de pedidos");
            Console.WriteLine("7) Salir");

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion.ToLower())
            {
                case "1":
                    // Lógica para dar de alta pedidos
                    cadeteria.CrearPedido();
                    Console.ReadKey();
                    break;
                case "2":
                    // Lógica para dar de alta pedidos
                    Console.WriteLine("Ingrese el id del pedido a realizar");
                    string inputId = Console.ReadLine();
                    int.TryParse(inputId, out int id);
                    cadeteria.EliminarPedido(id);
                    Console.ReadKey();
                    break;
                case "3":
                    // Lógica para asignar pedidos a cadetes
                    Console.WriteLine("Ingrese el id del cadete a asignar");
                    string inputIdCadete = Console.ReadLine();
                    int.TryParse(inputIdCadete, out int idCadete);
                    Console.WriteLine("Ingrese el nro del pedido a asignar");
                    string inputIdPedido = Console.ReadLine();
                    int.TryParse(inputIdPedido, out int idPedido);
                    Console.WriteLine("Estamos asignando el pedido a un cadete disponible...");
                    cadeteria.AsignarCadeteAPedido(idCadete, idPedido);
                    Console.WriteLine("");
                    Console.ReadKey();
                    break;
                case "4":
                    // Lógica para cambiar estado de pedidos
                    Console.WriteLine("");
                    cadeteria.CambiarEstado();
                    Console.ReadKey();
                    break;
                case "5":
                    // Lógica para reasignar pedido a otro cadete
                    Console.WriteLine("");
                    Console.WriteLine("Ingrese el id del cadete a cobrar");
                    string inputIdCadeteCobro = Console.ReadLine();
                    int.TryParse(inputIdCadeteCobro, out int idCadeteCobro);

                    Console.WriteLine("El cadete cobrará : $" + cadeteria.JornalACobrar(idCadeteCobro) + " .");
                    Console.ReadKey();
                    break;
                case "6":
                    // Lógica para mostrar informe de pedidos
                    Console.WriteLine("");
                    Console.WriteLine("Aqui tienes el informe:");
                    Informe informe = new Informe(cadeteria);
                    informe.MostrarInforme();
                    Console.ReadKey();
                    break;
                case "7":
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

