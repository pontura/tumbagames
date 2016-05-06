package virtual
{
	import Box2D.Collision.Shapes.b2CircleShape;
	import Box2D.Collision.Shapes.b2Shape;
	import Box2D.Dynamics.b2Fixture;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;

	public class BombEvent
	{
		private var fixture:b2Fixture
		public function BombEvent(fixture:b2Fixture):void
		{
			this.fixture = fixture;
			var obj:Object = fixture.GetBody().GetUserData();
			if(obj.destroyed)
				return;
			obj.destroyed = true;
			fixture.GetBody().SetUserData( obj );
			var circle:b2CircleShape = new b2CircleShape (100 * Game.I.virtualView.PHYSICS_SCALE);
			var sh:b2Shape = fixture.GetShape();
			sh.Set( circle );
			Game.I.tasks.add(
				new Sequence(
					new Wait(1000),
					new Func(reset)
				)		
			)
		}
		private function reset():void
		{
			Game.I.virtualView.destroyObject(fixture.GetBody());
		}
	}
}