﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Tests;

[Library]
public partial class SpawnList : Panel
{
	VirtualScrollPanel Canvas;

	public SpawnList()
	{
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemSize = new Vector2( 100, 100 );
		Canvas.OnCreateCell = ( cell, data ) =>
		{
			var file = (string)data;
			var panel = cell.Add.Panel( "icon" );
			panel.AddEventListener( "onclick", () => 
			{
				ConsoleSystem.Run("spawn", "models/" + file);
				Sound.FromScreen("ui.button.press");
			} );
			panel.Style.BackgroundImage = Texture.Load( $"/models/{file}_c.png", false );
		};

		LoadAllItem(false);
	}

	private void LoadAllItem(bool isreload)
	{
		if (isreload)
			Canvas.Data.Clear();

		foreach (var file in FileSystem.Mounted.FindFile("models", "*.vmdl_c.png", true))
		{
			if (string.IsNullOrWhiteSpace(file)) continue;
			if (file.Contains("_lod0")) continue;
			if (file.Contains("clothes")) continue;

			Canvas.AddItem(file.Remove(file.Length - 6));
		}
	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();

		LoadAllItem(true);
	}
}
