package ws_conv_uni_restful_java_consola;

import java.util.Scanner;
import ec.edu.monster.controlador.LoginControlador;
import ec.edu.monster.controlador.ConversionControlador;

public class WS_CONV_UNI_RESTFUL_JAVA_CONSOLA {

    public static void main(String[] args) {

        Scanner sc = new Scanner(System.in);
        LoginControlador loginControlador = new LoginControlador();
        ConversionControlador conversionControlador = new ConversionControlador();

        System.out.println("==================================");
        System.out.println("        LOGIN DEL SISTEMA         ");
        System.out.println("==================================");

        boolean acceso = false;

        // LOGIN CON VALIDACIÓN
        while (!acceso) {

            System.out.print("Usuario: ");
            String usuario = sc.nextLine();

            System.out.print("Contraseña: ");
            String clave = sc.nextLine();

            if (usuario.isEmpty() || clave.isEmpty()) {
                System.out.println("⚠ No puede dejar campos vacíos\n");
                continue;
            }

            if (loginControlador.login(usuario, clave)) {
                acceso = true;
                System.out.println("\n✔ Acceso concedido\n");
            } else {
                System.out.println("\n❌ Usuario o contraseña incorrectos\n");
            }
        }

        // SISTEMA DE CONVERSIONES
        System.out.println("==================================");
        System.out.println("      CONVERSOR DE UNIDADES       ");
        System.out.println("==================================");

        while (true) {

            double valor = 0;

            // VALIDAR NÚMERO
            while (true) {
                System.out.print("\nIngrese valor: ");
                if (sc.hasNextDouble()) {
                    valor = sc.nextDouble();
                    sc.nextLine(); // limpiar buffer
                    break;
                } else {
                    System.out.println("⚠ Debe ingresar un número");
                    sc.next(); // limpiar
                }
            }

            System.out.print("Unidad origen: ");
            String origen = sc.nextLine();

            System.out.print("Unidad destino: ");
            String destino = sc.nextLine();

            if (origen.isEmpty() || destino.isEmpty()) {
                System.out.println("⚠ Las unidades no pueden estar vacías");
                continue;
            }

            String resultado = conversionControlador.convertir(valor, origen, destino);

            System.out.println("Resultado: " + resultado);

            System.out.print("\n¿Desea continuar? (s/n): ");
            String op = sc.nextLine();

            if (op.equalsIgnoreCase("n")) {
                break;
            }
        }

        System.out.println("Programa finalizado.");
    }
}