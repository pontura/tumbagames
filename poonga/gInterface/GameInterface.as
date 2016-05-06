package gInterface
{
	import ui.TextfieldFormated;
	import flash.display.Sprite;
	import flash.text.TextFieldAutoSize;
	
	public class GameInterface extends BoardInterface
	{
		public var distanceText			:TextfieldFormated;
	 	public var pakobar				:Int_pakobar;
	 	public var vidasbar				:Int_vidas;
	 	private var puntos				:int = 0;
	 	private var energia				:Number = 1;
	 	private var vidas				:int = 3;
		
		function GameInterface () {
			this.txt_puntos.autoSize = TextFieldAutoSize.RIGHT;
			this.txt_level.autoSize = TextFieldAutoSize.LEFT;
			distanceText = new TextfieldFormated("0", 16, 0xFF0000);
			/*
			addChild (distanceText);			
			positionate();
			*/
			pakobar = new Int_pakobar();
			ph_pako.addChild(pakobar);
			
			vidasbar = new Int_vidas();
			ph_vidas.addChild(vidasbar);
			
		}
		public function changeWeapon(actualWeapon:int):void {
			pakobar.gotoAndStop(actualWeapon);
		}
		public function sumaPuntos(num:int):void {
			puntos += num;
			if (puntos<0)
				puntos = 0;
			this.txt_puntos.text = String(puntos);
		}
		public function restaEnergia(cant:int = 1):void {
			energia -= 0.33;
			if (energia < 0.2)
				restaVida();
			else
				vidasbar.bar_energia.scaleX = energia;
		}
		public function patovaresta():void {
			vidasbar.bar_energia_patova.scaleX -= 1/16;
		}
 		private function restaVida():void {
			Main.I().board.poonga.die()
			energia = 1;
			vidas -=1;
			if (vidas>0) {
				vidasbar.bar_vidas.scaleX -= 0.35;
				vidasbar.bar_energia.scaleX = 1;
			}else {
				vidasbar.bar_vidas.scaleX = 0;
				vidasbar.bar_energia.scaleX = 0;
				Main.I().gameOver();
			}
		}
		private function positionate():void {
			distanceText.x = 20;
			distanceText.y = 20;
		}
	}
}