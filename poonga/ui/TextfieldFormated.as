package ui{
	import flash.text.Font;
	import flash.text.TextField;
	import flash.text.TextFormat;
	import flash.text.TextFieldType;
	   
    public class TextfieldFormated extends TextField {
    	
   		private var myFormat			:TextFormat;
		private var myFont				:Font;
   		
   		function TextfieldFormated(text:String, size:Number, color:uint, type:String="default" ) {
   			super();   			
   			defaultFormat(size, color);
   			
   			this.mouseEnabled = false;		
   			switch (type) {
   				case "right":
	   				myFormat.align = "right";
	   				break;
	   			case "input":
	   				this.type = TextFieldType.INPUT;
	   				this.mouseEnabled = true;
	   				break;
	   			case "center":
	   				myFormat.align = "center";
	   				break;
   			}
   			this.defaultTextFormat = myFormat;
   			this.htmlText = text;
   			this.multiline = true;
   			this.embedFonts = true;
   		}
   		
	     private function defaultFormat(size:Number, color:uint):void {
	     	
	        myFormat = new TextFormat();
	        myFormat.letterSpacing = -1;
	        myFormat.leading = -2;
			myFormat.font = "_Standard";
			myFormat.size = size;
			myFormat.color = color;
		}
	
    }
}