package gui.energyProgress
{
	import globals.audio;

	public class ProgressBar extends progressMC
	{
		public var isOff:Boolean; 
		public var inventory_id:int;
		private var energyProgress:EnergyProgress;
		
		public function ProgressBar(energyProgress:EnergyProgress, inventory_id:int)
		{
			this.inventory_id = inventory_id;
			this.energyProgress = energyProgress;
		}
		public function resta():void
		{
			if(this.bar.scaleX == 1)
			{
				audio.play("sfx_punguear");
			}
			if(this.bar.scaleX <= settings.afanoSpeed)
			{
				energyProgress.tipo.pierdeRopa(inventory_id);
				this.bar.scaleX = 0;
				audio.stop("sfx_punguear");
				isOff = true;
				return;
			}
			this.bar.scaleX-=settings.afanoSpeed;				
		}
		public function reset():void
		{
			this.bar.scaleX = 1;
		}

	}
}