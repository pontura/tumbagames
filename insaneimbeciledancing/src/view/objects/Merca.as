package view.objects
{
	import flash.geom.ColorTransform;

	public class Merca extends SceneObject
	{
		public function Merca()
		{
			mc = new ItemMercaMC
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