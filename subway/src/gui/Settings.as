package gui
{
	import flashlib.net.LoadFile;
	import flashlib.tasks.TaskEvent;
	import flashlib.utils.ObjectUtil;

	/**
	 * Class to parse and access a configuration file with application settings.
	 *
	 * <p>The data is copied directly ON the object.</p>
	 *
	 * <p>How to use it like a real pro?
	 * Create a settings.as file in your project with this:
	 * <pre>
	 * 	package
	 * 	{
	 * 		import com.qb9.flashlib.config.Settings;
	 * 		import com.adobe.serialization.json.JSON;
	 *
	 * 		public const settings:Settings = new Settings(JSON.decode);
	 * 	}
	 * </pre></p>
	 *
	 * <p>How to use it with XML ?
	 * <pre>
	 * 	package
	 * 	{
	 * 		import com.qb9.flashlib.config.Settings;
	 * 		import com.qb9.flashlib.net.XMLDeserializer;
	 *
	 * 		public const settings:Settings = new Settings(XMLDeserializer.fromString);
	 * 	}
	 * </pre></p>
	 *
	 * <p>How to synchronize the file loading with the rest of the app ?
	 * <pre>
	 *  public const settings:Settings = new Settings(JSON.decode);
	 *  ......
	 *  var file:LoadFile = new LoadFile('settings.json');
	 * 	settings.addFile(file);
	 * </pre>
	 *
	 *  And then either:
	 * <pre>
	 *  file.addEventListener(TaskEvent.COMPLETE, onSettingsLoaded);
	 * </pre>
	 * or
	 * <pre>
	 * 	tasks.add(new Sequence(
	 * 		file,
	 * 		new Func(onSettingsLoaded)
	 * 	));
	 * </pre>
	 * </p>
	 *
	 * <p>After this, from any part of the application you can do:
	 * <pre>
	 *  trace(settings.foo.bar.baz);
	 * </pre></p>
	 */

	public dynamic class Settings
	{
		protected var decode:Function;

		/**
		 * Create a Settings instance.
		 *
		 * <p>The instance will not be ready for use immediately. It will be initialized once the <code>loadTask</code> completes.</p>
		 *
		 * @param decoder Decoder function for the settings file.
		 */
		public function Settings(decoder:Function = null):void
		{
			decode = decoder;
		}

		protected function fileLoaded(e:TaskEvent):void
		{
			var file:LoadFile = e.target as LoadFile;
			file.removeEventListener(e.type, fileLoaded);
			feed(file.data);
		}

		public function addFile(file:LoadFile):void
		{
			if (file.loaded)
				feed(file.data);
			else
				file.addEventListener(TaskEvent.COMPLETE, fileLoaded);
		}

		public function feed(obj:Object):void
		{
			if (obj is String)
				obj = decode(obj);

			ObjectUtil.copy(obj, this);
		}
	}
}
