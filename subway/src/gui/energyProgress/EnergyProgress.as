package gui.energyProgress
{
	import flash.display.Sprite;
	
	import level.Tipo;
	
	public class EnergyProgress
	{
		public var tipo:Tipo;
		
		private var separation:int = 0;
		private var container:Sprite;		
		
		public function EnergyProgress()
		{
			
		}
		public function show():void
		{
			container.visible = true;
		}
		public function hide():void
		{
			container.visible = false;
		}
		public function setEnergy(tipo:Tipo):void
		{
			this.tipo = tipo;
			container = new Sprite();
			tipo.asset.addChild(container);
			container.y = -210;
			
			var qty:int = tipo.tipoManager.inventario.length;
			for (var a:int=0; a<qty;a++)
			{
				var progressBar:ProgressBar = new ProgressBar(this, a);
				container.addChild(progressBar);
				progressBar.y -= (progressBar.height + separation)*a;
			}
			container.x -= (container.width/2);
		}
		public function resta():void
		{
			var i:int = container.numChildren;
			while(i--)
			{
				var progressBar:ProgressBar =  ProgressBar(container.getChildAt(i));
				if(!progressBar.isOff)
				{
					progressBar.resta();
					return;	
				} else
					progressBar.alpha = 0;
			}
		}
		public function resetLast():void
		{
			var i:int = container.numChildren;
			while(i--)
			{
				var progressBar:ProgressBar =  ProgressBar(container.getChildAt(i));
				if(progressBar.bar.scaleX > 0 && progressBar.bar.scaleX < 1 )
				{
					progressBar.reset();
					return;	
				}
			}
		}
		

	}
}