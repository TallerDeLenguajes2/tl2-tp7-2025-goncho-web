using System.Globalization;
using System.Linq;
using Microsoft.Data.Sqlite;
public class ProductosRepository
{
    string connectionString = "Data Source=Tienda_final.db;";
    public void CrearProducto(Producto productos){

        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        string query= "INSERT INTO Productos(descripcion,precio) VALUES(@descripcion,@precio)";
        using var command = new SqliteCommand(query,connection);
        command.Parameters.Add(new SqliteParameter("@descripcion", productos.descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", productos.precio));
        return command.ExecuteNonQuery();

        connection.Close();
    }
    
    public void ModificarProducto(int id, Producto productos){
        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        string query = "UPDATE Productos SET descripcion = @descripcion, precio = @precio WHERE idProducto = @id";
        using var command = new SqliteCommand(query,connection);
        command.Parameters.Add(new SqliteCommand("@descripcion",productos.descripcion));
        command.Parameters.Add(new SqliteCommand("@precio",productos.precio));
        command.Parameters.Add(new SqliteCommand("@idProducto",id));   
        return command.ExecuteNonQuery();

        connection.Close();
    }

    public List<Producto> ListaDeProductos(){
        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        string query = "SELECT * FROM Productos";
        using var command = new SqliteCommand(query,connection);
        using var lector = comando.ExecuteReader();
        while (lector.Read()){
            var producto = new Productos(
                    Convert.ToInt32(reader["idProducto"]),
                    reader["Descripcion"].ToString(),
                    Convert.ToInt32(reader["Precio"])
                );
            productos.Add(producto);
        }
        connection.Close();
        return productos;
    }

    public Producto DetalleProductos(int idProducto){
        using(SqlConnection connection = new SqlConnection(connectionString));
        connection.Open();

        string query = "SELECT * FROM Productos WHERE idProducto = @id";
        using var command = new SqliteCommand(query, connection); //using para eivtar que queden referencias activas 
        command.Parameters.Add(new SqliteParameter("@id", idProducto));
        using SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            var producto = new Producto
            {
                id = Convert.ToInt32(reader["idProducto"]),
                descripcion = reader["descripcion"].ToString(),
                precio = Convert.ToInt32(reader["precio"])
            };
            connection.Close();
            return producto;
        }
        connection.Close();
        return null;
    }

    public void EliminarProducto(int idProducto){
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();
        string query = "DELETE FROM Productos WHERE idProducto = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@id", idProducto));
        command.ExecuteNonQuery();
        connection.Close();
    }
}