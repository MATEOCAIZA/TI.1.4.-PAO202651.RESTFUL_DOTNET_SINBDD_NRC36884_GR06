package ec.edu.monster.controlador;

import ec.edu.monster.servicio.ConversionServicio;

public class ConversionControlador {

    ConversionServicio servicio = new ConversionServicio();

    public String convertir(double valor, String origen, String destino) {
        return servicio.convertir(valor, origen, destino);
    }
}