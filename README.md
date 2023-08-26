2)  
a_1
Relacion Pedidos-Clientes: es una relacion de composicion. Cliente depende de Pedidos. Si el pedido se elimina, el cliente desaparece.

Relacion Pedidos-Cadete: Es una relacion de agregacion. Si se elimina el pedido se elimina el cadete no se elimina. Si se elimina el cadete, el pedido puede ser asignado.

Relacion Cadete-Cadeteria: es una relacion de composicion. El cadete depende de la cadeteria. Si la cadeteria se elimina ,se eliminan todos los cadetes.

a_2

-Metodos Cadeteria:
CrearPedido
AsignarPedido
ReasignacionPedido

-Metodos Cadete:
EliminarPedido

a_3
Cliente:
Nombre PRIVATE
Direccion PRIVATE
Telefono PRIVATE

