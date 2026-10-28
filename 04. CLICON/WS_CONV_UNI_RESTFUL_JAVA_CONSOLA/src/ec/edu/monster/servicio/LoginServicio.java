package ec.edu.monster.servicio;

import ec.edu.monster.modelo.CredencialesUsuario;

public class LoginServicio {

    public boolean login(CredencialesUsuario credenciales) {

        if (credenciales.getUsuario() == null || credenciales.getUsuario().isEmpty()) {
            System.out.println("⚠ Usuario vacío");
            return false;
        }

        if (credenciales.getClave() == null || credenciales.getClave().isEmpty()) {
            System.out.println("⚠ Contraseña vacía");
            return false;
        }

        return credenciales.getUsuario().equals("MONSTER")
                && credenciales.getClave().equals("MONSTER69");
    }
}