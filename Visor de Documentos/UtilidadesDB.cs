using System;
using System.Linq;

public static class UtilidadesDB
{
    //Aquí se lee la función para leer la cadena de conexión
    //Esta es la que hay que llamar para el código que no usa Entity Framework
    public static string CadenaConexion(string NombreCadena)
    {
        return Environment.GetEnvironmentVariable(
                   NombreCadena,
                   EnvironmentVariableTarget.Machine
               );
    }

    //Esta función se encarga de convertir la cadena de conexión "normal" en la que usa Entity Framework
    //esta nunca se manda a llamar directamente
    private static string ConvertirAEntityFramework(string connectionString, string nombreModelo)
    {
        var parametros = connectionString.Split(';')
            .Where(parte => !string.IsNullOrWhiteSpace(parte) && parte.Contains('='))
            .Select(parte => parte.Split('='))
            .ToDictionary(parte => parte[0].Trim(), parte => parte[1].Trim());

        string server = parametros["Server"];
        string database = parametros["Database"];
        string userId = parametros["User Id"];
        string password = parametros["Password"];

        string providerConnString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};Encrypt=True;TrustServerCertificate=True", server, database, userId, password);

        return string.Format(@"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string=""{1}""", nombreModelo, providerConnString);
    }


    //Esta es la que hay que mandar a llamar para las cadenas de Entity Framework

    public static string CadenaConexionLinq(string nombreCadena, string carpetaModelo)
    {
        string original = CadenaConexion(nombreCadena);
        string efConnectionString = ConvertirAEntityFramework(original, carpetaModelo);

        return efConnectionString;
    }

}

