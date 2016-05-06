package virtual
{
	import Box2D.Collision.Shapes.b2CircleShape;
	import Box2D.Collision.Shapes.b2Shape;
	import Box2D.Common.Math.b2Mat22;
	import Box2D.Common.Math.b2Transform;
	import Box2D.Dynamics.Contacts.b2Contact;
	import Box2D.Dynamics.b2ContactListener;
	import Box2D.Dynamics.b2Fixture;
	import Box2D.Dynamics.b2FixtureDef;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import view.objects.Birra;
	import view.objects.Bomb;
	import view.objects.Merca;
	import view.objects.Player;
	
	public class CharactersContactListener extends b2ContactListener
	{
		public static const COLLISION:String = "collision";
		public var objects:Array;
		private static var PHYSICS_SCALE:Number = 1 / 30;
		
		public var eventDispatcher:EventDispatcher;
		
		
		public function CharactersContactListener()
		{
			eventDispatcher = new EventDispatcher();
		}
		override public function BeginContact(contact:b2Contact):void
		{
			var obj1:Object = contact.GetFixtureA().GetBody().GetUserData();
			var obj2:Object = contact.GetFixtureB().GetBody().GetUserData();
			if(!obj1 || !obj2) return;
			
			//
			if(obj1.sceneObject is Bomb)
				new BombEvent(contact.GetFixtureA())
			else if(obj2.sceneObject is Bomb)
				new BombEvent(contact.GetFixtureB())
			//	
			//
			if(obj1.sceneObject is Birra && obj2.sceneObject is Player)
				new BirraEvent(contact.GetFixtureA())
			else if(obj2.sceneObject is Birra && obj1.sceneObject is Player)
				new BirraEvent(contact.GetFixtureB())
				//	
			//
			if(obj1.sceneObject is Merca && obj2.sceneObject is Player)
				new MercaEvent(contact.GetFixtureA())
			else if(obj2.sceneObject is Merca && obj1.sceneObject is Player)
				new MercaEvent(contact.GetFixtureB())
				//
			eventDispatcher.dispatchEvent(new CollisionEvent(COLLISION, [obj1, obj2]));
		}
		
	}
}