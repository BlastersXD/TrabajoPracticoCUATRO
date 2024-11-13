using System;
using System.Collections.Generic;

public class vuelo
{
    public string CodigoVuelo { get; set; }
    public DateTime FechaHoraSalida { get; set; }
    public DateTime FechaHoraLlegada { get; set; }
    public int CapacidadMaxima { get; set; }
    public int PasajerosActuales { get; set; }
    public List<string> TripulantesCabina { get; set; } = new List<string>();

    // Método para calcular la ocupación
    public double CalcularOcupacion()
    {
        if (CapacidadMaxima == 0) return 0;
        return (double)PasajerosActuales / CapacidadMaxima * 100;
    }
}
