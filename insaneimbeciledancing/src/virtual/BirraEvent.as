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

	public class BirraEvent
	{
		private var fixture:b2Fixture
		
		public function BirraEvent(fixture:b2Fixture):void
		{
			Game.I._ui.winEnergy(0.5);
			this.fixture = fixture;
			
			var obj:Object = fixture.GetBody().GetUserData();
			if(obj.destroyed)
				return;
			obj.destroyed = true;
			fixture.GetBody().SetUserData( obj );
			
			var player:Player = Game.I.virtualView.getPlayerSceneObject();
			player.birra();
			Game.I.tasks.add(
				new Sequence(
					new Wait(100),
					new Func(destroyObject)
				)		
			)
			Game.I.tasks.start();
		}
		private function destroyObject():void
		{
			Game.I.virtualView.destroyObject(this.fixture.GetBody());
		}
	}
}