using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class CODESKY
{
    public string RazonSocial { get; set; }
    public string Telefono { get; set; }
    public string Domicilio { get; set; }
    public List<vuelo> ListaDeVuelos { get; set; } = new List<vuelo>();

    // Agregar vuelo
    public void AgregarVuelo(vuelo vuelo)
    {
        ListaDeVuelos.Add(vuelo);
    }

    // Buscar vuelo por código
    public vuelo BuscarVuelo(string codigoVuelo)
    {
        return ListaDeVuelos.Find(v => v.CodigoVuelo == codigoVuelo);
    }

    // Calcular ocupación media
    public double CalcularOcupacionMedia()
    {
        if (ListaDeVuelos.Count == 0) return 0;
        double ocupacionTotal = 0;
        foreach (var vuelo in ListaDeVuelos)
        {
            ocupacionTotal += vuelo.CalcularOcupacion();
        }
        return ocupacionTotal / ListaDeVuelos.Count;
    }

    // Vuelo con mayor ocupación
    public vuelo VueloConMayorOcupacion()
    {
        return ListaDeVuelos.Count > 0 ? ListaDeVuelos.OrderByDescending(v => v.CalcularOcupacion()).First() : null;
    }

    // Listar vuelos ordenados por ocupación
    public List<vuelo> ListarVuelosOrdenadosPorOcupacion()
    {
        return new List<vuelo>(ListaDeVuelos.OrderByDescending(v => v.CalcularOcupacion()));
    }

    // Guardar datos en archivo XML
    public void GuardarDatos(string rutaArchivo)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<vuelo>));
        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            serializer.Serialize(writer, ListaDeVuelos);
        }
    }

    // Cargar datos desde archivo XML
    public void CargarDatos(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<vuelo>));
            using (StreamReader reader = new StreamReader(rutaArchivo))
            {
                ListaDeVuelos = (List<vuelo>)serializer.Deserialize(reader);
            }
        }
    }
}
