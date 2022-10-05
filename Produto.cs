namespace EstoqueRgm
{
    class Produto
    {
    	public int id { get; set; }
		public string? categoria { get; set; }
    	public string? modelo { get; set; }
		public int quantidade { get; set; }
		
		//TODO data do pedido - está dando erro ao utilizar a api
		//public DateOnly data {get; set;}
    }
}