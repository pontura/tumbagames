package view.objects
{
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.b2Body;
	
	import flash.display.MovieClip;
	import flash.events.Event;
	
	import flashlib.utils.ArrayUtil;
	import flashlib.utils.DisplayUtil;
	
	import virtual.CharactersContactListener;
	import virtual.CollisionEvent;

	public class Objects extends MovieClip
	{
		private var all:Array = [];
		
		public function Objects()
		{
			create(	Game.I.virtualView.world.GetBodyList() );
			Game.I.virtualView.characterContactListener.eventDispatcher.addEventListener(CharactersContactListener.COLLISION, collision);
		}
		public function reset():void
		{
			for each(var so:SceneObject in all)
			{
				so.reset();
			}
			all = [];
			DisplayUtil.dispose(this);
		}
		private function collision(e:CollisionEvent):void
		{
			if( e.objects.length==0 || !e.objects[0].sceneObject || !e.objects[1].sceneObject ) return;
			
			var so1:SceneObject = SceneObject(e.objects[0].sceneObject);
			var so2:SceneObject = SceneObject(e.objects[1].sceneObject);
			
			so1.collisioned(so2);
			so2.collisioned(so1);
			
		}
		private function create(body:b2Body):void
		{
			var vec:b2Vec2 = new b2Vec2(0,0)
			if(body.GetFixtureList() && body.GetFixtureList().GetUserData())
			{
				var data:Object = body.GetFixtureList().GetUserData();
				var mc:MovieClip;
				switch(data.name)
				{
					case "boss":
						mc = new Boss();
						break;
					case "player":
						mc = new Player;
						break;
					case "pibe1":
						mc = new Pibe(data.characterID);
						break;
					case "bomb":
						mc = new Bomb;
						break;
					case "merca":
						mc = new Merca;
						break;
					case "birra":
						mc = new Birra;
						break;
				}
				body.SetUserData( {sceneObject:mc} );
				addChild(mc);
				all.push(mc);
			}
			if(body.GetNext())
				create(body.GetNext())
		}
		public function destroy(obj:SceneObject):void
		{
			ArrayUtil.removeElement(all, obj);
		}
		public function sortObjects():void
		{
			all.sortOn("y",Array.NUMERIC);
			for each (var obj:SceneObject in all)
			{
				if(!obj)
					return;
				setChildIndex(obj, numChildren-1)
			}
		}
	}
}