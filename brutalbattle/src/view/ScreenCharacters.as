package view
{
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Sequence;
	import com.qb9.flashlib.tasks.Wait;
	
	import globals.audio;
	
	public class ScreenCharacters extends ScreenCharactersMC
	{
		private var s1:CharacterSettings;
		private var s2:CharacterSettings;
		
		public function ScreenCharacters(s1:CharacterSettings, s2:CharacterSettings)
		{
			this.s1 = s1;
			this.s2 = s2;
			
			this.stop();
			
			this.characters.ph1.addChild(s1.character);
			this.characters.ph2.addChild(s2.character);		
		 	
		}
		public function start():void
		{
			Game.I.taskRunner.add(
				new Sequence 
				(
					new Func( playAudio, s1 ),
					new Wait(2000),
					new Func(move),
					new Func( playAudio, s2 ),
					new Wait(2000),
					new Func(reseted)
				)
			) 
		}
		private function playAudio(cs:CharacterSettings):void
		{
			 audio.play("character" + cs.characterId);
		}
		private function move():void
		{
			this.gotoAndPlay("on");
		}
		private function reseted():void
		{
			Game.I.startBoard();
		}

	}
}