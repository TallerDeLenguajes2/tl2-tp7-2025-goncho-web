namespace presupuestoDetalle;

using productos;
public class PresupuestoDetalle
{
    Producto productos;
    int cantidad;
    public PresupuestoDetalle()
    {

    }
    public PresupuestoDetalle(Producto producto, int cantidad)
    {
        this.cantidad = cantidad;
        this.productos = producto;
    }
    public Producto Producto { get => productos; set => productos = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}