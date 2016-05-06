package view.objects
{
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;

	public class Player extends SceneObject
	{
		public function Player()
		{
			mc = new CharacterMC
			addChild(mc)
		}
		public override function collisioned(so:SceneObject):void
		{
			if(so is Boss)
			{
				Boss(so).hit();
				catched();
			}
		}
		public function birra():void
		{
			state = "birra";
			mc.gotoAndPlay("birra");
		}
		public function merca():void
		{
			mc.scaleX = 1;
			state = "merca";
			mc.gotoAndPlay("merca");
		}
		public function jumpMerca():void
		{
			state = "jumpMerca";
			mc.gotoAndPlay("jumpMerca");
		}
		private function catched():void
		{
			if(state == "catched") 
				return;
			canMove = false;
			state = "catched";
			addTask(
				new Sequence(
					new Func(hitted),
					new Wait(500),
					new Func(resetHitted)
					)
			)
		}
		private function hitted():void
		{
			mc.gotoAndPlay("catched");
		}
		private function resetHitted():void
		{
			canMove = true;
			idle();
			//Game.I.pause();
		}
		public override function setAction (type:String):void
		{
			state = type;
			switch(type){
				case "idle": mc.gotoAndPlay("idle"); break;
			}
		}
	}
}