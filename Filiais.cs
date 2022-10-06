namespace EstoqueRgm
{
    class Filial
    {
    	public int id { get; set; }
        public string? nomeFilial { get; set; }
		public string? categoria { get; set; }
    	public string? modelo { get; set; }
		public int quantidade { get; set; }
        public DateTime dataHora;
    }
}