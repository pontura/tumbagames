package view
{
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.b2Body;
	
	import flash.display.MovieClip;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.TaskContainer;
	import flashlib.tasks.TaskRunner;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;
	
	import view.objects.Objects;
	import view.objects.SceneObject;

	public class View extends MovieClip
	{
		public var objects:Objects;
		public var bg: BgMC
		
		public function View()
		{
			bg = new BgMC;
			Game.I.addChild(bg);
			Game.I.setChildIndex(bg,0);
			objects = new Objects();
			addChild(objects);			
		}
		public function reset():void{
			objects.reset();
			objects = null;
			Game.I.removeChild(bg);
		}
		public function start():void
		{			
			Game.I.tasks.add(
			new Loop(
				new Sequence(
					new Func (update, Game.I.virtualView.world.GetBodyList()),
					new Func (objects.sortObjects),
					new Wait(20),
					new Func (update, Game.I.virtualView.world.GetBodyList()),
					new Wait(20)
					)
				)
			)
		}
		private function update(body:b2Body):void
		{
			var obj:Object = body.GetUserData();
			if(obj && obj.sceneObject)
			{
				var mc:SceneObject = SceneObject(obj.sceneObject);				
				if(mc.mc) 
				{
					var vec:b2Vec2 = b2Vec2(body.GetPosition())
					mc.setView(vec.x)
					mc.x = vec.x*30;
					mc.y = vec.y*30;
				}
			}
			if(body.GetNext()) update(body.GetNext());
		}
	}
}