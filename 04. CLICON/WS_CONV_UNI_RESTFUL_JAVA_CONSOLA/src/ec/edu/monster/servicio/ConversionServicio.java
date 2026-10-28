package ec.edu.monster.servicio;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Arrays;
import java.util.List;

public class ConversionServicio {

    private final List<String> unidades = Arrays.asList(
            "m","cm","km","in","ft",
            "kg","g","lb","oz","t",
            "C","F","K","R"
    );

    public String convertir(double valor, String origen, String destino) {

        // VALIDACIONES
        if (valor < 0) {
            return "⚠ El valor no puede ser negativo";
        }

        if (!unidades.contains(origen) || !unidades.contains(destino)) {
            return "⚠ Unidad no válida";
        }

        try {
            String ruta = "http://localhost:8080/WS_CONV_UNI_RESTFUL_JAVA/webresources/conversion/"
                    + valor + "/" + origen + "/" + destino;

            URL url = new URL(ruta);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");

            BufferedReader in = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            String resultado = in.readLine();

            in.close();

            return resultado;

        } catch (Exception e) {
            return "❌ Error al conectar con el servidor";
        }
    }
}