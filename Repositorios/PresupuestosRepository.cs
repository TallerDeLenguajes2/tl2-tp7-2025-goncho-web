using System.Globalization;
using System.Linq;
using Microsoft.Data.Sqlite;

public class PresupuestosRepository{
    string connectionString = "Data Source=Tienda_final.db;";

    public void CrearPresupuesto(Presupuesto presupuesto){

        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        string query= "INSERT INTO Presupuesto(nombreDestinatario,fechaCreacion) VALUES(@nombreDestinatario,@fechaCreacion)";
        using var command = new SqliteCommand(query,connection);

        command.Parameters.Add(new SqliteParameter("@nombreDestinatario", productos.nombreDestinatario));
        command.Parameters.Add(new SqliteParameter("@fechaCreacion", presupuesto.fechaCreacion));
        return command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Presupuesto> ListaDePresupuesto(){
        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        var ListaPresupuestos = new List<Presupuestos>();
        string query = "SELECT * FROM Presupuesto";
        using var command = new SqliteCommand(query,connection);

        using var lector = comando.ExecuteReader();

        while (lector.Read()){
            var presupuesto = new Presupuesto(
                    IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                    NombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                );
            ListaPresupuestos.Add(producto);
        }
        connection.Close();
        return ListaPresupuestos;
    }
    
    public Presupuesto GetPresupuestoById(int idPresupuesto){
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();

        string query = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Presupuesto
            {
                idPresupuesto = (reader["idPresupuesto"] != DBNull.Value) ? Convert.ToInt32(reader["idPresupuesto"]) : 0,
                nombreDestinatario = (reader["NombreDestinatario"] != DBNull.Value) ? reader["NombreDestinatario"].ToString() : string.Empty,
                fechaCreacion = (reader["FechaCreacion"] != DBNull.Value) ? Convert.ToDateTime(reader["FechaCreacion"]) : DateTime.MinValue
            };
        }
        return null;       
    }

    public void AgregarProducto(int idPresupuesto, PresupuestoDetalle detalle){
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();

        var presupuesto = new PresupuestoRepository().GetPresupuestoById(idPresupuesto);
        if (presupuesto == null) return false;

        string query = "INSERT INTO PresupuestosDetalle (idProducto,idPresupuesto,Cantidad) VALUES (@idProd,@idPres,@cantidad)"; 
            
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@idProd",detalle.producto.id));
        command.Parameters.Add(new SqliteParameter("@idPres",idPresupuesto));
        command.Parameters.Add(new SqliteParameter("@cantidad",detalle.cantidad));
        command.ExecuteNonQuery();
        connection.Close();
    }

     public bool EliminarPresupuesto(int idPresupuesto){
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();

        var presupuesto = new PresupuestoRepository().GetPresupuestoById(idPresupuesto);
        if (presupuesto == null) return false;

        string query0 = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
        using var command0 = new SqliteCommand(query0, connection);
        command0.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        if(command0.ExecuteNonQuery() > 0){
            string query = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
            using var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));
            return command.ExecuteNonQuery() > 0;
        }
        return command0.ExecuteNonQuery()>0;
    }
}