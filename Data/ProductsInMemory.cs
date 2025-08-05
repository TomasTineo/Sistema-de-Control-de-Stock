using Dominio;

namespace Data
{
    public class ProductsInMemory
    {
        public static List<Producto> Productos;

        static ProductsInMemory()
        {
            Productos = new List<Producto>()
            {
                new Producto(1, "Silla plegable", 1500, "Silla plegable de plástico blanca, ideal para eventos al aire libre.", 200),
                new Producto(2, "Mesa redonda", 8000, "Mesa redonda de 1.5m de diámetro, perfecta para banquetes.", 30),
                new Producto(3, "Carpa 3x3m", 25000, "Carpa blanca resistente al agua, fácil de montar.", 10),
                new Producto(4, "Equipo de sonido", 50000, "Equipo de sonido profesional con micrófono inalámbrico.", 5),
                new Producto(5, "Proyector HD", 40000, "Proyector de alta definición para presentaciones y videos.", 4),
                new Producto(6, "Mantel blanco", 1000, "Mantel de tela blanca para mesas rectangulares o redondas.", 100),
                new Producto(7, "Iluminación LED", 15000, "Kit de luces LED de colores para ambientar eventos.", 12),
                new Producto(8, "Escenario modular", 100000, "Escenario modular de 4x6m, altura ajustable.", 2)

            };
        }
    }
}
