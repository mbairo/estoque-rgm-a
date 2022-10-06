using System;
using System.Collections.Generic;

namespace EstoqueRgm
{
    class Produto
    {
    	public int id { get; set; }
		
		//public int idProduto { get; set; }
		//public int idFilial { get; set; }

		public string? categoria { get; set; }
    	public string? modelo { get; set; }
		public int quantidade { get; set; }
		public DateTime dataHora { get; set; }
		
		//TODO data do pedido - está dando erro ao utilizar a api
		//public DateOnly data {get; set;}  or public DateTime dataHora { get; set; }= DateTime.Now
    }
}