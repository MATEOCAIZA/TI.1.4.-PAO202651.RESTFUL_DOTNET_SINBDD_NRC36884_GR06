package ec.edu.monster.modelo;

public class CredencialesUsuario {

    private String usuario;
    private String clave;

    public CredencialesUsuario(String usuario, String clave) {
        this.usuario = usuario;
        this.clave = clave;
    }

    public String getUsuario() {
        return usuario;
    }

    public String getClave() {
        return clave;
    }
}