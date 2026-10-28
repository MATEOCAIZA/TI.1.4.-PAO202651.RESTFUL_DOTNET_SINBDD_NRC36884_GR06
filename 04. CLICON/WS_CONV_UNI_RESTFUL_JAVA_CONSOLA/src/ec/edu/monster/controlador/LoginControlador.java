package ec.edu.monster.controlador;

import ec.edu.monster.modelo.CredencialesUsuario;
import ec.edu.monster.servicio.LoginServicio;

public class LoginControlador {

    LoginServicio servicio = new LoginServicio();

    public boolean login(String usuario, String clave) {

        CredencialesUsuario credenciales = new CredencialesUsuario(usuario, clave);

        return servicio.login(credenciales);
    }
}