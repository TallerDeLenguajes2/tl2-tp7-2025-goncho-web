using presupuestoDetalle;
using productos;

class Presupuesto
{
    int idPresupuesto;
    string nombreDestinatario;
    DateTime fechaCreacion;
    List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }
    
     public Presupuesto(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion)
    {
        IdPresupuesto = idPresupuesto;
        NombreDestinatario = nombreDestinatario;
        FechaCreacion = fechaCreacion;
        detalle = new List<PresupuestoDetalle>();

    }

    public double montoPresupuesto()
    {
        int monto = detalle.Sum(d => d.Cantidad * d.Producto.Precio);
        return monto;
    }

    public double montoPresupuestoConIVA()
    {
        return montoPresupuesto() * 0.21;
    }

    public int cantidadProductos()
    {
        return detalle.Sum(d => d.Cantidad);
    }

}