package view.objects
{
	import flash.display.MovieClip;
	import flash.geom.ColorTransform;

	public class Bomb extends SceneObject
	{
		public function Bomb()
		{
			mc = new ItemBombMC
			addChild(mc)
			setOff();
		}
		public override function setAction (action:String):void
		{
			if(action == "collisioned")
			{
				//Game.I.show.rewind();
				mc.visible = false;
				var explotion:MovieClip = new ExplosionMC();
				addChild(explotion);
				explotion.scaleX = 2;
				explotion.scaleY = 2;
			}
		}
		private function setOff():void
		{
			
		}
	}
}