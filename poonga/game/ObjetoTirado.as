package game
{
	import assets.ObjetoTirado;
	
	public class ObjetoTirado extends assets.ObjetoTirado
	{
		function ObjetoTirado(){
			this.objeto.gotoAndStop(Math.round(Math.random()*4)+1)
		}
	}
}