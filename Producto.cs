using System;

public class Producto
{
	public int Id { get; set; }
	public string Nombre { get; set; }
	public decimal Precio { get; set; }
	public override string ToString()
	{
		return $"{Id} - {Nombre} - {Precio:C}";
	}
}
