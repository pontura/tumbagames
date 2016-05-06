package view.objects
{
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;

	public class Boss extends SceneObject
	{
		public function Boss()
		{
			mc = new BossMC;			
			addChild(mc);
			idle();
			canMove = false;
		}
		public function jump():void
		{
			if(state!="jump")
				mc.gotoAndStop("jump");
			state = "jump";
		}
		public function hit():void
		{
			Game.I._ui.lose(0.2);
			addTask(
				new Sequence(
					new Func(hitting),
					new Wait(1000),
					new Func(resetHitting)
				)
			)
		}
		private function hitting():void
		{			
			if(state!="hit")
			{
				canMove = false;
				mc.gotoAndStop("hit");	
				state = "hit";
			}
			/*if(Game.I.virtualView.player.GetPosition().x > Game.I.virtualView.boss.GetPosition().x)
				lookAt(true) else lookAt(false);*/			
		}
		private function resetHitting():void
		{
			canMove = true;
			jump();
		}
		public override function setAction (type:String):void
		{
			switch(type){
				case "idle": mc.gotoAndStop("idle"); break;
			}
		}
	}
}