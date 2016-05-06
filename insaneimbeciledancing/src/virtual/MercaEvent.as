package virtual
{
	import Box2D.Collision.Shapes.b2CircleShape;
	import Box2D.Collision.Shapes.b2MassData;
	import Box2D.Collision.Shapes.b2Shape;
	import Box2D.Dynamics.b2Fixture;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;
	
	import view.objects.Player;

	public class MercaEvent
	{
		private var fixture:b2Fixture
		public function MercaEvent(fixture:b2Fixture):void
		{
			this.fixture = fixture;
			
			var obj:Object = fixture.GetBody().GetUserData();
			if(obj.destroyed)
				return;
			obj.destroyed = true;
			fixture.GetBody().SetUserData( obj );
			
			var player:Player = Game.I.virtualView.getPlayerSceneObject();
			player.merca();
			Game.I.virtualView.setMovements(false)
			Game.I.tasks.add(
				new Sequence(
					new Wait(100),
					new Func(destroyObject),
					new Wait(3000),
					new Func(resetAnimation),
					new Wait(5000),
					new Func(reset)
				)		
			)
			Game.I.tasks.start();
		}
		private function destroyObject():void
		{
			Game.I.virtualView.destroyObject(this.fixture.GetBody());
		}
		private function resetAnimation():void
		{
			var player:Player = Game.I.virtualView.getPlayerSceneObject();
			player.jumpMerca();
			
			Game.I.virtualView.playerSpeed =16;
			
			Game.I.virtualView.setMovements(true);
			
		}
		private function reset():void
		{
			var player:Player = Game.I.virtualView.getPlayerSceneObject();
			player.setAction("idle");
			
			Game.I.virtualView.playerSpeed =8;
		}
	}
}