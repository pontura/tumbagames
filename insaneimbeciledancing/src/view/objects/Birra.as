package view.objects
{
	import flash.geom.ColorTransform;

	public class Birra extends SceneObject
	{
		public function Birra()
		{
			mc = new ItemBirraMC
			addChild(mc)
			setOff();
		}
		public override function setAction (action:String):void
		{
			var c:ColorTransform
			if(action == "collisioned")
			{
			}
		}
		private function setOff():void
		{
			
		}
	}
}