namespace EstoqueRgm
{
    class ControleEstoque
    {
    	public int id  { get; set; }
		public string? categoria  { get; set; }
    	public string? modelo  { get; set; }
		public int quantidadeTotal  { get; set; }

        // public void atualizaControle (string categoria, string modelo, int quantidade){
        // this.id = id;
        // this.categoria = categoria;
        // this.modelo = modelo;
        // this.quantidadeTotal = quantidade;
        // }

        public void atualizaEstoque(int qdt){
            quantidadeTotal = qdt;

            // var controleDeEstoque = baseProdutos.Produtos.Find(id);			
			// cadastro.quantidadeTotal = produtoAtualizado.quantidade;
        }
    }
}

